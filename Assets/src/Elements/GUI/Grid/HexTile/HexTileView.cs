namespace BattleForBetelgeuse.GUI.Hex {
    using BattleForBetelgeuse.GUI.Board;
    using BattleForBetelgeuse.View.Clickable;

    using UnityEngine;

    public class HexTileView : ClickableView {
        public HexTileView() {
            BoardStore.Instance.Subscribe(this.CheckSelected);
        }

        public Color Color { get; set; }
        internal HexCoordinate Coordinate { get; set; }

        public override void LeftClicked() {
            new HexTileClickedAction(this.Coordinate);
        }

        public void CheckSelected(BoardStatus status) {
            if (this.Coordinate.Equals(status.CurrentSelection)) {
                this.Color = Color.green;
            } else if (this.Coordinate.Equals(status.PreviousSelection)) {
                this.Color = Settings.ColorSettings.TileBaseColor;
            } else {
                return;
            }
            this.UpdateBehaviour();
        }
    }
}