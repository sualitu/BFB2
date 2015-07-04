namespace Assets.BattleForBetelgeuse.FluxElements.Combat.Events {
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;

    public class UnitCombatEvent : CombatEvent {
        public UnitCombatEvent(long time)
            : base(time) {}

        public Unit Attacker { get; set; }
        public HexCoordinate AttackerLocation { get; set; }
        public Unit Defender { get; set; }
        public HexCoordinate DefenderLocation { get; set; }
    }
}