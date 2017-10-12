using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class Character : MonoBehaviour // maybe merge with selectable
  {
    public List<PathNode> Path { get; set; }

    private float deltaTime;
    void Update()
    {
      if(Path != null && Path.Count >0 && deltaTime < Time.realtimeSinceStartup)
      {
        transform.position = new Vector3(Path[0].Coord[0], (Path[0].Coord[1] + transform.localScale.y/2 -0.5f), Path[0].Coord[2]);
        Path.RemoveAt(0);
        deltaTime = Time.realtimeSinceStartup + 0.5f;
      }
    }

    public Vector3 GetGridPosition()
    {
      float x = transform.position.x;
      float y = transform.position.y - (transform.localScale.y / 2f) + 0.5f;
      float z = transform.position.z;
      return new Vector3(x, y, z);
    }
  }
}
