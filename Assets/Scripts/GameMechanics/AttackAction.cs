using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.GameMechanics
{
  public class AttackAction
  {
    public ICharacter Source { get; set; }
    public ICharacter Target { get; set; }

    public void ResolveAction()
    {
      int targetAC = Target.Stats.CurrentPercentileAC;
      int attackRoll = Random.Range(1, 20) * 5;
      if (GameCheck.PercentileCheck(targetAC)) {
        int damageRoll = Random.Range(1, Source.Stats.EquippedWeapon.DamageDie) * -1;
        Debug.Log(string.Format("Dealt {0} damage to target.", damageRoll));
        Target.Stats.ChangeHitpoints(damageRoll);
      }
    }
  }
}
