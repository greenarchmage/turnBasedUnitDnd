using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;

namespace Assets.Scripts.AI.BehaviourTree.CompositeNodes
{
  public class SequenceNode : CompositeNode
  {

    public SequenceNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
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
        if (childStatus == NodeStatus.Failure)
        {
          return NodeStatus.Failure;
        }
      }
      return NodeStatus.Success;
    }
  }
}
