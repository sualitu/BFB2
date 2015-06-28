namespace BattleForBetelgeuse.GameElements.Units {
    using System.Collections.Generic;

    using BattleForBetelgeuse.GUI.Hex;

    public class UnitChange {
        public HexCoordinate From { get; set; }
        public HexCoordinate To { get; set; }
        public HexCoordinate Attack { get; set; }
        public List<HexCoordinate> Path { get; set; }
    }
}