using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI.BehaviourTree.Base;
using Assets.Scripts.AI.BehaviourTree.CompositeNodes;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree
{
  public enum BehaviourTreeData
  {
    CurrentCharacter,
    AllCharacters,
    WorldLayout,
    WorldLayoutObstructed,
    ShortestPath,
    DesireDestination,
    SetNewDestAllowed,
    DesireTimeStart,
    DesireStimulated,
    NearbyActors
  }

  public class BehaviourTree
  {
    public GameObject owner;
    public Dictionary<BehaviourTreeData, object> treeData { get; private set; }
    public BehaviourTreeNode root { get; private set; }

    public BehaviourTree(GameObject owner)
    {
      root = new SequenceNode(this, null);
      this.owner = owner;
      this.treeData = new Dictionary<BehaviourTreeData, object>();
    }

    public void AddDataToTree(BehaviourTreeData dataType, object data)
    {
      //Checks if data is present
      if (treeData.ContainsKey(dataType))
      {
        treeData[dataType] = data;
      }
      else
      {
        treeData.Add(dataType, data);
      }
    }

    // run the tree
    public void Tick()
    {
      root.Tick();
    }
  }
}
