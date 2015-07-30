namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board;
    using Assets.BattleForBetelgeuse.Interactable.Clickable;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class HexTileView : ClickableView {
        private bool selected;

        public Color Color { get; set; }
        internal HexCoordinate Coordinate { get; set; }

        private void CheckMousedOver(MouseOverStatus status) {
            if (!selected) {
                if (Coordinate.Equals(status.CurrentMouseOver)) {
                    Color = Color.red;
                } else if (Coordinate.Equals(status.PreviousMouseOver)) {
                    Color = Settings.ColorSettings.TileBaseColor;
                } else {
                    return;
                }
                UpdateBehaviour();
            }
        }

        public override void LeftClicked() {
            new HexTileClickedAction(Coordinate);
        }

        public void CheckSelected(BoardStatus status) {
            if (Coordinate.Equals(status.CurrentSelection)) {
                Color = Color.green;
                selected = true;
            } else if (Coordinate.Equals(status.PreviousSelection)) {
                selected = false;
                Color = Settings.ColorSettings.TileBaseColor;
            } else {
                return;
            }
            UpdateBehaviour();
        }

        public override void SetupSubscriptions() {
            MouseStore.Instance.Subscribe(guid, CheckMousedOver);
            BoardStore.Instance.Subscribe(guid, CheckSelected);
        }
    }
}