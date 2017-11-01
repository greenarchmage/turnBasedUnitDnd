using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree.Base;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree.LeafNodes
{
  public class Startup : LeafNode
  {
    public Startup(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
    }

    public override NodeStatus Tick()
    {
      // get from tree
      bool startup = false;
      if (startup)
      {
        startup = false;
        return NodeStatus.Success;
      }
      return NodeStatus.Failure;
    }
  }
}
