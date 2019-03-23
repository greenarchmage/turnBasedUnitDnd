
using Assets.Scripts.GameMechanics.Classes;
using Assets.Scripts.GameMechanics.Interfaces;
using System;

namespace Assets.Scripts.GameMechanics.Actions
{
  public class SimpleSpell : IAction
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public void ExecuteAction(TargetObject target)
    {
      Random ran = new Random();
      int damageRolls = ran.Next(4) + ran.Next(4) + ran.Next(4) + 3;
      target.TargetCharacter.HitpointsCurrent -= damageRolls + 3;
    }
  }
}
