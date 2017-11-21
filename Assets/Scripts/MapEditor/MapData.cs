using UnityEngine;
using Assets.Scripts.Utility;

public class MapData : ScriptableObject
{
  public CubeType[,,] Layout {
    get { return deepenArray(flatLayout); }
    set { flatLayout = flattenArray(value); }
  }
  
  [SerializeField]
  private CubeType[] flatLayout = new CubeType[200*20*200];

  public MapData() { }

  private CubeType[] flattenArray(CubeType[,,] deepArray)
  {
    int[] deepArrayDimensions = new int[] { deepArray.GetLength(0), deepArray.GetLength(1), deepArray.GetLength(2) };
    CubeType[] flatArray = new CubeType[deepArrayDimensions[0] * deepArrayDimensions[1] * deepArrayDimensions[2]];
    for (int x = 0; x < deepArrayDimensions[0]; x++)
      for (int y = 0; y < deepArrayDimensions[1]; y++)
        for (int z = 0; z < deepArrayDimensions[2]; z++)
          flatArray[x + (deepArrayDimensions[1] * (y + deepArrayDimensions[2] * z))] = deepArray[x, y, z];

    return flatArray;
  } 

  private CubeType[,,] deepenArray(CubeType[] flatArray)
  {
    CubeType[,,] deepArray = new CubeType[200, 20, 200];
    int[] deepArrayDimensions = new int[] { deepArray.GetLength(0), deepArray.GetLength(1), deepArray.GetLength(2) };
    for (int i = 0; i < flatArray.Length; i++) {
      //Debug.Log("i: " + i);
      var x = i % 200;
      var y = Mathf.FloorToInt((i % 4000) / 200);
      var z = Mathf.FloorToInt(i / 4000);
      deepArray[x, y, z] = flatArray[i];
    }

    return deepArray;
  }
}
