namespace BattleForBetelgeuse.GUI.Hex {
    using BattleForBetelgeuse.Actions;

    public class HexTileClickedAction : UnpausableAction {
        private HexCoordinate hexCoord;

        public HexTileClickedAction(HexCoordinate coordinate) {
            this.Coordinate = coordinate;
        }

        public HexCoordinate Coordinate {
            get {
                this._readyToGo.WaitOne();
                return this.hexCoord;
            }
            private set {
                this._readyToGo.Set();
                this.hexCoord = value;
            }
        }
    }
}