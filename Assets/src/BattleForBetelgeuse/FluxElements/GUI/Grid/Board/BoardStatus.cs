namespace Assets.Elements.GUI.Grid.Board {
    using System.Collections.Generic;

    using Assets.Elements.GUI.Grid.HexTile;

    public class BoardStatus {
        private List<HexCoordinate> path = new List<HexCoordinate>();

        public HexCoordinate CurrentSelection { get; set; }
        public HexCoordinate PreviousSelection { get; set; }

        public List<HexCoordinate> Path {
            get {
                return path;
            }
            set {
                path = value;
            }
        }

        public override string ToString() {
            return string.Format("[BoardStatus: CurrentSelection={0}, PreviousSelection={1}]",
                                 CurrentSelection,
                                 PreviousSelection);
        }

        public BoardStatus Copy() {
            return new BoardStatus {
                CurrentSelection = CurrentSelection,
                PreviousSelection = PreviousSelection,
                Path = Path
            };
        }
    }
}