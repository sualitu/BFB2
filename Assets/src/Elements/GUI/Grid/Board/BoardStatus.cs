namespace BattleForBetelgeuse.GUI.Board {
    using System.Collections.Generic;

    using BattleForBetelgeuse.GUI.Hex;

    public class BoardStatus {
        private List<HexCoordinate> path = new List<HexCoordinate>();

        public HexCoordinate CurrentSelection { get; set; }
        public HexCoordinate PreviousSelection { get; set; }

        public List<HexCoordinate> Path {
            get {
                return this.path;
            }
            set {
                this.path = value;
            }
        }

        public override string ToString() {
            return string.Format("[BoardStatus: CurrentSelection={0}, PreviousSelection={1}]",
                                 this.CurrentSelection,
                                 this.PreviousSelection);
        }

        public BoardStatus Copy() {
            return new BoardStatus {
                CurrentSelection = this.CurrentSelection,
                PreviousSelection = this.PreviousSelection,
                Path = this.Path
            };
        }
    }
}