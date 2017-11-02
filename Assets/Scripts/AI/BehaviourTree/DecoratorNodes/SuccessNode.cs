using System;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;

namespace Assets.Scripts.AI.BehaviourTree.DecoratorNodes
{
  public class SuccessNode : DecoratorNode
  {

    public SuccessNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {

    }

    public override NodeStatus Tick()
    {
      child.Tick();
      return NodeStatus.Success;
    }
  }
}


