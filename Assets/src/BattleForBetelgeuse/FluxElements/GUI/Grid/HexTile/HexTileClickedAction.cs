namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.Flux.Actions;

    public class HexTileClickedAction : UnpausableAction {
        private HexCoordinate hexCoord;

        public HexTileClickedAction(HexCoordinate coordinate) {
            Coordinate = coordinate;
        }

        public HexCoordinate Coordinate {
            get {
                _readyToGo.WaitOne();
                return hexCoord;
            }
            private set {
                _readyToGo.Set();
                hexCoord = value;
            }
        }
    }
}