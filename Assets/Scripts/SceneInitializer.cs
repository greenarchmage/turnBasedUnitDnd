using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts { 
  public class SceneInitializer : MonoBehaviour {

    public List<GameObject> InitObjects = new List<GameObject>();
    public List<Vector3> InitPositions = new List<Vector3>();

	  // Use this for initialization
	  void Start () {
		  if (InitPositions.Count < InitObjects.Count) { 
        Debug.LogWarning("Not all objects have initialized positions!! Defaulting to V(0,0,0)");
        for (int i = InitPositions.Count; i <= InitObjects.Count; i++)
          InitPositions.Add(new Vector3(0, 0, 0));
      }

      for (int i = 0; i < InitObjects.Count; i++) {
        Instantiate<GameObject>(InitObjects[i], InitPositions[i], Quaternion.identity);
      }
	  }
  }
}
