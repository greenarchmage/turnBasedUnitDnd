using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Utility;
using System;

public class MaterialSelection : MonoBehaviour, IPointerDownHandler {

  public MapBuilder MapBuilderObject;
  public CubeType CubeType;

  public void OnPointerDown(PointerEventData eventData)
  {
    MapBuilderObject.ChangeCubeType(CubeType);
  }
}
