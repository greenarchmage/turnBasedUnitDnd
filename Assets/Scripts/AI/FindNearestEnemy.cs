using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI
{
  public class FindNearestEnemy
  {
    public void Run(Hashtable data)
    {
      Character.Character thisChar = (Character.Character)data["currentChar"];
      List<Character.Character> listOfCharacters = (List<Character.Character>)data["characters"];

      List<Pathfinding.PathNode> shortPath = null;

      foreach (Character.Character c in listOfCharacters)
      {
        if(c.Owner.Name != thisChar.Owner.Name)
        {
          Vector3 thisPos = thisChar.GetGridPosition();
          Vector3 cPos = c.GetGridPosition();
          List<Pathfinding.PathNode> path = Pathfinding.AStar.ShortestPath((Utility.CubeType[,,])data["terrainLayout"], (bool[,,])data["obstructed"],
            (int)thisPos.x, (int)thisPos.y, (int)thisPos.z,
          (int)cPos.x, (int)cPos.y, (int)cPos.z);
          if(shortPath == null || PathLength(path) < PathLength(shortPath))
          {
            shortPath = path;
          }
        }
      }

    }

    private float PathLength(List<Pathfinding.PathNode> path)
    {
      float pathCost = 0;
      for (int i = 0; i < path.Count; i++)
      {
        pathCost += path[i].Cost;
      }
      return pathCost;
    }
  }
}
