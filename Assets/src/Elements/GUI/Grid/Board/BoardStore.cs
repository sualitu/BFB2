namespace BattleForBetelgeuse.GUI.Board {
    using System.Collections.Generic;

    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.PathFinding;
    using BattleForBetelgeuse.Stores;

    public class BoardStore : PublishingStore<BoardStatus> {
        private static BoardStore instance;

        private readonly BoardStatus status;

        private BoardStore() {
            this.status = new BoardStatus();
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
            msg(this.status);
        }

        private void UpdateStatus(HexCoordinate coordinate) {
            if (coordinate != null && coordinate.Equals(this.status.CurrentSelection)) {
                return;
            }
            this.status.PreviousSelection = this.status.CurrentSelection;
            this.status.CurrentSelection = coordinate;
            if (this.status.PreviousSelection != null && this.status.CurrentSelection != null) {
                try {
                    this.status.Path = AStar<HexCoordinate>.FindPath(this.status.PreviousSelection,
                                                                     this.status.CurrentSelection);
                } catch {
                    this.status.Path = new List<HexCoordinate>();
                }
            }
        }

        public override void Update(Dispatchable action) {
            if (action is HexTileClickedAction) {
                var hexTileClickedAction = (HexTileClickedAction)action;
                this.UpdateStatus(hexTileClickedAction.Coordinate);
                this.Publish();
            } else if (action is RightClickAction) {
                this.Deselect();
            }
        }

        internal override void Publish() {
            new BoardUpdateAction(this.status.Copy());
            base.Publish();
        }

        public void Deselect() {
            this.UpdateStatus(null);
            this.Publish();
        }
    }
}