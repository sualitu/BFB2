namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board;
    using Assets.BattleForBetelgeuse.Interactable.Clickable;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class HexTileView : ClickableView {
        public HexTileView() {
            BoardStore.Instance.Subscribe(CheckSelected);
        }

        public Color Color { get; set; }
        internal HexCoordinate Coordinate { get; set; }

        public override void LeftClicked() {
            //new HexTileClickedAction(Coordinate);

            new CardDrawnAction();
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