using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Pathfinding
{
  public class PathNode
  {
    public float Cost { get; set; }
    public int[] Coord { get; set; }
    public PathNode(float moveCost, int[] coordinate)
    {
      Cost = moveCost;
      Coord = coordinate;
    }
  }
}
