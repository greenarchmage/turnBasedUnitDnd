using UnityEngine;

namespace Assets.Scripts.GameMechanics
{
  public static class GameCheck
  {
    public static bool PercentileCheck(int target, int modifier = 0)
    {
      int result = Random.Range(1, 100) + modifier;
      Debug.Log(string.Format("Percentile check with roll: '{0}' and target: '{1}'.", result, target));
      return result >= target; // GTE means that "attacker" wins.
    }
  }
}
