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
    public AttackTarget(BehaviourTree tree, BehaviourTreeNode parent, string tag) : base(tree, parent, tag)
    {
    }

    public override NodeStatus Tick()
    {
      // using the character attack nearest target
      // trigger the endturn, set the action points to 0 
      // return success 
      return NodeStatus.Success;
    }
  }
}
