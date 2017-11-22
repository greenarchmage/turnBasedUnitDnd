using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.World;
using Assets.Scripts.Character;

public class TerrainController : MonoBehaviour
{

  public MapData MapData;
  public GameObject TerrainHolder;
  public CubeType[,,] MapLayout = new CubeType[200, 20, 200];

  private GameObject terrainCube;
  private Material[] terrainMaterials = new Material[5];

  // Use this for initialization
  void Start()
  {
    terrainMaterials[0] = Resources.Load<Material>("Materials/TerrainMaterials/Wood");
    terrainMaterials[1] = Resources.Load<Material>("Materials/TerrainMaterials/Grass");
    terrainMaterials[2] = Resources.Load<Material>("Materials/TerrainMaterials/Rock");
    terrainMaterials[3] = Resources.Load<Material>("Materials/TerrainMaterials/Dirt");
    terrainMaterials[4] = Resources.Load<Material>("Materials/TerrainMaterials/Leaves");

    terrainCube = Resources.Load<GameObject>("Prefabs/terrainCube");

    LoadMap();
  }

  // TODO: Make into a Co-routine?
  public void LoadMap()
  {
    if (MapData != null) {
      CubeType[,,] newMapLayout = MapData.Layout;

      CubeType newType;
      for (int x = 0; x < 200; x++)
        for (int y = 0; y < 20; y++)
          for (int z = 0; z < 200; z++) {
            newType = newMapLayout[x, y, z];

            if (newType != CubeType.NONE)
              CreateAt(x, y, z, newType);
          }
    }
    else {
      Debug.LogWarning(string.Format("MapData field is null. Hook MapData asset to scene!"));
    }
  }
  
  // TODO: Move to (new) MapManipulator class?
  public void CreateAt(Vector3 point, CubeType type) { CreateAt((int)point.x, (int)point.y, (int)point.z, type); }
  public void CreateAt(int x, int y, int z, CubeType type)
  {
    if (type != CubeType.NONE) {
      MapLayout[x, y, z] = type;
      var newCube = Instantiate(terrainCube, new Vector3(x, y, z), Quaternion.identity, TerrainHolder.transform);
      newCube.GetComponentInChildren<Renderer>().material = terrainMaterials[(int)type - 1]; // -1 to converts enum to array index.
    }
    else {
      Debug.LogWarning("Attempted to create NONE-type cube at " + new Vector3(x, y, z));
    }
  }
}
