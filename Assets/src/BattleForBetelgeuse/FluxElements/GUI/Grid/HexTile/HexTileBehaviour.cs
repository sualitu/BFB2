namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.BattleForBetelgeuse.Interactable.Clickable;
    using Assets.BattleForBetelgeuse.Interactable.MouseInteraction;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class HexTileBehaviour : ClickableViewBehaviour<HexTileView>, IMouseOverable {

        private Renderer rend;

        public HexCoordinate Coordinate { get; set; }

        public void MouseOver() {
            new MouseOverHexAction(Coordinate);
        }

        public void MouseOut() {
        }

        public void UpdateColor(Color color) {
            rend.material.SetColor("_Color", color);
        }

        private void Start() {
            BehaviourManager.Behaviours.Add(this);
            
            rend = GetComponent<Renderer>();
            gameObject.name = "Hex:" + UniqueId();
            gameObject.tag = "HexTile";
            Companion = new HexTileView();
            Companion.Coordinate = Coordinate;
        }

        public override void PushUpdate() {
            UpdateColor(Companion.Color);
        }
    }
}