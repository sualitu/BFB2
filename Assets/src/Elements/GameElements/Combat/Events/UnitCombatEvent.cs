namespace BattleForBetelgeuse.GameElements.Combat.Events {
    using BattleForBetelgeuse.GameElements.Units;
    using BattleForBetelgeuse.GUI.Hex;

    public class UnitCombatEvent : CombatEvent {
        public UnitCombatEvent(long time)
            : base(time) {}

        public Unit Attacker { get; set; }
        public HexCoordinate AttackerLocation { get; set; }
        public Unit Defender { get; set; }
        public HexCoordinate DefenderLocation { get; set; }
    }
}