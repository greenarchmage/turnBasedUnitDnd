using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public class CompositeNode : BehaviourTreeNode
  {

    public List<BehaviourTreeNode> children;

    public CompositeNode(BehaviourTree tree, BehaviourTreeNode parent, string tag) : base(tree, parent, tag)
    {
      this.children = new List<BehaviourTreeNode>();
    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }

  }
}
