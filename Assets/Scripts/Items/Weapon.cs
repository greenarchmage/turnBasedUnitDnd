using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Items
{
  public class Weapon : IWeapon
  {
    public static readonly Weapon Fists = new Weapon() { Name = "Fists", DamageDie = 2, Range = 1 };

    public string Name { get; set; }
    public int DamageDie { get; set; }
    public int Range { get; set; }

  }
}
