﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.AI.BehaviourTree;
using Assets.Scripts.Pathfinding;
using Assets.Scripts.GameMechanics;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class Character : MonoBehaviour, ICharacter // NOTE: maybe merge with selectable
  {
    public List<PathNode> Path { get; set; }

    public CharacterData Stats { get; set; }

    public Player Owner { get; set; }
    public float MoveLeft { get; set; }
    public int ActionsPoins { get; set; }

    public BehaviourTree AIBehaviour;

    private float deltaTime;
    void Update()
    {
      if(Path != null && Path.Count > 0 && deltaTime < Time.realtimeSinceStartup && Path[0].Cost < MoveLeft)
      {
        transform.position = new Vector3(Path[0].Coord[0], (Path[0].Coord[1] + transform.localScale.y/2 -0.5f), Path[0].Coord[2]);
        MoveLeft -= Path[0].Cost;
        Path.RemoveAt(0);
        deltaTime = Time.realtimeSinceStartup + 0.5f;
      }
      if(AIBehaviour != null && !(bool)AIBehaviour.treeData[BehaviourTreeData.EndTurn])
      {
        AIBehaviour.root.Tick();
      }

    }

    public Vector3 GetGridPosition()
    {
      float x = transform.position.x;
      float y = transform.position.y - (transform.localScale.y / 2f) + 0.5f;
      float z = transform.position.z;
      return new Vector3(x, y, z);
    }

    public void AttackCharacter(Character target)
    {
      //TODO Do unarmed???
      if (Stats.EquippedWeapon != null)
      {
        if (Stats.EquippedWeapon.Range >= Mathf.FloorToInt(Vector3.Distance(target.GetGridPosition(), GetGridPosition())))
        {
          Debug.Log("Attacking character " + target.name);
          new AttackAction() { Target = target, Source = this }.ResolveAction();
          if (!target.Stats.IsDead)
          {
            Debug.Log("Target still alive");
          }
          else
          {
            Debug.Log("Target is dead");
            Destroy(target.gameObject);
          }
        }
        else
        {
          Debug.Log("Out of range");
        }
      }
    }

    public void ResetActionPoints()
    {
      MoveLeft = Stats.MovementSpeed;
      ActionsPoins = 1;
    }
    public void StartTurn()
    {
      if (AIBehaviour != null)
      {
        AIBehaviour.AddDataToTree(BehaviourTreeData.StartUp, true);
        AIBehaviour.AddDataToTree(BehaviourTreeData.EndTurn, false);
      }
    }

    /// <summary>
    /// Moves the character to the next point on the path
    /// </summary>
    /// <param name="path"></param>
    /// <returns>true if there still is movepoints left</returns>
    public bool MoveToPoint(List<Pathfinding.PathNode> path)
    {
      if (path != null && path.Count > 0 && deltaTime < Time.realtimeSinceStartup && path[0].Cost < MoveLeft)
      {
        transform.position = new Vector3(path[0].Coord[0], (path[0].Coord[1] + transform.localScale.y / 2 - 0.5f), path[0].Coord[2]);
        MoveLeft -= path[0].Cost;
        path.RemoveAt(0);
        deltaTime = Time.realtimeSinceStartup + 0.5f;
      } else if(path[0].Cost > MoveLeft)
      {
        return false;
      }
      return true;
    }
  }
}
