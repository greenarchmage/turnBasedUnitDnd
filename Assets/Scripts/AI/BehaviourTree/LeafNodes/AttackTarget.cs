using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class AttackTarget : LeafNode
  {
    public AttackTarget(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      // using the character attack nearest target
      Assets.Scripts.Character.Character AICharacter = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      Character.Character target = (Character.Character)tree.treeData[BehaviourTreeData.CurrentTarget];
      AICharacter.AttackCharacter(target);
      // Set the action points to 0 and trigger end turn
      AICharacter.ActionsPoins = 0;
      tree.treeData[BehaviourTreeData.EndTurn] = true;
      return NodeStatus.Success;
    }
  }
}
