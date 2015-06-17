using System.Collections.Generic;
using BattleForBetelgeuse.GUI.Hex;


namespace BattleForBetelgeuse.GUI.Board {

  public class BoardStatus {
    public HexCoordinate CurrentSelection { get; set; }

    public HexCoordinate PreviousSelection { get; set; }

    List<HexCoordinate> path = new List<HexCoordinate>();

    public List<HexCoordinate> Path { 
      get { return path; }
      set { path = value; }
    }

    public override string ToString() {
      return string.Format("[BoardStatus: CurrentSelection={0}, PreviousSelection={1}]", CurrentSelection, PreviousSelection);
    }

    public BoardStatus Copy() {
      return new BoardStatus {
        CurrentSelection = CurrentSelection,
        PreviousSelection = PreviousSelection,
        Path = Path
      };
    }
    
  }
  
}
