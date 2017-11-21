using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utility;
using UnityEditor;

public class MapDataHandler : MonoBehaviour {

  public InputField SaveNameInput;
  public Text EditorOnlyText;
  public GameObject TerrainCube;

  private CubeType[,,] rawMapLayout = new CubeType[200, 20, 200];
  private string saveName = "defaultNewSave";
  private Material[] terrainMaterials = new Material[3];

  // Use this for initialization
  void Start ()
  {
    terrainMaterials[0] = Resources.Load<Material>("Materials/Wood");
    terrainMaterials[1] = Resources.Load<Material>("Materials/Grass");
    terrainMaterials[2] = Resources.Load<Material>("Materials/Rock");

    // Removes "Editor Only" text from screen.
    Destroy(EditorOnlyText.gameObject);

    SaveNameInput.onEndEdit.AddListener(ChangeSaveName);
  }

  public void ChangeSaveName(string newName)
  { saveName = newName; }

  // Wont work when compiled due to lacking UnityEditor
  public void SaveMap()
  {
    MapData data = ScriptableObject.CreateInstance<MapData>();
    data.Layout = rawMapLayout;
    AssetDatabase.CreateAsset(data, "Assets/ScriptableObjects/" + saveName + ".asset");
    AssetDatabase.SaveAssets();
  }

  public void LoadMap()
  {
    MapData data = AssetDatabase.LoadAssetAtPath<MapData>("Assets/ScriptableObjects/" + saveName + ".asset");
    if (data != null) { 
      var newMapLayout = data.Layout;

      rawMapLayout = new CubeType[200, 20, 200];
      for (int x = 0; x < 200; x++)
        for (int y = 0; y < 20; y++)
          for (int z = 0; z < 200; z++)
            FlipAt(x, y, z, newMapLayout[x, y, z]);
    } else {
      Debug.LogWarning(string.Format("No MapData asset named {0} was found.", saveName));
    }
  }



  public void DestroyAt(Vector3 point)
  {
    foreach (Collider col in Physics.OverlapSphere(point, 0.1f)) {
      DeepDestroy(col.gameObject);
    }
  }

  private void DeepDestroy(GameObject gameObject)
  {
    if (gameObject.transform.parent != null)
      DeepDestroy(gameObject.transform.parent.gameObject);
    else
      Destroy(gameObject);
  }

  public void FlipAt(int x, int y, int z, CubeType flipType) {
    if (rawMapLayout[x, y, z] != flipType && flipType != CubeType.NONE) {
      rawMapLayout[x, y, z] = flipType;
      var newCube = Instantiate(TerrainCube, new Vector3(x, y, z), Quaternion.identity, null);
      newCube.GetComponentInChildren<Renderer>().material = terrainMaterials[(int)flipType - 1]; // -1 to converts enum to array index.
    }
    else {
      rawMapLayout[x, y, z] = CubeType.NONE;
    }
  }

  #region FlipAt Overloads
  public void FlipAt(Vector3Int point, CubeType flipType)
  {
    FlipAt(point.x, point.y, point.z, flipType);
  }

  public void FlipAt(Vector3 point, CubeType flipType)
  {
    FlipAt((int)point.x, (int)point.y, (int)point.z, flipType);
  }
  #endregion
}
