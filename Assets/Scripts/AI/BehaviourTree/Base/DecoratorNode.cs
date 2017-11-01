using System.Collections;

namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public class DecoratorNode : BehaviourTreeNode
  {

    public BehaviourTreeNode child;

    public DecoratorNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {

    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }
  }
}
