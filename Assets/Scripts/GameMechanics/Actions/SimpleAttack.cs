using Assets.Scripts.GameMechanics.Classes;
using Assets.Scripts.GameMechanics.Interfaces;
using System;

namespace Assets.Scripts.GameMechanics.Actions
{
  public class SimpleAttack : IAction
  {
    public int AttackBonus { get; set; }
    public int DiceSides { get; set; }
    public int DamageBonus { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void ExecuteAction(TargetObject target)
    {
      Random ran = new Random();
      if (ran.Next(20) + 1 + AttackBonus >= target.TargetCharacter.ArmorClass)
      {
        target.TargetCharacter.HitpointsCurrent -= ran.Next(DiceSides) + 1 + DamageBonus;
      }
    }
  }
}
