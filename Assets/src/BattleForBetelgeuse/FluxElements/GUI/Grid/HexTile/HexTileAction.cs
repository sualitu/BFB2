namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile {
    using Assets.Flux.Actions;

    public abstract class HexTileAction : UnpausableAction {
        private HexCoordinate hexCoord;

        protected HexTileAction(HexCoordinate coordinate) {
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