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
using Assets.Scripts;

public class AIController : MonoBehaviour {

  public TerrainController TerrainController;

  public List<Character> AllCharacters;
  // Use this for initialization
  void Start()
  {
    // Instantiate characters
    AllCharacters = new List<Character>();
    
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

  public void CreateAICharacterAtLocation(Vector3 pos, Player owner, CharacterData stats)
  {
    GameObject character = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), pos, Quaternion.identity) as GameObject;
    character.GetComponent<Character>().Stats = stats;
    character.GetComponent<Character>().Owner = owner;
    AddCharacter(character.GetComponent<Character>());

    BehaviourTree controlTree = new BehaviourTree(character);
    //Startupsubtree reset the character and find the nearest enemy
    SuccessNode startUpDecorator = new SuccessNode(controlTree, controlTree.root);
    SequenceNode startUpSubTree = new SequenceNode(controlTree, startUpDecorator);
    startUpSubTree.Children.Add(new Startup(controlTree, startUpSubTree));
    startUpSubTree.Children.Add(new ResetActionPoints(controlTree, startUpSubTree));
    startUpSubTree.Children.Add(new FindNearestEnemy(controlTree, startUpSubTree));

    startUpDecorator.child = startUpSubTree;
    ((SequenceNode)controlTree.root).Children.Add(startUpDecorator);

    //Attack subtree
    InverterNode attackInverter = new InverterNode(controlTree, controlTree.root);
    SequenceNode attackSubtree = new SequenceNode(controlTree, attackInverter);
    attackSubtree.Children.Add(new CheckRange(controlTree, attackSubtree));
    attackSubtree.Children.Add(new AttackTarget(controlTree, attackSubtree));

    attackInverter.child = attackSubtree;
    ((SequenceNode)controlTree.root).Children.Add(attackInverter);

    //Movement subtree
    ((SequenceNode)controlTree.root).Children.Add(new Assets.Scripts.AI.BehaviourTree.LeafNodes.MoveTowardsNearestEnemy(controlTree, controlTree.root));

    // Tree data, this should be added/or available to all AI characters
    controlTree.AddDataToTree(BehaviourTreeData.AllCharacters, AllCharacters);
    controlTree.AddDataToTree(BehaviourTreeData.CurrentCharacter, character.GetComponent<Character>());
    controlTree.AddDataToTree(BehaviourTreeData.WorldLayout, TerrainController.MapLayout);
    controlTree.AddDataToTree(BehaviourTreeData.WorldLayoutObstructed, TerrainController.Obstructed);
    controlTree.AddDataToTree(BehaviourTreeData.StartUp, true);
    controlTree.AddDataToTree(BehaviourTreeData.EndTurn, true);
    character.GetComponent<Character>().AIBehaviour = controlTree;
  }
}
