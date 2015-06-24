using BattleForBetelgeuse.View;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Interactable;
using UnityEngine;
using System.Collections.Generic;
using BattleForBetelgeuse.GameElements.Combat;
using BattleForBetelgeuse.GameElements.Combat.Events;

namespace BattleForBetelgeuse.GameElements.Units {

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

    public void Combat(CombatLog log) {
      Debug.Log("CombatLog recieved");
      while(log.MoveNext()) {
        Debug.Log("CombatEvent recieved");
        var current = log.Current;
        if(current is UnitCombatEvent) {
          var unitCombatEvent = current as UnitCombatEvent;
          if(unitCombatEvent.AttackerLocation.Equals(Coordinate)) {
            Debug.Log("I'm attacking!");
          } else if(unitCombatEvent.DefenderLocation.Equals(Coordinate)) {
            Debug.Log("I'm defending...");
          }
        }
      }
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

