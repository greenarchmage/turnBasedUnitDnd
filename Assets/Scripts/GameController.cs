using UnityEngine;
using Assets.Scripts.Utility;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    //private bool run = false;
    //private int curx = 0;
    //private int cury = 0;
    //private int curz = 0;

    private int count = 0;

    private int worldx = 100;
    private int worldy = 20;
    private int worldz = 100;

    private GameObject cube;
    private float camSpeed = 5;
    private cubeType[,,] worldLayout;
    private bool[,,] visible;
    // Use this for initialization
    void Start () {
        worldLayout = new cubeType[worldx, worldy, worldz];
        visible = new bool[worldx, worldy, worldz];
        cube = Resources.Load("Prefabs/cube") as GameObject;
        // TODO: temp function, should use better algorithm to generate level
        generateWorld();
        Debug.Log("World generated");
        //Start in top corner
        // TODO fix the point wise spawn of blocks
        //instantiateBlockType(new Vector3(worldx - 1, worldy - 1, worldz - 1), worldLayout[worldx - 1, worldy - 1, worldz - 1]);
        //instantiateNearByBlocks(new Vector3(9, 9, 9));

        testFillWorld();
        /*
        //Fill the world, slowest
        for (int i = 0; i< worldx; i++)
        {
            for(int j= 0; j< worldy; j++)
            {
                for(int k = 0; k < worldz; k++)
                {
                    instantiateBlockType(new Vector3(i, j, k), worldLayout[i, j, k]);
                }
            }
        }
        */
        // TODO: Temporary function to Instantiate floor level 
        /*
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                GameObject baseCube = Instantiate(cube, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                baseCube.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                worldLayout[i, 0, j] = cubeType.GRASS;
            }
        }
        */
    }

    // Update is called once per frame
    void Update () {
        //Camera movement keyboard
        // Works at any rotation
        if (Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(new Vector3(0, 1, 0) * camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(new Vector3(0, -1, 0) * camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(new Vector3(1, 0, 1) * -camSpeed / 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(new Vector3(1, 0, 1) * camSpeed / 10);
        }

        /*
        //Test of the algorithm in short burst
        if (run)
        {
            instantiateBlockType(new Vector3(curx, cury, curz), worldLayout[curx, cury, curz]);
            curx++;
            if(curx == worldx)
            {
                curx = 0;
                cury++;
            }
            if(cury == worldy)
            {
                cury = 0;
                curz++;
            }
            if(curz == worldz)
            {
                curz = 0;
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Save world
            for(int i = 0; i < worldLayout.GetLength(0); i++)
            {
                for(int j = 0; j<worldLayout.GetLength(0); j++)
                {
                    for(int k = 0; k< worldLayout.GetLength(0); k++)
                    {

                    }
                }
            }
        }
    }

    /// <summary>
    /// Used to instatiate a structure made by the player
    /// </summary>
    /// <param name="blueprint"></param>
    /// <param name="baseVector"></param>
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
                            tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                            tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;
                            worldLayout[i, j, k] = cubeType.WOOD;
                            break;
                        case cubeType.GRASS:
                            tempStruc = Instantiate(cube, baseVector + new Vector3(i, j, k), Quaternion.identity) as GameObject;
                            tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Grass") as Material;
                            worldLayout[i, j, k] = cubeType.GRASS;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Deprecated
    /// </summary>
    /// <param name="coords"></param>
    private void instantiateNearByBlocks(Vector3 coords)
    {
        count++;
        int x = (int)coords.x;
        int y = (int)coords.y;
        int z = (int)coords.z;

        List<Vector3> coordNone = new List<Vector3>();

        for(int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    int xt =  x-i;
                    int yt = y-j;
                    int zt = z-k;
                    if((xt) >=visible.GetLength(0) || (yt) >= visible.GetLength(1) ||
                        (zt) >= visible.GetLength(2) ||xt<0 ||yt<0||zt<0)
                    {
                        //Out of bounds check
                        continue;
                    }/*
                    if(k == 0 && j == 0 && i == 0)
                    {
                        //sameblock check
                        continue;
                    }
                    */
                    //Debug.Log("xt " + xt + ", yt " + yt + ", zt " + zt);
                    //Debug.Log(worldLayout[xt, yt, zt]);
                    if (!visible[xt, yt, zt])
                    {
                        Debug.Log("Non VisibleBlock " + "xt " + xt + ", yt " + yt + ", zt " + zt);
                        //Create block at location
                        instantiateBlockType(new Vector3(xt, yt, zt), worldLayout[xt, yt, zt]);
                        //Debug.Log(visible[xt, yt, zt]);
                        if(worldLayout[xt,yt,zt] == cubeType.NONE && count < 15)
                        {
                            coordNone.Add(new Vector3(xt, yt, zt));
                            //recursive strat
                            //instantiateNearByBlocks(new Vector3(xt, yt, zt));
                        }
                    }
                }
            }
        }

        for(int i = 0; i<coordNone.Count; i++)
        {
            instantiateNearByBlocks(coordNone[i]);
        }
    }

    /// <summary>
    /// Function used to instatiate
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="type"></param>
    private void instantiateBlockType(Vector3 pos, cubeType type)
    {
        //Debug.Log("Cube start");
        visible[(int)pos.x, (int)pos.y, (int)pos.z] = true;
        worldLayout[(int)pos.x, (int)pos.y, (int)pos.z] = type;
        switch (type)
        {
            case cubeType.NONE:
                //Air
                break;
            case cubeType.WOOD:
                instantiateCube(pos, "Wood");
                break;
            case cubeType.GRASS:
                instantiateCube(pos, "Grass");
                break;
            case cubeType.ROCK:
                instantiateCube(pos, "Rock");
                break;
        }
    }

    /// <summary>
    /// Helper function. Instatiate the cube when it has been set in instantiateBlockType
    /// </summary>
    /// <param name="pos"> Position in the world space</param>
    /// <param name="name"> Name of the material</param>
    private void instantiateCube(Vector3 pos, string name)
    {
        GameObject tempStruc = Instantiate(cube, pos, Quaternion.identity) as GameObject;
        tempStruc.GetComponent<MeshRenderer>().material = Resources.Load("Materials/"+name) as Material;
    }

    /// <summary>
    /// Main function for generating the world
    /// </summary>
    private void generateWorld()
    {
        // Noise height
        int[,] height = new int[visible.GetLength(0), visible.GetLength(2)];
        for (int i = 0; i < height.GetLength(0); i++)
        {
            for (int j = 0; j < height.GetLength(1); j++)
            {
                height[i, j] = Random.Range(1, 3);
            }
        }
        //Noise grass
        int[,] grass = new int[visible.GetLength(0), visible.GetLength(2)];
        for (int i = 0; i < grass.GetLength(0); i++)
        {
            for (int j = 0; j < grass.GetLength(1); j++)
            {
                grass[i, j] = Random.Range(0, 4);
            }
        }

        //Fill array
        for (int i = 0; i < visible.GetLength(0); i++)
        {
            for (int j = 0; j < visible.GetLength(2); j++)
            {
                for( int y = 0; y< height[i, j]; y++)
                {
                    worldLayout[i, y, j] = cubeType.ROCK;
                }
                for(int x = 0; x< grass[i,j]; x++)
                {
                    worldLayout[i, height[i, j]+x, j] = cubeType.GRASS;
                }
                for(int z = grass[i, j]+ height[i, j]; z<visible.GetLength(1); z++)
                {
                    worldLayout[i, z, j] = cubeType.NONE;
                }
            }
        }

    }

    /// <summary>
    /// Test function for filling the world
    /// So far the fastest
    /// </summary>
    private void testFillWorld()
    {
        //traverse the world
        for(int x = 0; x < worldx; x++)
        {
            for (int z = 0; z < worldz; z++)
            {
                //Debug.Log(worldLayout[x, 0, z]);
                //traverse height last to ensure the fewest runs
                for (int y = worldy-1; y >= 0; y--)
                {
                    if (worldLayout[x, y, z] == cubeType.NONE)
                    {
                        continue;
                    }
                    int found = 0;
                    //Check for none x
                    if( x+1 >= worldx || worldLayout[x+1, y, z] != cubeType.NONE)
                    {
                        //increment found
                        found++;
                    }
                    if(x-1 < 0 ||worldLayout[x-1,y,z] != cubeType.NONE)
                    {
                        found++;
                    }

                    //Check y
                    if (y + 1 >= worldy || worldLayout[x, y+1, z] != cubeType.NONE)
                    {
                        //increment found
                        found++;
                    }
                    if (y - 1 < 0 || worldLayout[x, y-1, z] != cubeType.NONE)
                    {
                        found++;
                    }

                    //Check z
                    if (z + 1 >= worldz || worldLayout[x, y, z+1] != cubeType.NONE)
                    {
                        //increment found
                        found++;
                    }
                    if (z - 1 < 0 || worldLayout[x, y, z-1] != cubeType.NONE)
                    {
                        found++;
                    }
                    // This means at least one neighbor is NONE
                    if (found < 6)
                    {
                        instantiateBlockType(new Vector3(x, y, z), worldLayout[x, y, z]);
                    }

                    if(found == 6)
                    {
                        break;
                    }
                }
            }
        }
    }
}
