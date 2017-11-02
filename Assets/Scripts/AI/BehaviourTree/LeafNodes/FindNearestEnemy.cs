using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class FindNearestEnemy : LeafNode
  {
    public FindNearestEnemy(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      Character.Character thisChar = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      List<Character.Character> listOfCharacters = (List<Character.Character>)tree.treeData[BehaviourTreeData.AllCharacters];

      List<Pathfinding.PathNode> shortPath = null;
      Character.Character target = null;
      foreach (Character.Character c in listOfCharacters)
      {
        if (c.Owner.Name != thisChar.Owner.Name)
        {
          Vector3 thisPos = thisChar.GetGridPosition();
          Vector3 cPos = c.GetGridPosition();
          List<Pathfinding.PathNode> path = Pathfinding.AStar.ShortestPath((Utility.CubeType[,,])tree.treeData[BehaviourTreeData.WorldLayout], 
            (bool[,,])tree.treeData[BehaviourTreeData.WorldLayoutObstructed],
            (int)thisPos.x, (int)thisPos.y, (int)thisPos.z,
          (int)cPos.x, (int)cPos.y, (int)cPos.z);
          if (shortPath == null || PathLength(path) < PathLength(shortPath))
          {
            shortPath = path;
            target = c;
          }
        }
      }
      if(shortPath != null)
      {
        tree.AddDataToTree(BehaviourTreeData.ShortestPath, shortPath);
        tree.AddDataToTree(BehaviourTreeData.CurrentTarget, target);
        return NodeStatus.Success;
      } else
      {
        return NodeStatus.Failure;
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
