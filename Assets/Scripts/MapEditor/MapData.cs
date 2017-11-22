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
        for (int z = 0; z < deepArrayDimensions[2]; z++) {
          var i = x + (deepArrayDimensions[0] * (y + deepArrayDimensions[1] * z));
          flatArray[i] = deepArray[x, y, z];
          //if (i % 4000 == 0 || i % 399 == 0) Debug.Log("(x,y,z,i): " + new Vector4(x, y, z, i));
        }

    return flatArray;
  }

  private CubeType[,,] deepenArray(CubeType[] flatArray)
  {
    CubeType[,,] deepArray = new CubeType[200, 20, 200];
    int[] deepArrayDimensions = new int[] { deepArray.GetLength(0), deepArray.GetLength(1), deepArray.GetLength(2) };
    int xLimit = deepArrayDimensions[0];
    int yLimit = xLimit * deepArrayDimensions[1];
    for (int i = 0; i < flatArray.Length; i++) {
      var x = i % xLimit;
      var y = Mathf.FloorToInt((i % yLimit) / xLimit);
      var z = Mathf.FloorToInt(i / yLimit);
      //if (i % 4000 == 0 || i % 399 == 0) Debug.Log("(x,y,z,i): " + new Vector4(x,y,z, i));
      deepArray[x, y, z] = flatArray[i];
    }

    return deepArray;
  }
}
