using System.Collections;

namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public class DecoratorNode : BehaviourTreeNode
  {

    public BehaviourTreeNode child;

    public DecoratorNode(BehaviourTree tree, BehaviourTreeNode parent, string tag) : base(tree, parent, tag)
    {

    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }
  }
}
