using Assets.Scripts.GameMechanics.Actions;
using Assets.Scripts.GameMechanics.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.GameMechanics.Classes
{
  public enum CharacterType
  {
    Soldier, Archer, Unique
  }

  public class Character
  {
    public string Name { get; set; }
    public CharacterType Type { get; set; }
    public int ArmorClass { get; set; }
    public int Speed { get; set; }
    public int Initiative { get; set; }
    public int HitpointsMaximum { get; set; }
    public int HitpointsCurrent { get; set; }
    public List<IAction> Actions { get; set; }
    public Character(CharacterType type)
    {
      switch (type)
      {
        case CharacterType.Soldier:
          // create a soldier class
          ArmorClass = 16;
          Speed = 30;
          Initiative = 1;
          HitpointsMaximum = 16;
          HitpointsCurrent = HitpointsMaximum;
          Actions = new List<IAction>()
        { new SimpleAttack() { DiceSides = 6, AttackBonus = 4, DamageBonus = 2, Name = "Spear Attack", Description = "Melee attack with a spear" },
          new SimpleAttack() { DiceSides = 6, AttackBonus = 4, DamageBonus = 2, Name = "Javelin Attack", Description = "Ranged attack with a javelin" },};
          Type = type;
          Name = type.ToString();
          break;
        case CharacterType.Archer:
          // create an archer class
          ArmorClass = 14;
          Speed = 30;
          Initiative = 2;
          HitpointsMaximum = 16;
          HitpointsCurrent = HitpointsMaximum;
          Actions = new List<IAction>()
        { new SimpleAttack() { DiceSides = 8, AttackBonus = 4, DamageBonus = 2, Name = "Longbow Attack", Description = "Ranged attack with a longbow" } };
          Type = type;
          Name = type.ToString();
          break;
        case CharacterType.Unique:
        //throw new Exception("Cannot create an unique character without specific data");
        default:
          break;
      }
    }

    public Character(int armorclass, int speed, int initiative, int hitpointMax, string name)
    {
      ArmorClass = armorclass;
      Speed = speed;
      Initiative = initiative;
      HitpointsMaximum = hitpointMax;
      HitpointsCurrent = HitpointsMaximum;
      Type = CharacterType.Unique;
      Name = name;
    }
  }
}

