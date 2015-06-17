using BattleForBetelgeuse.View.Clickable;
using BattleForBetelgeuse.Actions;
using UnityEngine;
using BattleForBetelgeuse.GUI.Board;

namespace BattleForBetelgeuse.GUI.Hex {

  public class HexTileView : ClickableView {

    public Color Color { get; set; }

    internal HexCoordinate Coordinate { get; set; }

    public HexTileView() {
      BoardStore.Instance.Subscribe(CheckSelected);
    }

    public override void LeftClicked() {
      new HexTileClickedAction(Coordinate);
    }

   public void CheckSelected(BoardStatus status) {
      if(Coordinate.Equals(status.CurrentSelection)) {
        Color = Color.green;
      } else if (Coordinate.Equals(status.PreviousSelection)) {
        Color = Settings.ColorSettings.TileBaseColor;
      } else {
        return;
      }
      UpdateBehaviour();
    }
  }

}

