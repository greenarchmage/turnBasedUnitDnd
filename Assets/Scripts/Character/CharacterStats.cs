using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
  public class CharacterStats
  {
    public int Hitpoints { get; set; }
    public float MovementSpeed { get; set; }
    public int Damage { get; set; }

    public CharacterStats() { }

    public CharacterStats(int hitpoints, float movementspeed, int damage)
    {
      Hitpoints = hitpoints;
      MovementSpeed = movementspeed;
      Damage = damage;
    }
  }
}
