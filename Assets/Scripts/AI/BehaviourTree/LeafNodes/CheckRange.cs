using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class CheckRange : LeafNode
  {
    public CheckRange(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      // using the weapon, check if the nearest target ( in the tree already) is within range
      Assets.Scripts.Character.Character AICharacter = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      Character.Character target = (Character.Character)tree.treeData[BehaviourTreeData.CurrentTarget];
      Vector3 tarGridPos = target.GetGridPosition();
      Vector3 curGridPos = AICharacter.GetGridPosition();
      if(AICharacter.Stats.EquippedWeapon.Range >= Mathf.FloorToInt(Vector3.Distance(tarGridPos, curGridPos)))
      {
        return NodeStatus.Success;
      }
      else
      {
        return NodeStatus.Failure;
      }
    }
  }
}
