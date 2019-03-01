using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;

namespace Assets.Scripts
{
  public class InputController : MonoBehaviour {

    public GameObject SelectedObject;
    public CameraController CameraController;

    public KeyCode CameraTranslateUp    = KeyCode.W;
    public KeyCode CameraTranslateRight = KeyCode.D;
    public KeyCode CameraTranslateDown  = KeyCode.S;
    public KeyCode CameraTranslateLeft  = KeyCode.A;

    public KeyCode CameraRotateClockwise        = KeyCode.Q;
    public KeyCode CameraRotateCounterClockwise = KeyCode.E;

    private Color selectedEmissionColor = new Color(0.045f, 0.875f, 0.84f);
    private Dictionary<KeyCode, Action> actionMap = new Dictionary<KeyCode, Action>();
    #region Action Map Keys/Values
    private IEnumerable<KeyCode> inputList {
      get {
        return new KeyCode[] {
          CameraTranslateUp,
          CameraTranslateRight,
          CameraTranslateDown,
          CameraTranslateLeft,
          CameraRotateClockwise,
          CameraRotateCounterClockwise
        };
      }
    }
    private IEnumerable<Action> actionList {
      get {
        return new Action[] {
          new Action(() => CameraController.Translate(new Vector3(0, 1, 0))),
          new Action(() => CameraController.Translate(new Vector3(1, 0, -1))),
          new Action(() => CameraController.Translate(new Vector3(0, -1, 0))),
          new Action(() => CameraController.Translate(new Vector3(-1, 0, 1))),
          new Action(() => CameraController.Rotate(90)),
          new Action(() => CameraController.Rotate(-90))
        };
      }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
      var actionEnumerator = actionList.GetEnumerator();
      actionEnumerator.MoveNext();

      foreach (KeyCode k in inputList) {
        actionMap.Add(k, actionEnumerator.Current);
        actionEnumerator.MoveNext();
      }
    }

    // Update is called once per frame
    void Update () {
      // Handle Translation / Rotation
      foreach (KeyCode k in inputList)
        if (Input.GetKey(k))
          actionMap[k].Invoke();

      if (Input.GetAxis("Mouse ScrollWheel") > 0)
        CameraController.Zoom(-1);
      else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        CameraController.Zoom(1);

      if (Input.GetMouseButtonDown(0)) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
          Transform objectHit = hit.transform;

          // Do something with the object that was hit by the raycast.
          // NOTE: GetComponent is a heavy operation, and is used twice in the following 5 lines.
          Selectable selectable = objectHit.GetComponent<Selectable>();
          if (selectable != null) {
            if (SelectedObject != null)
              SelectedObject.GetComponent<Selectable>().ChangeEmission(Color.black); // Color.black means no emission.

            SelectedObject = objectHit.gameObject;
            selectable.ChangeEmission(selectedEmissionColor);
          }
        }
      }

      //Raycast mouse 1
      if (Input.GetMouseButtonDown(1)) {
        if (SelectedObject != null) {
          RaycastHit hit;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

          if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;

            //if (SelectedObject.GetComponent<Character>() != null) {
            //  Vector3 selectedCubePos = SelectedObject.GetComponent<Character>().GetGridPosition();
            //  Vector3 hitPos = hit.transform.position;
            //  List<PathNode> path = AStar.ShortestPath(TerrainController.MapLayout, new bool[200, 20, 200],
            //      (int)selectedCubePos.x, (int)selectedCubePos.y, (int)selectedCubePos.z,
            //      (int)hitPos.x, (int)hitPos.y, (int)hitPos.z);
            //  SelectedObject.GetComponent<Character>().Path = path;
            //  SelectedObject.GetComponent<Character>().ResetActionPoints();
            //}
          }
        }
      }
    }
  }
}
