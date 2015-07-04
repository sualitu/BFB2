namespace Assets.Elements.GameElements.Combat.Events {
    using Assets.Elements.GameElements.Unit;
    using Assets.Elements.GUI.Grid.HexTile;

    public class UnitCombatEvent : CombatEvent {
        public UnitCombatEvent(long time)
            : base(time) {}

        public Unit Attacker { get; set; }
        public HexCoordinate AttackerLocation { get; set; }
        public Unit Defender { get; set; }
        public HexCoordinate DefenderLocation { get; set; }
    }
}