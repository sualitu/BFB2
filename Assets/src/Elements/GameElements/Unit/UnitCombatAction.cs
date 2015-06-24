using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.GameElements.Units {

  public class UnitCombatAction : Dispatchable {
    HexCoordinate from;

    public HexCoordinate From {
      get {
        return from;
      }
      private set {
        from = value;
      }
    }
    HexCoordinate to;
    
    public HexCoordinate To {
      get {
        return to;
      }
      private set {
        to = value;
      }
    }
    Unit attacker;

    public Unit Attacker {
      get {
        return attacker;
      }
      private set {
        attacker = value;
      }
    }

    Unit defender;

    public Unit Defender {
      get {
        return defender;
      }
      private set {
        defender = value;
      }
    }

    public void Wait() {
      _readyToGo.WaitOne();
    }

    public UnitCombatAction(HexCoordinate from, HexCoordinate to, Unit attacker, Unit defender) {
      From = from;
      To = to;
      Attacker = attacker;
      Defender = defender;
      _readyToGo.Set();
    }

    public override string ToString() {
      return string.Format("[UnitCombatAction: from={0}]", from);
    }
    
  }


}

