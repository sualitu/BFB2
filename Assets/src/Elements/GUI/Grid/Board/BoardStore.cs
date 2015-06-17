using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.PathFinding;
using BattleForBetelgeuse.Actions;

using System.Collections.Generic;
using BattleForBetelgeuse.GUI.Hex;
using UnityEngine;

namespace BattleForBetelgeuse.GUI.Board {

  public class BoardStore : PublishingStore<BoardStatus> {
    private static BoardStore instance;
        
    public static BoardStore Instance { 
      get {
        if(instance == null) {
          instance = new BoardStore();
        }
        return instance;
      } 
    }

    private BoardStore() : base() {
      status = new BoardStatus();
    }
    
    BoardStatus status;

    internal override void SendMessage(Message msg) {
      msg(status);
    }

    void UpdateStatus(HexCoordinate coordinate) {
      if(coordinate != null && coordinate.Equals(status.CurrentSelection)) {
        return;
      }
      status.PreviousSelection = status.CurrentSelection;
      status.CurrentSelection = coordinate;
      if(status.PreviousSelection != null && status.CurrentSelection != null) {
        try {
          status.Path = AStar<HexCoordinate>.FindPath(status.PreviousSelection, status.CurrentSelection);
        } catch {
          status.Path = new List<HexCoordinate>();
        }
      }
    }
    
    public override void Update(Dispatchable action) {
      if(action is HexTileClickedAction) {
        var hexTileClickedAction = (HexTileClickedAction)action;
        UpdateStatus(hexTileClickedAction.Coordinate);
        Publish();
      } else if(action is RightClickAction) {
        Deselect();
      }
    }

    internal override void Publish() { 
      new BoardUpdateAction(status.Copy());
      base.Publish();
    }

    public void Deselect() {
      UpdateStatus(null);
      Publish();
    }
  }
}

