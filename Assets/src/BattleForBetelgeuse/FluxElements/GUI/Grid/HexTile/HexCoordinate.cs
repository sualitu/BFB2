namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BattleForBetelgeuse.Management;
    using Assets.Utilities.PathFinding;

    using UnityEngine;

    public class HexCoordinate : IPathable {
        public HexCoordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public IList<T> Neighbors<T>() {
            var xAdjustment = Y % 2 == 0 ? 0 : -1;
            var neighbors = new List<HexCoordinate> {
                new HexCoordinate(X - 1, Y),
                new HexCoordinate(X + 1, Y),
                new HexCoordinate(X + xAdjustment, Y - 1),
                new HexCoordinate(X + xAdjustment, Y + 1),
                new HexCoordinate(X + 1 + xAdjustment, Y - 1),
                new HexCoordinate(X + 1 + xAdjustment, Y + 1)
            };
            return neighbors.Cast<T>().ToList();
        }

        public bool IsMoveable() {
            return GridManager.Instance.MoveableHex(this);
        }

        public int EstimateCostTo(IPathable goal) {
            return EstimateCostTo((HexCoordinate)goal);
        }

        public int EstimateCostTo(HexCoordinate goal) {
            var deltaX = X - goal.X;
            var deltaY = Y - goal.Y;
            var d = Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2)));
            return d;
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != typeof(HexCoordinate)) {
                return false;
            }
            if (obj is HexCoordinate) {
                var other = (HexCoordinate)obj;
                return other.X == X && other.Y == Y;
            }
            return false;
        }

        public override int GetHashCode() {
            var hash = 7;
            hash = 71 * hash + X;
            hash = 71 * hash + Y;
            return hash;
        }

        public override string ToString() {
            return string.Format("[HexCoordinate: X={0}, Y={1}]", X, Y);
        }
    }
}