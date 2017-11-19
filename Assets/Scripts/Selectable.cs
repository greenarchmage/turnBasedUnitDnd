using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {
  public void ChangeEmission(Color c)
  {
    var emissionColor = c;
    Renderer[] gObjRenderers = gameObject.GetComponentsInChildren<Renderer>();
    foreach (Renderer r in gObjRenderers) {
      r.material.SetColor("_EmissionColor", emissionColor);
    }
  }
}
