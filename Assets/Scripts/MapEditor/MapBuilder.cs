using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

public class MapBuilder : MonoBehaviour {

  public GameObject Cursor;
  private Material cursorMaterial;
  public GameObject TerrainCube;

  public CubeType[,,] worldLayout = new CubeType[200, 20, 200];

  private CubeType selectedCubeType = CubeType.WOOD;
  private Material[] terrainMaterials = new Material[3];

  void Start()
  {
    cursorMaterial = Cursor.GetComponentInChildren<Renderer>().material;
    terrainMaterials[0] = Resources.Load<Material>("Materials/Wood");
    terrainMaterials[1] = Resources.Load<Material>("Materials/Grass");
    terrainMaterials[2] = Resources.Load<Material>("Materials/Rock");
    ChangeCubeType(CubeType.WOOD);
  }

  // Update is called once per frame
  void Update () {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit)) {
      var markerCoords = new Vector3(Mathf.Floor(hit.point.x), Mathf.Floor(Mathf.Abs(hit.point.y)), Mathf.Floor(hit.point.z));
      Cursor.transform.position = markerCoords;

      if (Input.GetMouseButtonDown(0)) {
        Debug.Log(markerCoords);

        // Offset marker to center og cubic coordinates.
        var centerMarkerCoords = markerCoords + new Vector3(0.5f, 0.5f, 0.5f);

        foreach (Collider col in Physics.OverlapSphere(centerMarkerCoords, 0.1f)) {
          DeepDestroy(col.gameObject);
        }

        if (worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] != selectedCubeType) {
          worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] = selectedCubeType;
          var newCube = Instantiate(TerrainCube, markerCoords, Quaternion.identity, null);
          newCube.GetComponentInChildren<Renderer>().material = terrainMaterials[(int)selectedCubeType-1]; // -1 to converts enum to array index.
        } else {
          worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] = CubeType.NONE;
        }
      }
    }
	}

  private void DeepDestroy(GameObject gameObject)
  {
    if (gameObject.transform.parent != null)
      DeepDestroy(gameObject.transform.parent.gameObject);
    else
      Destroy(gameObject);
  }

  public void ChangeCubeType(CubeType newType)
  {
    selectedCubeType = newType;
    cursorMaterial.color = cubeTypeToColor(newType);
  }

  private Color cubeTypeToColor(CubeType type)
  {
    switch (type) {
      case CubeType.WOOD: return new Color(0.66f, 0.5f, 0.16f, 0.75f);
      case CubeType.GRASS: return new Color(0.2f, 0.5f, 0.2f, 0.75f);
      case CubeType.ROCK: return new Color(0.5f, 0.5f, 0.5f, 0.75f);
      default: return new Color(1, 1, 0, 0.75f);
    }
  }
}
