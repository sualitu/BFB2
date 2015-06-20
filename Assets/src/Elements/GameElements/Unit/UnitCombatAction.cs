using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.GameElements.Unit {

  class UnitCombatAction : Dispatchable {
    HexCoordinate from;

    public HexCoordinate From {
      get {
        _readyToGo.WaitOne();
        return from;
      }
      private set {
        from = value;
      }
    }

    Unit attacker;

    public Unit Attacker {
      get {
        _readyToGo.WaitOne();
        return attacker;
      }
      private set {
        attacker = value;
      }
    }

    Unit defender;

    public Unit Defender {
      get {
        _readyToGo.WaitOne();
        return defender;
      }
      private set {
        defender = value;
      }
    }

    public UnitCombatAction(HexCoordinate from, Unit attacker, Unit defender) {
      From = from;
      Attacker = attacker;
      Defender = defender;
      _readyToGo.Set();
    }


  }


}

