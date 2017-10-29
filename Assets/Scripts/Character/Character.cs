﻿using System;
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

    public CharacterData Stats { get; set; }

    public Player Owner { get; set; }
    public float MoveLeft { get; set; }

    private float deltaTime;
    void Update()
    {
      if(Path != null && Path.Count >0 && deltaTime < Time.realtimeSinceStartup && Path[0].Cost < MoveLeft)
      {
        transform.position = new Vector3(Path[0].Coord[0], (Path[0].Coord[1] + transform.localScale.y/2 -0.5f), Path[0].Coord[2]);
        MoveLeft -= Path[0].Cost;
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

    public void AttackCharacter(Character target)
    {
      //TODO Do unarmed???
      if (Stats.EquipedWeapon != null)
      {
        if (Stats.EquipedWeapon.Range >= Mathf.FloorToInt(Vector3.Distance(target.GetGridPosition(), GetGridPosition())))
        {
          Debug.Log("Attacking character " + target.name);
          int damageDealt = (UnityEngine.Random.Range(0, Stats.EquipedWeapon.DamageDie) + 1) * -1;
          if (target.ChangeHitpoints(damageDealt))
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
    /// <summary>
    /// Change the hitpoints of the character.
    /// Clamped between 0 and the max hitpoints of the character
    /// </summary>
    /// <param name="change">The amount to change the hitpoints, positive add, negative subtracts</param>
    /// <returns></returns>
    public bool ChangeHitpoints(int change)
    {
      Stats.CurrentHitpoints += change;
      Stats.CurrentHitpoints = Mathf.Clamp(Stats.CurrentHitpoints, 0, Stats.Hitpoints);
      if(Stats.CurrentHitpoints <= 0)
      {
        return false;
      } else
      {
        return true;
      }
    }
  }
}
