using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts.AI.BehaviourTree.Base
{
  public enum NodeStatus
  {
    Success,
    Failure,
    Running
  }

  public class BehaviourTreeNode
  {

    public BehaviourTree tree;
    public BehaviourTreeNode parent;
    //public string tag;

    public BehaviourTreeNode(BehaviourTree tree, BehaviourTreeNode parent)//, string tag)
    {
      this.tree = tree;
      this.parent = parent;
      //this.tag = tag;
    }

    // the run function takes the controller as input to directly set its
    // behaviour in a leaf node
    public virtual NodeStatus Tick()
    {
      return NodeStatus.Success;
    }
  }
}
