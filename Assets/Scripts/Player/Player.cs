using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Character;

namespace Assets.Scripts
{
  public class Player
  {
    public string Name { get; set; }
    public Character.ICharacter Character { get; set; }

    public List<ICharacter> Actors { get; set; }

    public Player()
    {
      Actors = new List<ICharacter>();
    }
  }
}
