namespace Assets.Elements.GameElements.Unit {
    using System.Collections.Generic;

    using Assets.Elements.GUI.Grid.HexTile;
    using Assets.GameManagement;

    public class UnitChange {
        public HexCoordinate From { get; set; }
        public HexCoordinate To { get; set; }
        public HexCoordinate Attack { get; set; }
        public List<HexCoordinate> Path { get; set; }

        public Player Owner { get; set; }
    }
}