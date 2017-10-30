using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.World;
using Assets.Scripts.Character;

public class UnitTestBaseController : MonoBehaviour {
  private GameObject cube;

  private GameObject selected;

  private GameObject testPathChar;

  public TerrainController TerrainController;

  // Use this for initialization
  void Start()
  {
    // load prefab
    cube = Resources.Load("Prefabs/cube") as GameObject;

    // Instantiate characters
    // TODO
    testPathChar = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(10f, 2f, 10f), Quaternion.identity) as GameObject;
    testPathChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();

    GameObject fightChar = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(12f, 2f, 12f), Quaternion.identity) as GameObject;
    fightChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();
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
        if(objectHit.GetComponent<Selectable>() != null)
        {
          if (selected != null) changeEmission(selected, Color.black); // Color.black means no emission.

          selected = objectHit.gameObject;
          changeEmission(selected, emissionColor);
        } else if (selected != null) {
          changeEmission(selected, Color.black);
          selected = null;
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

          if(objectHit.GetComponent<MeshRenderer>().material.name == "Grass (Instance)")
          {
            selected.transform.position = objectHit.position + new Vector3(0f, (selected.transform.localScale.y/2) +0.5f, 0f);
          } else if(objectHit.GetComponent<Character>() != null && selected != null && selected.GetComponent<Character>() != null)
          {
            Character hitChar = objectHit.GetComponent<Character>();
            Character attackChar = selected.GetComponent<Character>();
            attackChar.AttackCharacter(hitChar);
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
        List<PathNode> path = AStar.ShortestPath(TerrainController.worldLayout, new bool[200, 20, 200],
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
      testPathChar.GetComponent<Character>().MoveLeft = testPathChar.GetComponent<Character>().Stats.MovementSpeed;
    }
  }

  private void changeEmission(GameObject gObj, Color c)
  {
    var emissionColor = c;
    Renderer[] gObjRenderers = gObj.GetComponentsInChildren<Renderer>();
    foreach (Renderer r in gObjRenderers) {
      r.material.SetColor("_EmissionColor", emissionColor);
    }
  }

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
              if (TerrainController.worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == CubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                TerrainController.worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = CubeType.WOOD;
              }
              break;
            case CubeType.GRASS:
              if (TerrainController.worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == CubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                TerrainController.worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = CubeType.GRASS;
              }
              break;
          }
        }
      }
    }
  }
}
