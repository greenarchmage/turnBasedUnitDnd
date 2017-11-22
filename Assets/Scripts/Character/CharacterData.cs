using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
  public class CharacterData
  {
    public int Hitpoints { get; private set; }
    public float MovementSpeed { get; set; }
    
    public int CurrentHitpoints { get; set; }
    public Weapon EquipedWeapon { get; set; }

    public CharacterData() { }

    public CharacterData(int hitpoints, float movementspeed)
    {
      Hitpoints = hitpoints;
      CurrentHitpoints = hitpoints;
      MovementSpeed = movementspeed;
    }
  }
}
