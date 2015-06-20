using BattleForBetelgeuse.GUI.Hex;
using System.Collections.Generic;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class UnitChange {
    public HexCoordinate From { get; set; }

    public HexCoordinate To { get; set; }

    public List<HexCoordinate> Path { get; set; }
  }
}
