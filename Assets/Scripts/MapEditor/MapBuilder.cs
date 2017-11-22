using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;

public delegate void CubeChangeEventHandler(object sender, CubeChangeEventArgs e);

public class MapBuilder : MonoBehaviour {

#if UNITY_EDITOR
  public MapDataHandler MapDataHandler;
#endif

  public GameObject Cursor;
  public GameObject CameraHolder;

  public event CubeChangeEventHandler SelectedCubeChanged;

  private CubeType selectedCubeType = CubeType.WOOD;
  private Material cursorMaterial;

  void Start()
  {
    cursorMaterial = Cursor.GetComponentInChildren<Renderer>().material;
    ChangeSelectedCubeType(selectedCubeType);
  }

  // Update is called once per frame
  void Update () {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit)) {
      Vector3 markerCoords = hit.point + (hit.normal / 2);
      markerCoords = new Vector3(Mathf.Floor(markerCoords.x), Mathf.Floor(Mathf.Abs(hit.point.y)), Mathf.Floor(markerCoords.z));

      Cursor.transform.position = markerCoords;

#if UNITY_EDITOR
      if (Input.GetMouseButtonDown(0)) {
        //Debug.Log("hit.point: " + hit.point);
        //Debug.Log("hit.normal: " + hit.normal);
        //Debug.Log("marker: " + markerCoords);
        //Debug.Log(ray);

        // MarkerCoords should always hit vacant fields.
        MapDataHandler.CreateAt(markerCoords, selectedCubeType);
      }

      if (Input.GetMouseButtonDown(1)) {
        // Offset marker to center og cubic coordinates.
        if (hit.collider.gameObject.name != "MapFoundation") {
          Vector3 hitObjectCoords = rootPosition(hit.collider.gameObject);
          MapDataHandler.DestroyAt(hitObjectCoords);
        }
      }
#endif
    }
  }

  public void ChangeSelectedCubeType(CubeType newType)
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
      case CubeType.DIRT: return new Color(0.88f, 0.66f, 0.33f, 0.75f);
      case CubeType.LEAVES: return new Color(0.25f, 0.85f, 0.25f, 0.75f);
      default: return new Color(1, 1, 0, 0.75f);
    }
  }

  private Vector3 rootPosition(GameObject gObj)
  {
    if (gObj.transform.parent != null)
      return rootPosition(gObj.transform.parent.gameObject);
    else
      return gObj.transform.position;
  }
}
