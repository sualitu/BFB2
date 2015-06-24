using BattleForBetelgeuse.GUI.Hex;
using System.Collections.Generic;

namespace BattleForBetelgeuse.GameElements.Units {

  public class UnitChange {
    public HexCoordinate From { get; set; }

    public HexCoordinate To { get; set; }

    public HexCoordinate Attack { get; set; }

    public List<HexCoordinate> Path { get; set; }
  }
}
