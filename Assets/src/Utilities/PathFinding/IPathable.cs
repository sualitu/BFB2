using System.Collections.Generic;

namespace BattleForBetelgeuse.PathFinding {

  public interface IPathable {

    IList<T> Neighbors<T>();

    bool IsMoveable();

    int EstimateCostTo(IPathable goal);
  }
}

