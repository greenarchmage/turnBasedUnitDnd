using Assets.Scripts.Items;
using UnityEngine;

namespace Assets.Scripts.Character
{
  public class CharacterData
  {
    public int Hitpoints { get; private set; }
    public float MovementSpeed { get; set; }
    
    public int CurrentHitpoints { get; set; }
    public bool IsDead { get { return CurrentHitpoints <= 0; } }

    #region Equipment
    public IWeapon EquippedWeapon { get; set; }
    public Armor EquippedArmor { get; set; }
    private const int BaseAC = 10;
    public int CurrentAC { get { return BaseAC + (EquippedArmor != null ? EquippedArmor.ACBonus : 0); } }
    public int CurrentPercentileAC { get { return CurrentAC * 5; } }
    #endregion

    public CharacterData() { }

    public CharacterData(int hitpoints, float movementspeed)
    {
      Hitpoints = hitpoints;
      CurrentHitpoints = hitpoints;
      MovementSpeed = movementspeed;
    }

    /// <summary>
    /// Change the hitpoints of the character.
    /// Clamped between 0 and the max hitpoints of the character
    /// </summary>
    /// <param name="change">The amount to change the hitpoints, positive add, negative subtracts</param>
    /// <returns></returns>
    public void ChangeHitpoints(int change)
    {
      CurrentHitpoints += change;
      CurrentHitpoints = Mathf.Clamp(CurrentHitpoints, 0, Hitpoints);
    }
  }
}
