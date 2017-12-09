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

    public List<Character.Character> actors
    {
      get;
      set;
    }
    public Player()
    {
      actors = new List<Character.Character>();
    }
  }
}
