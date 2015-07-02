namespace BattleForBetelgeuse.GUI.Hex {
    using BattleForBetelgeuse.Interactable.MouseInteraction;
    using BattleForBetelgeuse.View.Clickable;

    using UnityEngine;

    public class HexTileBehaviour : ClickableViewBehaviour<HexTileView>, IMouseOverable {
        private Color NonTempColor = Settings.ColorSettings.TileBaseColor;

        private Renderer rend;

        public HexCoordinate Coordinate { get; set; }

        public void MouseOver() {
            ColorTemporarily(Color.red);
        }

        public void MouseOut() {
            UpdateColor(NonTempColor);
        }

        public void UpdateColor(Color color) {
            NonTempColor = color;
            rend.material.SetColor("_Color", color);
        }

        public void ColorTemporarily(Color color) {
            rend.material.SetColor("_Color", color);
        }

        private void Start() {
            BehaviourUpdater.Behaviours.Add(this);
            Companion = new HexTileView();
            Companion.Coordinate = Coordinate;
            rend = GetComponent<Renderer>();
            gameObject.name = "Hex:" + UniqueId();
            gameObject.tag = "HexTile";
        }

        public override void PushUpdate() {
            UpdateColor(Companion.Color);
        }
    }
}