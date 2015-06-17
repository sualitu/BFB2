using BattleForBetelgeuse.View.Clickable;
using BattleForBetelgeuse.Actions;
using UnityEngine;
using BattleForBetelgeuse.PathFinding;
using System.Collections.Generic;
using System.Linq;

namespace BattleForBetelgeuse.GUI.Hex {

  public class HexCoordinate : IPathable {

    public int X { get; private set; }

    public int Y { get; private set; }

    public HexCoordinate(int x, int y) {
      X = x;
      Y = y;
    }

    public IList<IPathable> Neighbors<IPathable>() {
      var xAdjustment = Y%2 == 0 ? 0 : -1;
      var neighbors = new List<HexCoordinate>() {
        new HexCoordinate(X-1, Y),
        new HexCoordinate(X+1, Y),
        new HexCoordinate(X+xAdjustment, Y-1),
        new HexCoordinate(X+xAdjustment, Y+1),
        new HexCoordinate(X+1+xAdjustment, Y-1),
        new HexCoordinate(X+1+xAdjustment, Y+1),
      };
      return neighbors.Where(hex => GridManager.Instance.MoveableHex(hex)).Cast<IPathable>().ToList();
    }

    public int EstimateCostTo(HexCoordinate goal) {
      var deltaX = this.X - goal.X;
      var deltaY = this.Y - goal.Y;
      var d = Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2)));
      return d;
    }

    public int EstimateCostTo(IPathable goal) {
      return EstimateCostTo((HexCoordinate) goal);
    }

    public override bool Equals(object obj) {
      if(obj == null)
        return false;
      if(ReferenceEquals(this, obj))
        return true;
      if(obj.GetType() != typeof(HexCoordinate))
        return false;
      if(obj is HexCoordinate) {
        var other = (HexCoordinate) obj;
        return other.X == this.X && other.Y == this.Y;
      }
      return false;
    }

    public override int GetHashCode() {
      int hash = 7;
      hash = 71 * hash + X;
      hash = 71 * hash + Y;
      return hash;
    }

    public override string ToString() {
      return string.Format("[HexCoordinate: X={0}, Y={1}]", X, Y);
    }
    
  }
}
