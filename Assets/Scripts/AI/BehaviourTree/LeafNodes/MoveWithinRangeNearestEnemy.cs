using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  /// <summary>
  /// This node is obsolete for the full movement tree
  /// </summary>
  public class MoveWithinRangeNearestEnemy : LeafNode
  {
    public MoveWithinRangeNearestEnemy(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      Character.Character thisChar = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      List<Pathfinding.PathNode> path = (List<Pathfinding.PathNode>)tree.treeData[BehaviourTreeData.ShortestPath];
      path.Reverse();
      int removeCount = 0;
      int rangeCost = 0;
      for(int i = 0; i<path.Count; i++)
      {
        rangeCost += (int)path[i].Cost;
        if(rangeCost > thisChar.Stats.EquippedWeapon.Range)
        {
          break;
        }
        removeCount++;
      }
      path.RemoveRange(0, removeCount);
      path.Reverse();
      thisChar.Path = path;
      Debug.Log("Path Set");
      
      //foreach (Pathfinding.PathNode node in path)
      //{
      //  transform.position = new Vector3(path[0].Coord[0], (path[0].Coord[1] + transform.localScale.y / 2 - 0.5f), path[0].Coord[2]);
      //}

      //if (path != null && path.Count > 0 && deltaTime < Time.realtimeSinceStartup && path[0].Cost < MoveLeft)
      //{
      //  transform.position = new Vector3(path[0].Coord[0], (path[0].Coord[1] + transform.localScale.y / 2 - 0.5f), path[0].Coord[2]);
      //  MoveLeft -= path[0].Cost;
      //  path.RemoveAt(0);
      //  deltaTime = Time.realtimeSinceStartup + 0.5f;
      //}

      return NodeStatus.Failure;
    }
  }
}
