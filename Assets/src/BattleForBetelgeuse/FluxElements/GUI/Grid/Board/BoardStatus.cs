namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;

    public class BoardStatus {
        private List<HexCoordinate> path = new List<HexCoordinate>();
        public Guid? CardOnBoard { get; set; }
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
            return string.Format("CurrentSelection: {0}, PreviousSelection: {1}", CurrentSelection, PreviousSelection);
        }

        public BoardStatus Copy() {
            return new BoardStatus {
                CardOnBoard = CardOnBoard,
                CurrentSelection = CurrentSelection,
                PreviousSelection = PreviousSelection,
                Path = Path
            };
        }
    }
}