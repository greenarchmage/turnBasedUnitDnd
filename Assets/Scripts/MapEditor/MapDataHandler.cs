using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utility;

public class MapDataHandler : MonoBehaviour {

  public InputField SaveNameInput;
  public Text EditorOnlyText;
  public GameObject TerrainCube;

  private CubeType[,,] rawMapLayout = new CubeType[200, 20, 200];
  private string saveName = "defaultNewSave";
  private Material[] terrainMaterials = new Material[5];

  // Use this for initialization
  void Start ()
  {
    terrainMaterials[0] = Resources.Load<Material>("Materials/TerrainMaterials/Wood");
    terrainMaterials[1] = Resources.Load<Material>("Materials/TerrainMaterials/Grass");
    terrainMaterials[2] = Resources.Load<Material>("Materials/TerrainMaterials/Rock");
    terrainMaterials[3] = Resources.Load<Material>("Materials/TerrainMaterials/Dirt");
    terrainMaterials[4] = Resources.Load<Material>("Materials/TerrainMaterials/Leaves");

    SaveNameInput.onEndEdit.AddListener(ChangeSaveName);
  }

  public void ChangeSaveName(string newName)
  { saveName = newName; }


  public void SaveMap()
  {
    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
    MapData mapData = new MapData();
    mapData.Layout = rawMapLayout;
    System.IO.Stream fileStream = System.IO.File.OpenWrite(Application.dataPath + "/" + saveName + ".gmap");

    binaryFormatter.Serialize(fileStream, mapData);
  }

  public void LoadMap()
  {
    System.IO.FileStream fileStream = System.IO.File.OpenRead(Application.dataPath + "/" + saveName + ".gmap");
    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
    MapData mapData = (MapData)binaryFormatter.Deserialize(fileStream);
    if (mapData != null) {
      CubeType[,,] newMapLayout = mapData.Layout;

      CubeType newType;
      for (int x = 0; x < 200; x++)
        for (int y = 0; y < 20; y++)
          for (int z = 0; z < 200; z++) {
            DestroyAt(x, y, z);
            newType = newMapLayout[x, y, z];
            if (newType != CubeType.NONE) CreateAt(x, y, z, newType);
          }
    }
    else {
      Debug.LogWarning(string.Format("No MapData asset named {0} was found.", saveName));
    }
  }


  public void CreateAt(Vector3 point, CubeType type) { CreateAt((int)point.x, (int)point.y, (int)point.z, type); }
  public void CreateAt(int x, int y, int z, CubeType type)
  {
    if (type != CubeType.NONE) { 
      rawMapLayout[x, y, z] = type;
      var newCube = Instantiate(TerrainCube, new Vector3(x, y, z), Quaternion.identity, null);
      newCube.GetComponentInChildren<Renderer>().material = terrainMaterials[(int)type - 1]; // -1 to converts enum to array index.
    } else {
      Debug.LogWarning("Attempted to create NONE-type cube at " + new Vector3(x, y, z));
    }
  }

  public void DestroyAt(Vector3 point) { DestroyAt((int)point.x, (int)point.y, (int)point.z); }
  public void DestroyAt(int x, int y, int z)
  {
    rawMapLayout[x, y, z] = CubeType.NONE;
    Vector3 centerPoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f); // Ensures that destruction only hits targeted object.
    foreach (Collider col in Physics.OverlapSphere(centerPoint, 0.1f)) {
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
}
