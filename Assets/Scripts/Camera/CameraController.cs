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
