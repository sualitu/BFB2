namespace BattleForBetelgeuse.GUI.Hex {
    using BattleForBetelgeuse.Interactable.MouseInteraction;
    using BattleForBetelgeuse.View.Clickable;

    using UnityEngine;

    public class HexTileBehaviour : ClickableViewBehaviour<HexTileView>, IMouseOverable {
        private Color NonTempColor = Settings.ColorSettings.TileBaseColor;

        private Renderer rend;

        public HexCoordinate Coordinate { get; set; }

        public void MouseOver() {
            this.ColorTemporarily(Color.red);
        }

        public void MouseOut() {
            this.UpdateColor(this.NonTempColor);
        }

        public void UpdateColor(Color color) {
            this.NonTempColor = color;
            this.rend.material.SetColor("_Color", color);
        }

        public void ColorTemporarily(Color color) {
            this.rend.material.SetColor("_Color", color);
        }

        private void Start() {
            BehaviourUpdater.Behaviours.Add(this);
            this.Companion = new HexTileView();
            this.Companion.Coordinate = this.Coordinate;
            this.rend = this.GetComponent<Renderer>();
            this.gameObject.name = "Hex:" + this.UniqueId();
            this.gameObject.tag = "HexTile";
        }

        public override void PushUpdate() {
            this.UpdateColor(this.Companion.Color);
        }
    }
}