using System;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;

namespace Assets.Scripts.AI.BehaviourTree.CompositeNodes
{
  public class SelectorNode : CompositeNode
  {

    public SelectorNode(BehaviourTree tree, BehaviourTreeNode parent, String tag) : base(tree, parent, tag)
    {

    }

    public override NodeStatus Tick()
    {
      foreach (BehaviourTreeNode child in children)
      {
        NodeStatus childStatus = child.Tick();
        if (childStatus == NodeStatus.Running)
        {
#if BT_DEBUG
                Debug.Log(tag + " Running");
#endif
          return NodeStatus.Running;
        }
        if (childStatus == NodeStatus.Success)
        {
#if BT_DEBUG
                Debug.Log(tag + " Child Success");
                Debug.Log(tag + " Success");
#endif
          return NodeStatus.Success;
        }
#if BT_DEBUG
            Debug.Log(tag + " Child Failure");
#endif
      }
#if BT_DEBUG
        Debug.Log(tag + " Failure");
#endif
      return NodeStatus.Failure;
    }
  }
}
