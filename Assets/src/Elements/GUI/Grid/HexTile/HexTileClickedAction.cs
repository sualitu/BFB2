using BattleForBetelgeuse.Actions;
using System.Threading;

namespace BattleForBetelgeuse.GUI.Hex {

  public class HexTileClickedAction : ClickAction {

    HexCoordinate hexCoord;

    public HexCoordinate Coordinate { 
      get {
        _ReadyToGo.WaitOne();
        return hexCoord;
      }
      private set {
        _ReadyToGo.Set();
        hexCoord = value;
      }
    }

    public HexTileClickedAction(Click click, HexCoordinate coordinate) : base(click) {

      Coordinate = coordinate;
    }    
  }
}