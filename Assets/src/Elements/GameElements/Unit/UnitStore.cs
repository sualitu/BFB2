using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Cards.UnitCards;
using BattleForBetelgeuse.Utilities;
using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.GameElements.Cards;

using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using BattleForBetelgeuse.Dispatching;
using BattleForBetelgeuse.GUI.Board;
using BattleForBetelgeuse.Actions.DispatcherActions;
using ExtensionMethods;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class UnitStore : PublishingStore<List<UnitChange>> {

    private Dictionary<HexCoordinate, Unit> units;
    private static UnitStore instance;
        
    public static UnitStore Instance { 
      get {
        if(instance == null) {
          instance = new UnitStore();
        }
        return instance;
      } 
    }

    private UnitStore() : base() {
      units = new Dictionary<HexCoordinate, Unit>();
    }

    List<UnitChange> changes = new List<UnitChange>();

    void BoardUpdate(BoardStatus status) {
      if(status.PreviousSelection == null || status.CurrentSelection == null) {
        return;
      }
      if(units.ContainsKey(status.PreviousSelection)) {
        MoveUnit(status.PreviousSelection, status.CurrentSelection, units[status.PreviousSelection], status.Path);
      }
      Publish();
    }

    void UpdateUnitLocation(HexCoordinate oldLocation, HexCoordinate newLocation) {
      var unit = units[oldLocation];
      units.Remove(oldLocation);
      units.Add(newLocation, unit);
    }

    void MoveUnit(HexCoordinate from, HexCoordinate to, Unit unit, List<HexCoordinate> path) {
      if(IsUnitAtTile(to)) {
        UnitCollision(from, to, path);
      } else {
        UpdateUnitLocation(from, to);
        changes.Add(new UnitChange { From = from, To = to, Path = path });        
      }
    }

    public void UnitCollision(HexCoordinate from, HexCoordinate to, List<HexCoordinate> path) { 
      if(path.Count > 0) {
        path.RemoveFirst();
        if(path.Count > 1) {
          var moveTo = path.GetFirst();
          UpdateUnitLocation(from, moveTo);
          changes.Add(new UnitChange { From = from, To = moveTo, Path = path });
          from = moveTo;
        }
      }
      new UnitCombatAction(from, units[from], units[to]);
    }

    public bool IsUnitAtTile(HexCoordinate tile) {
      return units.ContainsKey(tile);
    }

    internal override void SendMessage(Message msg) {
      msg(changes);
    }

    internal override void Publish() {
      lock(changes) {
        base.Publish();
        changes = new List<UnitChange>();
      }
    }

    void UnitPlayed(HexCoordinate coordinate, UnitCard card) {
      UnitHandler.unitsToCreate.Add(new Tuple<HexCoordinate, string>(coordinate, card.PrefabPath));
    }

    public void HandleAction(Dispatchable action) {
      if(action is UnitCardPlayedAction) {
        var unitCardPlayedAction = (UnitCardPlayedAction)action;
        UnitPlayed(unitCardPlayedAction.Location, unitCardPlayedAction.Card);
        units.Add(unitCardPlayedAction.Location, (Unit.FromCard(unitCardPlayedAction.Card)));
      } else if(action is BoardUpdateAction) {
        var boardUpdateAction = (BoardUpdateAction)action;
        BoardUpdate(boardUpdateAction.BoardStatus);
      }
    }

    public override void Update(Dispatchable action) {
      HandleAction(action);
    }

    public static UnitStore Init() {
      return Instance;
    }
  }

}

