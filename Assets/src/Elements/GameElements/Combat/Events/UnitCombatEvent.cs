using BattleForBetelgeuse.GameElements.Units;
using BattleForBetelgeuse.GUI.Hex;

namespace BattleForBetelgeuse.GameElements.Combat.Events {

  public class UnitCombatEvent : CombatEvent {

    public Unit Attacker { get; set; }

    public HexCoordinate AttackerLocation { get; set; }

    public Unit Defender { get; set; }

    public HexCoordinate DefenderLocation { get; set; }

    public UnitCombatEvent(UnitCombatAction action) : base(action.Invocation) {
      Attacker = action.Attacker;
      AttackerLocation = action.From;
      Defender = action.Defender;
      DefenderLocation = action.To;
    }
  }
}

