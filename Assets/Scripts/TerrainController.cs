using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.World;
using Assets.Scripts.Character;

public class TerrainController : MonoBehaviour
{

  public GameObject TerrainHolder;
  private GameObject terrainCube;
  public CubeType[,,] worldLayout = new CubeType[200, 20, 200];

  void Start()
  {
    terrainCube = Resources.Load<GameObject>("Prefabs/terrainCube");

    // Instantiate floor level, used for testing
    for (int i = 0; i < 20; i++) {
      for (int j = 0; j < 20; j++) {
        GameObject baseCube = Instantiate(terrainCube, new Vector3(i, 0, j), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i, 0, j] = CubeType.GRASS;
      }
    }

    //build wall
    for (int i = 0; i < 5; i++) {
      for (int j = 1; j < 3; j++) {
        GameObject baseCube = Instantiate(terrainCube, new Vector3(i, j, 3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        worldLayout[i, j, 3] = CubeType.ROCK;
      }
    }

    // place rock
    for (int i = 0; i < 2; i++) {
      for (int j = 0; j < 2; j++) {
        GameObject baseCube = Instantiate(terrainCube, new Vector3(i + 7, 1, j + 3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        GameObject topCube = Instantiate(terrainCube, new Vector3(i + 7, 2, j + 3), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        topCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Rock") as Material;
        worldLayout[i + 7, 1, j + 3] = CubeType.ROCK;
        worldLayout[i + 7, 2, j + 3] = CubeType.ROCK;
      }
    }

    //make a plataeu
    for (int i = 0; i < 4; i++) {
      for (int j = 0; j < 4; j++) {
        GameObject baseCube = Instantiate(terrainCube, new Vector3(i + 15, 1, j + 13), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i + 15, 1, j + 13] = CubeType.GRASS;
      }
    }
    for (int i = 0; i < 2; i++) {
      for (int j = 0; j < 2; j++) {
        GameObject baseCube = Instantiate(terrainCube, new Vector3(i + 15, 2, j + 13), Quaternion.identity, TerrainHolder.transform) as GameObject;
        baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
        worldLayout[i + 15, 2, j + 13] = CubeType.GRASS;
      }
    }
  }
}
