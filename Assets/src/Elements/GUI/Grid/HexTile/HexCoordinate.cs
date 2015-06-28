namespace BattleForBetelgeuse.GUI.Hex {
    using System.Collections.Generic;
    using System.Linq;

    using BattleForBetelgeuse.PathFinding;

    using UnityEngine;

    public class HexCoordinate : IPathable {
        public HexCoordinate(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public IList<IPathable> Neighbors<IPathable>() {
            var xAdjustment = this.Y % 2 == 0 ? 0 : -1;
            var neighbors = new List<HexCoordinate> {
                new HexCoordinate(this.X - 1, this.Y),
                new HexCoordinate(this.X + 1, this.Y),
                new HexCoordinate(this.X + xAdjustment, this.Y - 1),
                new HexCoordinate(this.X + xAdjustment, this.Y + 1),
                new HexCoordinate(this.X + 1 + xAdjustment, this.Y - 1),
                new HexCoordinate(this.X + 1 + xAdjustment, this.Y + 1)
            };
            return neighbors.Cast<IPathable>().ToList();
        }

        public bool IsMoveable() {
            return GridManager.Instance.MoveableHex(this);
        }

        public int EstimateCostTo(IPathable goal) {
            return this.EstimateCostTo((HexCoordinate)goal);
        }

        public int EstimateCostTo(HexCoordinate goal) {
            var deltaX = this.X - goal.X;
            var deltaY = this.Y - goal.Y;
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
                return other.X == this.X && other.Y == this.Y;
            }
            return false;
        }

        public override int GetHashCode() {
            var hash = 7;
            hash = 71 * hash + this.X;
            hash = 71 * hash + this.Y;
            return hash;
        }

        public override string ToString() {
            return string.Format("[HexCoordinate: X={0}, Y={1}]", this.X, this.Y);
        }
    }
}