namespace Assets.Elements.GUI.Grid.HexTile {
    using Assets.Flux.Interactable.Clickable;
    using Assets.Flux.Interactable.MouseInteraction;
    using Assets.GameManagement;
    
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
            BehaviourManager.Behaviours.Add(this);
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