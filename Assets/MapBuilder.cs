using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

public class MapBuilder : MonoBehaviour {

  public GameObject Cursor;
  public GameObject TerrainCube;
  public CubeType[,,] worldLayout = new CubeType[200, 20, 200];

  // Update is called once per frame
  void Update () {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit)) {
      var markerCoords = new Vector3(Mathf.Floor(hit.point.x), Mathf.Floor(Mathf.Abs(hit.point.y)), Mathf.Floor(hit.point.z));
      Cursor.transform.position = markerCoords;

      if (Input.GetMouseButtonDown(0)) {
        if (worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] == CubeType.NONE) { 
          worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] = CubeType.GRASS;
          Instantiate(TerrainCube, markerCoords, Quaternion.identity, null);
        } else { 
          worldLayout[(int)markerCoords.x, (int)markerCoords.y, (int)markerCoords.z] = CubeType.NONE;
          if (hit.transform.gameObject.name == "EditorTerrainCube" || hit.transform.gameObject.name == "TerrainCube")
            DeepDestroy(hit.transform.gameObject);
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
}
