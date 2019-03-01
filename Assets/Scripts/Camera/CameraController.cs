using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  private GameObject CameraHolder;

  private float camSpeed = 5f;

  void Start()
  {
    CameraHolder = gameObject;
  }

  // Update is called once per frame
  //void Update() {
  //  //Camera movement keyboard
  //  // Works at any rotation
  //  if (Input.GetKey(KeyCode.W)) {
  //    CameraHolder.transform.Translate(new Vector3(0, 1, 0) * camSpeed / 10);
  //  }
  //  if (Input.GetKey(KeyCode.S)) {
  //    CameraHolder.transform.Translate(new Vector3(0, -1, 0) * camSpeed / 10);
  //  }
  //  if (Input.GetKey(KeyCode.A)) {
  //    CameraHolder.transform.Translate(new Vector3(1, 0, -1) * -camSpeed / 10);
  //  }
  //  if (Input.GetKey(KeyCode.D)) {
  //    CameraHolder.transform.Translate(new Vector3(1, 0, -1) * camSpeed / 10);
  //  }
  //  //Rotate 
  //  if (Input.GetKeyDown(KeyCode.Q)) {
  //    CameraHolder.transform.RotateAround(new Vector3(
  //    Mathf.Sin(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
  //    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * Camera.main.transform.parent.position.y,
  //    0,
  //    Mathf.Cos(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
  //    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * Camera.main.transform.parent.position.y
  //    )
  //    , Vector3.up, 90);
  //  }
  //  if (Input.GetKeyDown(KeyCode.E)) {
  //    CameraHolder.transform.RotateAround(new Vector3(
  //    Mathf.Sin(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
  //    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y,
  //    0,
  //    Mathf.Cos(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
  //    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y
  //    )
  //    , Vector3.up, -90);
  //  }

  //  //zoom
  //  if (Input.GetAxis("Mouse ScrollWheel") > 0) {
  //    float newCamSize = Mathf.Clamp(Camera.main.orthographicSize - 1, 1.0f, 20.0f);
  //    Camera.main.orthographicSize = newCamSize;
  //  }

  //  if (Input.GetAxis("Mouse ScrollWheel") < 0) {
  //    float newCamSize = Mathf.Clamp(Camera.main.orthographicSize + 1, 1.0f, 20.0f);
  //    Camera.main.orthographicSize = newCamSize;
  //  }
  //}

  public void Translate(Vector3 direction)
  {
    CameraHolder.transform.Translate(direction * camSpeed / 10);
  }

  public void Rotate(int degrees)
  {
    CameraHolder.transform.RotateAround(new Vector3(
    Mathf.Sin(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y,
    0,
    Mathf.Cos(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
    Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y
    )
    , Vector3.up, degrees);
  }

  public void Zoom(float speed)
  {
    float newCamSize = Mathf.Clamp(Camera.main.orthographicSize + speed, 1.0f, 20.0f);
    Camera.main.orthographicSize = newCamSize;
  }
}
