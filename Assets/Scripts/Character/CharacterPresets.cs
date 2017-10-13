using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
  public class CharacterPresets
  {
    public static CharacterStats CreateWarrior()
    {
      return new CharacterStats(10,10,2);
    }
  }
}
