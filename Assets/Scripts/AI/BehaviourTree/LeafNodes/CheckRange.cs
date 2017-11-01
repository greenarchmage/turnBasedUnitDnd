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
      // return success if within range 
      // return failure else
      return NodeStatus.Success;
    }
  }
}
