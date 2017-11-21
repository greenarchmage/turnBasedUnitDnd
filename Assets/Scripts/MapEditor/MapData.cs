using UnityEngine;
using Assets.Scripts.Utility;

public class MapData : ScriptableObject
{
  public CubeType[,,] Layout { get { return Layout; } }
  private CubeType[,,] layout = new CubeType[200, 20, 200];

  public MapData(CubeType[,,] layout) { this.layout = layout; }
}
