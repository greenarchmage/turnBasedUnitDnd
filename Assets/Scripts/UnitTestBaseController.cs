using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.World;
using Assets.Scripts.Character;

public class UnitTestBaseController : MonoBehaviour {
  public GameObject CameraHolder;

  public GameObject TerrainHolder;

  private GameObject cube;
  private float camSpeed = 5;
  private cubeType[,,] worldLayout = new cubeType[200, 20, 200];

  private GameObject selected;

  private GameObject testPathChar;
  // Use this for initialization
  void Start()
  {
    // load prefab
    cube = Resources.Load("Prefabs/cube") as GameObject;
    // world generation test, from old source
    //generateWorld();

    // Instantiate floor level, used for testing

    for (int i = 0; i < 20; i++)
    {
      for (int j = 0; j < 20; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i, 0, j] = cubeType.GRASS;
      }
    }

    //build wall
    for(int i = 0; i< 5; i++)
    {
      for (int j = 1; j < 3; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i, j, 3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        worldLayout[i, j, 3] = cubeType.ROCK;
      }
    }

    // place rock
    for(int i = 0; i< 2; i++)
    {
      for(int j = 0; j < 2; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i+7, 1, j+3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        GameObject topCube = Instantiate(cube, new Vector3(i+7, 2, j+3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        topCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        worldLayout[i+7, 1, j+3] = cubeType.ROCK;
        worldLayout[i+7, 2, j+3] = cubeType.ROCK;
      }
    }

    //make a platuea
    for(int i = 0; i < 4; i++)
    {
      for(int j = 0; j<4; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i + 15, 1, j + 13), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i + 15, 1, j + 13] = cubeType.GRASS;
      }
    }
    for(int i = 0; i<2; i++)
    {
      for(int j = 0; j < 2; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i + 15, 2, j + 13), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i + 15, 2, j + 13] = cubeType.GRASS;
      }
    }
    // Instantiate characters
    // TODO
    testPathChar =Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(10f, 2f, 10f), Quaternion.identity) as GameObject;
    testPathChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();

    GameObject fightChar = Instantiate(Resources.Load("Prefabs/CharacterMedBlock"), new Vector3(12f, 2f, 12f), Quaternion.identity) as GameObject;
    fightChar.GetComponent<Character>().Stats = CharacterPresets.CreateWarrior();
  }

  // Update is called once per frame
  void Update()
  {
    #region CameraControls
    //Camera movement keyboard
    // Works at any rotation
    if (Input.GetKey(KeyCode.W))
    {
      CameraHolder.transform.Translate(new Vector3(0, 1, 0) * camSpeed / 10);
    }
    if (Input.GetKey(KeyCode.S))
    {
      CameraHolder.transform.Translate(new Vector3(0, -1, 0) * camSpeed / 10);
    }
    if (Input.GetKey(KeyCode.A))
    {
      CameraHolder.transform.Translate(new Vector3(1, 0, -1) * -camSpeed / 10);
    }
    if (Input.GetKey(KeyCode.D))
    {
      CameraHolder.transform.Translate(new Vector3(1, 0, -1) * camSpeed / 10);
    }
    //Rotate 
    if (Input.GetKeyDown(KeyCode.Q))
    {
      CameraHolder.transform.RotateAround(new Vector3(
      Mathf.Sin(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
      Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * Camera.main.transform.parent.position.y,
      0,
      Mathf.Cos(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
      Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * Camera.main.transform.parent.position.y
      )
      , Vector3.up, 90);
    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      CameraHolder.transform.RotateAround(new Vector3(
      Mathf.Sin(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
      Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y,
      0,
      Mathf.Cos(Camera.main.transform.localEulerAngles.y * Mathf.PI / 180) *
      Mathf.Tan(Camera.main.transform.localEulerAngles.x * Mathf.PI / 180) * CameraHolder.transform.position.y
      )
      , Vector3.up, -90);
    }

    //zoom
    if (Input.GetAxis("Mouse ScrollWheel") > 0)
    {
      float newCamSize = Mathf.Clamp(Camera.main.orthographicSize - 1, 1.0f, 20.0f);
      Camera.main.orthographicSize = newCamSize;
    }

    if (Input.GetAxis("Mouse ScrollWheel") < 0)
    {
      float newCamSize = Mathf.Clamp(Camera.main.orthographicSize + 1, 1.0f, 20.0f);
      Camera.main.orthographicSize = newCamSize;
    }

    #endregion

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
          selected = objectHit.gameObject;
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
        List<PathNode> path = AStar.ShortestPath(worldLayout, new bool[200, 20, 200],
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

  private void instantiateBlueprint(cubeType[,,] blueprint, Vector3 baseVector)
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
            case cubeType.NONE:
              //Air
              break;
            case cubeType.WOOD:
              if (worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == cubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = cubeType.WOOD;
              }
              break;
            case cubeType.GRASS:
              if (worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] == cubeType.NONE)
              {
                tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                worldLayout[i + (int)baseVector.x, j + (int)baseVector.y, k + (int)baseVector.z] = cubeType.GRASS;
              }
              break;
          }
        }
      }
    }
  }


  private void generateWorld()
  {
    // Noise height
    int[,] height = new int[200, 200];
    for (int i = 0; i < height.GetLength(0); i++)
    {
      for (int j = 0; j < height.GetLength(1); j++)
      {
        height[i, j] = Random.Range(0, 4);
      }
    }
    //Noise grass
    int[,] grass = new int[200, 200];
    for (int i = 0; i < grass.GetLength(0); i++)
    {
      for (int j = 0; j < grass.GetLength(1); j++)
      {
        grass[i, j] = Random.Range(0, 4);
      }
    }

    //Instantiate
    //test with grass
    for (int i = 0; i < 200; i++)
    {
      for (int j = 0; j < 200; j++)
      {
        GameObject baseCube = Instantiate(cube, new Vector3(i, height[i, j], j), Quaternion.identity) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i, 0, j] = cubeType.GRASS;
      }
    }

  }
}
