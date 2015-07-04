namespace Assets.Utilities.PathFinding {
    using System.Collections.Generic;

    public interface IPathable {
        IList<T> Neighbors<T>();

        bool IsMoveable();

        int EstimateCostTo(IPathable goal);
    }
}