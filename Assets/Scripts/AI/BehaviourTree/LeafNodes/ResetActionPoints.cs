using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class ResetActionPoints : LeafNode
  {
    public ResetActionPoints(BehaviourTree tree, BehaviourTreeNode parent, string tag) : base(tree, parent, tag)
    {
    }

    public override NodeStatus Tick()
    {
      //Set the character move points and the characters action points
      return NodeStatus.Success;
    }
  }
}
