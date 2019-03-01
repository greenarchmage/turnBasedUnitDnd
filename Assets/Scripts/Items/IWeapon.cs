using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
  public interface IWeapon
  {
    string Name { get; }
    int DamageDie { get; }
    int Range { get; }
  }
}
