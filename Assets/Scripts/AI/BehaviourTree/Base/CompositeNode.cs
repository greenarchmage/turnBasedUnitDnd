using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public class CompositeNode : BehaviourTreeNode
  {

    public List<BehaviourTreeNode> Children;

    public CompositeNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {
      this.Children = new List<BehaviourTreeNode>();
    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }

  }
}
