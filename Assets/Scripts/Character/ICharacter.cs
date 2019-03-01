using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
  public interface ICharacter
  {
    CharacterData Stats { get; set; }
  }
}
