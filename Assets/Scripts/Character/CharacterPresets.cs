using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
  public class CharacterPresets
  {
    public static CharacterData CreateWarrior()
    {
      return new CharacterData(10, 10) { EquippedWeapon = ItemFactory.CreateSpear() };
    }
  }
}
