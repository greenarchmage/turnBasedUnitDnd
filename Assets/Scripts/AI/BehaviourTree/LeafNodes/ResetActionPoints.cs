using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;
using Assets.Scripts.Character;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class ResetActionPoints : LeafNode
  {
    public ResetActionPoints(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      //Set the character move points and the characters action points
      Assets.Scripts.Character.Character AICharacter = (Character.Character)tree.treeData[BehaviourTreeData.CurrentCharacter];
      AICharacter.ResetActionPoints();
      return NodeStatus.Success;
    }
  }
}
