namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;
    using Assets.Utilities.PathFinding;

    public class BoardStore : PublishingStore<BoardStatus> {
        private static BoardStore instance;

        private readonly BoardStatus status;

        private BoardStore() {
            status = new BoardStatus();
        }

        public static BoardStore Instance {
            get {
                if (instance == null) {
                    instance = new BoardStore();
                }
                return instance;
            }
        }

        internal override void SendMessage(Message msg) {
            msg(status);
        }

        private void UpdateStatus(HexCoordinate coordinate) {
            if (coordinate != null && coordinate.Equals(status.CurrentSelection)) {
                return;
            }
            status.PreviousSelection = status.CurrentSelection;
            status.CurrentSelection = coordinate;
            if (status.PreviousSelection != null && status.CurrentSelection != null) {
                try {
                    status.Path = AStar<HexCoordinate>.FindPath(status.PreviousSelection, status.CurrentSelection);
                } catch {
                    status.Path = new List<HexCoordinate>();
                }
            }
        }

        public override void UpdateStore(Dispatchable action) {
            if (action is HexTileClickedAction) {
                var hexTileClickedAction = (HexTileClickedAction)action;
                UpdateStatus(hexTileClickedAction.Coordinate);
                Publish();
            } else if (action is RightClickAction) {
                Deselect();
            }
        }

        internal override void Publish() {
            new BoardUpdateAction(status.Copy());
            base.Publish();
        }

        public void Deselect() {
            UpdateStatus(null);
            Publish();
        }
    }
}