using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.World;
using Assets.Scripts.Character;
using Assets.Scripts.AI.BehaviourTree;
using Assets.Scripts.AI.BehaviourTree.CompositeNodes;
using Assets.Scripts.AI.BehaviourTree.LeafNodes;
using Assets.Scripts.AI.BehaviourTree.DecoratorNodes;

public class AIController : MonoBehaviour {  

  public List<Character> AllCharacters;
  // Use this for initialization
  void Start()
  {
    // Instantiate characters
    AllCharacters = new List<Character>();

    //((SequenceNode)testTree.root).Children.Add(new Assets.Scripts.AI.BehaviourTree.LeafNodes.FindNearestEnemy(testTree, testTree.root));
    //((SequenceNode)testTree.root).Children.Add(new Assets.Scripts.AI.BehaviourTree.LeafNodes.MoveWithinRangeNearestEnemy(testTree, testTree.root));
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void StartAITurn()
  {
    foreach(Character cha in AllCharacters)
    {
      cha.StartTurn();
    }
  }
  public void AddCharacter(Character cha)
  {
    AllCharacters.Add(cha);
  }
}
