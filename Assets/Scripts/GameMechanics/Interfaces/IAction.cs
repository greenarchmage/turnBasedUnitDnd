
using Assets.Scripts.GameMechanics.Classes;

namespace Assets.Scripts.GameMechanics.Interfaces
{
  public interface IAction
  {
    string Name { get; set; }
    string Description { get; set; }
    void ExecuteAction(TargetObject target);
  }
}
