using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
  public static class ItemFactory
  {
    public static IWeapon CreateWeapon(string name)
    {
      switch (name) {
        case "Spear": return CreateSpear();
        default: return Weapon.Fists;
      }
    }

    public static IWeapon CreateSpear() {  return new Weapon() { Name = "Spear", DamageDie = 6, Range = 1 }; }
  }
}
