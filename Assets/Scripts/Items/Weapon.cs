using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Items
{
  public class Weapon
  {
    public int DamageDie { get; set; }
    public int Range { get; set; }

    public static Weapon CreateSpear()
    {
      return new Weapon() { DamageDie = 6, Range = 1 };
    }
  }
}
