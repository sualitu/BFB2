using BattleForBetelgeuse.View;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Interactable;
using UnityEngine;
using System.Collections.Generic;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class UnitView : BehaviourUpdatingView {

    internal HexCoordinate Coordinate { get; set; }

    public List<HexCoordinate> Path { get; set; }

    internal bool Alive { get; set; }

    bool hasMoved = false;

    internal bool HasMoved {
      get {
        var old = hasMoved;
        hasMoved = false;
        return old;
      }
    }

    public UnitView(HexCoordinate coordinate) {
      Alive = true;
      Coordinate = coordinate;
      UnitStore.Instance.Subscribe(Move);
    }

    public void Move(List<UnitChange> changes) {
      foreach(var change in changes) {
        if(change.From.Equals(Coordinate)) {
          Coordinate = change.To;
          Path = change.Path;
          hasMoved = true;
          Alive = change.Unit.CurrentHealth() > 0;
        }
        UpdateBehaviour();
      }
    }
  }
}

