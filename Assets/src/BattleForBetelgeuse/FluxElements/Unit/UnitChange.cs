namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Player;

    public class UnitChange {
        public HexCoordinate From { get; set; }
        public HexCoordinate To { get; set; }
        public HexCoordinate Attack { get; set; }
        public List<HexCoordinate> Path { get; set; }

        public bool NewUnitSpawned { get; set; }
    }
}