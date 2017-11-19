using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utility;

public class CubeChangeEventArgs : EventArgs
{
  public CubeType NewCubeType { get { return newCubeType; } }
  private CubeType newCubeType;

  public CubeChangeEventArgs(CubeType newType)
  {
    newCubeType = newType;
  }
}
