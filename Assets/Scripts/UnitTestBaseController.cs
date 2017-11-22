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

public class UnitTestBaseController : MonoBehaviour {
  private GameObject cube;

  private GameObject selected;

  private GameObject testPathChar;

  public TerrainController TerrainController;
  public AIController AIControllerObj;

  private BehaviourTree testTree;
  // Use this for initialization
  void Start()
  {
    // load prefab
    cube = Resources.Load("Prefabs/cube") as GameObject;

    // Instantiate characters
    // TODO

    GameObject fightChar = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(12f, 2f, 12f), Quaternion.identity) as GameObject;
    fightChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();
    fightChar.GetComponent<Character>().Owner = new Assets.Scripts.Player() { Name = "Simba" };
    AIControllerObj.AddCharacter(fightChar.GetComponent<Character>());

    AIControllerObj.CreateAICharacterAtLocation(new Vector3(10f, 2f, 10f), new Assets.Scripts.Player() { Name = "Alex" }, CharacterPresets.CreateWarrior());
    //testPathChar = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(10f, 2f, 10f), Quaternion.identity) as GameObject;
    //testPathChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();
    //testPathChar.GetComponent<Character>().Owner = new Assets.Scripts.Player() { Name = "Alex" };
    //AIControllerObj.AddCharacter(testPathChar.GetComponent<Character>());

    //testTree = new BehaviourTree(testPathChar);
    ////Startupsubtree reset the character and find the nearest enemy
    //SuccessNode startUpDecorator = new SuccessNode(testTree, testTree.root);
    //SequenceNode startUpSubTree = new SequenceNode(testTree, startUpDecorator);
    //startUpSubTree.Children.Add(new Startup(testTree, startUpSubTree));
    //startUpSubTree.Children.Add(new ResetActionPoints(testTree, startUpSubTree));
    //startUpSubTree.Children.Add(new FindNearestEnemy(testTree, startUpSubTree));

    //startUpDecorator.child = startUpSubTree;
    //((SequenceNode)testTree.root).Children.Add(startUpDecorator);

    ////Attack subtree
    //InverterNode attackInverter = new InverterNode(testTree, testTree.root);
    //SequenceNode attackSubtree = new SequenceNode(testTree, attackInverter);
    //attackSubtree.Children.Add(new CheckRange(testTree, attackSubtree));
    //attackSubtree.Children.Add(new AttackTarget(testTree, attackSubtree));

    //attackInverter.child = attackSubtree;
    //((SequenceNode)testTree.root).Children.Add(attackInverter);

    ////Movement subtree
    //((SequenceNode)testTree.root).Children.Add(new Assets.Scripts.AI.BehaviourTree.LeafNodes.MoveTowardsNearestEnemy(testTree, testTree.root));

    //// Tree data, this should be added/or available to all AI characters
    //testTree.AddDataToTree(BehaviourTreeData.AllCharacters, AIControllerObj.AllCharacters);
    //testTree.AddDataToTree(BehaviourTreeData.CurrentCharacter, testPathChar.GetComponent<Character>());
    //testTree.AddDataToTree(BehaviourTreeData.WorldLayout, TerrainController.MapLayout);
    //testTree.AddDataToTree(BehaviourTreeData.WorldLayoutObstructed, new bool[200, 20, 200]);
    //testTree.AddDataToTree(BehaviourTreeData.StartUp, true);
    //testTree.AddDataToTree(BehaviourTreeData.EndTurn, true);
    //testPathChar.GetComponent<Character>().AIBehaviour = testTree;
  }

  // Update is called once per frame
  void Update()
  {
    Color emissionColor = new Color(0.045f, 0.875f, 0.84f);
    //Raycast mouse 0
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit))
      {
        Transform objectHit = hit.transform;

        // Do something with the object that was hit by the raycast.
        // NOTE: GetComponent is a heavy operation, and is used twice in the following 5 lines.
        Selectable selectableScript = objectHit.GetComponent<Selectable>();
        if (selectableScript != null) {
          if (selected != null)
            selected.GetComponent<Selectable>().ChangeEmission(Color.black); // Color.black means no emission.

          selected = objectHit.gameObject;
          selectableScript.ChangeEmission(emissionColor);
        }
      }
    }

    //Raycast mouse 1
    if (Input.GetMouseButtonDown(1))
    {
      if(selected != null)
      {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
          Transform objectHit = hit.transform;

          //if(objectHit.GetComponent<MeshRenderer>().material.name == "Grass (Instance)")
          //{
          //  selected.transform.position = objectHit.position + new Vector3(0f, (selected.transform.localScale.y/2) +0.5f, 0f);
          //}
          if(selected.GetComponent<Character>() != null)
          {
            Vector3 selectedCubePos = selected.GetComponent<Character>().GetGridPosition();
            Vector3 hitPos = hit.transform.position;
            List<PathNode> path = AStar.ShortestPath(TerrainController.MapLayout, new bool[200, 20, 200],
          (int)selectedCubePos.x, (int)selectedCubePos.y, (int)selectedCubePos.z,
          (int)hitPos.x, (int)hitPos.y, (int)hitPos.z);
            selected.GetComponent<Character>().Path = path;
            selected.GetComponent<Character>().ResetActionPoints();
          }
        }
      }
    }

    // test shortest path
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if(selected != null)
      {
        Vector3 selectedCubePos = selected.GetComponent<Character>().GetGridPosition();
        Vector3 testCubePos = testPathChar.GetComponent<Character>().GetGridPosition();
        List<PathNode> path = AStar.ShortestPath(TerrainController.MapLayout, new bool[200, 20, 200],
          (int)testCubePos.x, (int)testCubePos.y, (int)testCubePos.z,
          (int)selectedCubePos.x, (int)selectedCubePos.y, (int)selectedCubePos.z);
        testPathChar.GetComponent<Character>().Path = path;
        float pathCost = 0;
        for(int i = 0; i < path.Count; i++)
        {
          pathCost += path[i].Cost;
        }
        Debug.Log("Cost of path " +pathCost);
      }
    }

    // reset MovementLeft test
    if (Input.GetKeyDown(KeyCode.Return))
    {
      AIControllerObj.StartAITurn();
    }
  }

  //private void changeEmission(GameObject gObj, Color c)
  //{
  //  var emissionColor = c;
  //  Renderer[] gObjRenderers = gObj.GetComponentsInChildren<Renderer>();
  //  foreach (Renderer r in gObjRenderers) {
  //    r.material.SetColor("_EmissionColor", emissionColor);
  //  }
  //}

  private void instantiateBlueprint(CubeType[,,] blueprint, Vector3 baseVector)
  {
    for (int i = 0; i < blueprint.GetLength(0); i++)
    {
      for (int j = 0; j < blueprint.GetLength(1); j++)
      {
        for (int k = 0; k < blueprint.GetLength(2); k++)
        {
          GameObject tempStruc = null;
          switch (blueprint[i, j, k])
          {
            case CubeType.NONE:
              //Air
              break;
            case CubeType.WOOD:
              if (TerrainController.MapLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == CubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                TerrainController.MapLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = CubeType.WOOD;
              }
              break;
            case CubeType.GRASS:
              if (TerrainController.MapLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == CubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                TerrainController.MapLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = CubeType.GRASS;
              }
              break;
          }
        }
      }
    }
  }
}
