using BattleForBetelgeuse.View;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Interactable;
using UnityEngine;
using System.Collections.Generic;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class UnitView : BehaviourUpdatingView {

    internal HexCoordinate Coordinate { get; set; }

    public List<HexCoordinate> Path { get; set; }

    public HexCoordinate AttackTarget { get; set; }

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
      CombatStore.Instance.Subscribe(Combat);
    }

    public void Combat(IEnumerable<CombatEvent> log) {
    }

    public void Move(List<UnitChange> changes) {
      foreach(var change in changes) {
        if(change.From.Equals(Coordinate)) {
          if(change.To != null) {
            Coordinate = change.To;
            Path = change.Path;
            hasMoved = true;
          }
          if(change.Attack != null) {
            AttackTarget = change.Attack;
          } else {
            AttackTarget = null;
          }
        }
        UpdateBehaviour();
      }
    }
  }
}

