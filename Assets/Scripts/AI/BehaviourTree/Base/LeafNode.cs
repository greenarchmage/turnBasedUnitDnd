using System.Collections;

namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public class LeafNode : BehaviourTreeNode
  {

    public bool running { get; protected set; }

    /// <summary>
    /// Basic leaf node for the behavior tree
    /// </summary>
    /// <param name="tree">The behavior tree for the node</param>
    /// <param name="parent">The parent node in the behavior tree</param>
    /// <param name="tag">Sub group tag</param>
    public LeafNode(BehaviourTree tree, BehaviourTreeNode parent) : base(tree, parent)
    {

    }

    public override NodeStatus Tick()
    {
      return NodeStatus.Success;
    }

  }
}

