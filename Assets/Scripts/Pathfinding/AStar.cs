using System;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Pathfinding
{
  public class AStar
  {
    public static int ManhattanDistance(int posx1, int posz1, int posx2, int posz2)
    {
      return Math.Abs(posx1 - posx2) + Math.Abs(posz1 - posz2);
    }
    
    public static bool exists(int x, int y, CubeType[,,] array)
    {
      return x >= 0 && y >= 0 && x < array.GetLength(0) && y < array.GetLength(1);
    }

    public static List<PathNode> ShortestPath(CubeType[,,] terrainLayout, bool[,,] obstructed, int startX, int startY,int startZ, 
      int goalX, int goalY, int goalZ)
    {
      // intantiate distance matrix
      float[,] distanceMatrix = new float[terrainLayout.GetLength(0), terrainLayout.GetLength(2)];
      for (int i = 0; i < distanceMatrix.GetLength(0); i++)
      {
        for (int j = 0; j < distanceMatrix.GetLength(1); j++)
        {
          distanceMatrix[i, j] = -1;
        }
      }
      distanceMatrix[startX, startZ] = 0;

      // Add start node
      PriorityQueueMin<Node> openSet = new PriorityQueueMin<Node>();
      Node initial = new Node(startX, startY, startZ, 0, ManhattanDistance(startX, startZ, goalX, goalZ),0);
      initial.MoveCost = 10; // initial baseline cost
      openSet.Insert(initial);

      while (!openSet.IsEmpty())
      {
        Node current = openSet.DelMin();
        if (current.x == goalX && current.z == goalZ)
        {
          List<PathNode> path = new List<PathNode>();
          while (current.Parent != null)
          {
            path.Add(new PathNode(current.MoveCost,new int[] { current.x, current.y, current.z }));
            current = current.Parent;
          }
          path.Reverse();
          return path;
        }
        // search all the neighbours
        List<Node> neighbours = current.generateNeighbours(terrainLayout, obstructed, distanceMatrix, goalX, goalY, goalZ);
        openSet.insertRange(neighbours);
      }
      // we failed to find the goal
      return null;
    }
  }

  #region Node

  public class Node : IComparable<Node>
  {
    public Node Parent { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public int z { get; set; }
    public float costToGetHere { get; set; }
    public float estimatedCostToGoal { get; set; }
    public float MoveCost { get; set; }
    public Node(int x,int y, int z, float costToGetHere, float estimatedCostToGoal, float moveCost)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.costToGetHere = costToGetHere;
      this.estimatedCostToGoal = estimatedCostToGoal;
      this.MoveCost = moveCost;
    }

    public float total()
    {
      return costToGetHere + estimatedCostToGoal;
    }
    public int CompareTo(Node node)
    {
      Node other = node;
      if (this.total() == other.total())
      {
        return 0;
      }
      else return (this.total() < other.total() ? -1 : 1);
    }

    public List<Node> generateNeighbours(CubeType[,,] terrainLayout, bool[,,] obstructed, float[,] distanceMatrix, int goalX, int goalY, int goalZ)
    {
      List<Node> list = new List<Node>();
      createAndAdd(x + 1, z, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1);
      createAndAdd(x + 1, z + 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1.5f);
      createAndAdd(x + 1, z - 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list,1.5f);
      createAndAdd(x - 1, z, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1);
      createAndAdd(x - 1, z - 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1.5f);
      createAndAdd(x - 1, z + 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1.5f);
      createAndAdd(x, z + 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1);
      createAndAdd(x, z - 1, goalX, goalY, goalZ, terrainLayout, obstructed, distanceMatrix, list, 1);

      return list;
    }

    private void createAndAdd(int newX, int newZ, int goalX, int goalY, int goalZ, CubeType[,,] terrainLayout,
                                bool[,,] obstructed, float[,] distanceMatrix, List<Node> list, float movecost)
    {
      if (AStar.exists(newX, newZ, terrainLayout))
      {
        bool passableTerrain = false;
        float newCost = this.costToGetHere + movecost; //TODO add idiagonal rout calculation
        float moveCost = movecost; //TODO add idiagonal rout calculation
        if (!obstructed[newX, y, newZ] && !( y-2 >= 0 && terrainLayout[newX, y - 2, newZ] == CubeType.NONE)) 
        {
          if ((terrainLayout[newX, y, newZ] == CubeType.NONE && terrainLayout[newX, y + 1, newZ] == CubeType.NONE) //check height difference upwards
            || (terrainLayout[newX, y + 1, newZ] == CubeType.NONE && terrainLayout[newX, y + 2, newZ] == CubeType.NONE) // check height difference for one up
            ||  (y-1 >=0 && (terrainLayout[newX, y, newZ] == CubeType.NONE && terrainLayout[newX, y - 1, newZ] == CubeType.NONE))) // check height difference for one down
          {
            passableTerrain = true;
          }
        } 

        if (passableTerrain)
        {
          int newEstimate = AStar.ManhattanDistance(newX, newZ, goalX, goalZ);
          int height = terrainLayout[newX, y, newZ] != CubeType.NONE ? y + 1 : terrainLayout[newX, y - 1, newZ] == CubeType.NONE ? y - 1 : y;
          Node newNode = new Node(newX, height,newZ, newCost, newEstimate, moveCost);
          newNode.Parent = this;
          if (distanceMatrix[newX, newZ] < 0 || newCost < distanceMatrix[newX, newZ])
          {
            list.Add(newNode);
            distanceMatrix[newX, newZ] = newCost;
          }
        }
      }
    }
  }
  #endregion
}
