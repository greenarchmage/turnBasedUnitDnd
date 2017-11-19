using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

public delegate void CubeChangeEventHandler(object sender, CubeChangeEventArgs e);

public class MapBuilder : MonoBehaviour {

  public GameObject Cursor;
  public GameObject TerrainCube;
  public GameObject CameraHolder;

  public CubeType[,,] worldLayout = new CubeType[200, 20, 200];

  public event CubeChangeEventHandler SelectedCubeChanged;

  private CubeType selectedCubeType = CubeType.WOOD;
  private Material cursorMaterial;
  private Material[] terrainMaterials = new Material[3];

  // Ranges from 0-3 (0, 90, 180, 270)
  private int camFacing { get { return (int)(CameraHolder.transform.rotation.eulerAngles.y / 90f); } } 

  void Start()
  {
    cursorMaterial = Cursor.GetComponentInChildren<Renderer>().material;
    terrainMaterials[0] = Resources.Load<Material>("Materials/Wood");
    terrainMaterials[1] = Resources.Load<Material>("Materials/Grass");
    terrainMaterials[2] = Resources.Load<Material>("Materials/Rock");
    ChangeCubeType(selectedCubeType);
  }

  // Update is called once per frame
  void Update () {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit)) {
      //Original marker determination
      //Vector3 markerCoords = new Vector3(Mathf.Floor(hit.point.x), Mathf.Floor(Mathf.Abs(hit.point.y)), Mathf.Floor(hit.point.z));

      //Uses projected x & z to account for direction, uses hit.point.y for proper height
      Vector3 projectedCoords;
      if (Input.GetKey(KeyCode.LeftControl))
        projectedCoords = hit.point + ray.direction;
      else
        projectedCoords = hit.point - ray.direction;

      Vector3 markerCoords = new Vector3(Mathf.Floor(projectedCoords.x), Mathf.Floor(Mathf.Abs(hit.point.y)), Mathf.Floor(projectedCoords.z));
      Cursor.transform.position = markerCoords;

      if (Input.GetMouseButtonDown(0)) {
        Debug.Log(markerCoords);
        Debug.Log(ray);

        // Offset marker to center og cubic coordinates.
        Vector3 centerMarkerCoords = markerCoords + new Vector3(0.5f, 0.5f, 0.5f);

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
    var temp = SelectedCubeChanged;
    if (temp != null)
      SelectedCubeChanged(this, new CubeChangeEventArgs(newType));
    else
      Debug.Log("SelectedCubeChanged event is not subscribed to.");

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
