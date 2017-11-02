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
  public class MoveTowardsNearestEnemy : LeafNode
  {
    public MoveTowardsNearestEnemy(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      Character.Character thisChar = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      List<Pathfinding.PathNode> path = (List<Pathfinding.PathNode>)tree.treeData[BehaviourTreeData.ShortestPath];
      if (thisChar.MoveToPoint(path))
      {
        return NodeStatus.Success;
      } else
      {
        tree.treeData[BehaviourTreeData.EndTurn] = true;
        return NodeStatus.Failure;
      }
    }
  }
}
