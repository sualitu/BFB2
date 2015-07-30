namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.Flux.Actions;

    public class MouseOverHexAction : HexTileAction {
        public MouseOverHexAction(HexCoordinate coordinate)
            : base(coordinate) {}
    }
}