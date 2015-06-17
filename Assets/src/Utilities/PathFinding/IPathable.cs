using System.Collections.Generic;

namespace BattleForBetelgeuse.PathFinding {

  public interface IPathable {

    IList<T> Neighbors<T>();

    int EstimateCostTo(IPathable goal);
  }
}

