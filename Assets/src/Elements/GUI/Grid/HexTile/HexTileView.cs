namespace BattleForBetelgeuse.GUI.Hex {
    using BattleForBetelgeuse.GUI.Board;
    using BattleForBetelgeuse.View.Clickable;

    using UnityEngine;

    public class HexTileView : ClickableView {
        public HexTileView() {
            BoardStore.Instance.Subscribe(CheckSelected);
        }

        public Color Color { get; set; }
        internal HexCoordinate Coordinate { get; set; }

        public override void LeftClicked() {
            new HexTileClickedAction(Coordinate);
        }

        public void CheckSelected(BoardStatus status) {
            if (Coordinate.Equals(status.CurrentSelection)) {
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