using BattleForBetelgeuse.GameElements.Units;
using BattleForBetelgeuse.GUI.Hex;

namespace BattleForBetelgeuse.GameElements.Combat.Events {

  public class UnitCombatEvent : CombatEvent {

    public Unit Attacker { get; set; }

    public HexCoordinate AttackerLocation { get; set; }

    public Unit Defender { get; set; }

    public HexCoordinate DefenderLocation { get; set; }

    public UnitCombatEvent(long time) : base(time) {
      
    }
  }
}

