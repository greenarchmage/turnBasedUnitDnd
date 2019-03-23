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

public class UnitTestBaseController : MonoBehaviour {
  private GameObject cube;

  private GameObject selected;

  private GameObject testPathChar;

  public TerrainController TerrainController;
  public AIController AIControllerObj;

  private BehaviourTree testTree;

  private List<Player> playerList = new List<Player>(); // maybe change to dictionary? how will that work with turn change?
  private int currentPlayerInt;
  // Use this for initialization
  void Start()
  {
    // load prefab
    cube = Resources.Load("Prefabs/cube") as GameObject;

    // create players, should come from some other source
    Player ai = new Player() { Name = "MercenaryMenace" };
    playerList.Add(ai);
    Player player = new Player() { Name = "Simba" };
    playerList.Add(player);

    // Instantiate characters
    // TODO

    GameObject PlayerCharacterObject = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(3f, 2f, 3f), Quaternion.identity) as GameObject;
    PlayerCharacterObject.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();
    PlayerCharacterObject.GetComponent<Character>().Owner = player;
    player.Actors.Add(PlayerCharacterObject.GetComponent<Character>());
    AIControllerObj.AddCharacter(PlayerCharacterObject.GetComponent<Character>()); // add the character to the list of characters the AI can interact with

    AIControllerObj.CreateAICharacterAtLocation(new Vector3(10f, 2f, 7f), ai, CharacterPresets.CreateWarrior());

    currentPlayerInt = 0;
    AIControllerObj.StartAITurn();
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

    // End turn
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
