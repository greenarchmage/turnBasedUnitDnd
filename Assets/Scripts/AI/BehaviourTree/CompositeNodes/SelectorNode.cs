using System;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;

namespace Assets.Scripts.AI.BehaviourTree.CompositeNodes
{
  public class SelectorNode : CompositeNode
  {

    public SelectorNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {

    }

    public override NodeStatus Tick()
    {
      foreach (BehaviourTreeNode child in Children)
      {
        NodeStatus childStatus = child.Tick();
        if (childStatus == NodeStatus.Running)
        {
          return NodeStatus.Running;
        }
        if (childStatus == NodeStatus.Success)
        {
          return NodeStatus.Success;
        }
      }
      return NodeStatus.Failure;
    }
  }
}
