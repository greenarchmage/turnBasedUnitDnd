using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Utility;
using System;

public class MaterialSelection : MonoBehaviour, IPointerDownHandler {

  public MapBuilder MapBuilderObject;
  public CubeType CubeType;

  private bool isSelected;

  void Start()
  {
    MapBuilderObject.SelectedCubeChanged += OnCubeChange;
  }
  
  public void OnPointerDown(PointerEventData eventData)
  {
    MapBuilderObject.ChangeSelectedCubeType(CubeType);
  }

  public void OnCubeChange(object sender, CubeChangeEventArgs e)
  {
    if (e.NewCubeType == CubeType) { 
      isSelected = true;
    } else { 
      isSelected = false;
    }
  }
}
