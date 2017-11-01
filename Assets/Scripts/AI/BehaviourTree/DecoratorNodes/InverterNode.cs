using System;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;

namespace Assets.Scripts.AI.BehaviourTree.DecoratorNodes
{
  public class InverterNode : DecoratorNode
  {

    public InverterNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {

    }

    public override NodeStatus Tick()
    {
      NodeStatus childStatus = child.Tick();
      if (childStatus == NodeStatus.Failure)
      {
        return NodeStatus.Success;
      }
      if (childStatus == NodeStatus.Success)
      {
        return NodeStatus.Failure;
      }
      return childStatus;
    }
  }
}


