namespace Assets.Elements.GUI.Grid.Board {
    using Assets.Flux.Actions;

    public class BoardUpdateAction : ThrottledAction {
        private BoardStatus boardStatus;

        public BoardUpdateAction(BoardStatus boardStatus) {
            BoardStatus = boardStatus;
        }

        public BoardStatus BoardStatus {
            get {
                _readyToGo.WaitOne();
                return boardStatus;
            }
            private set {
                _readyToGo.Set();
                boardStatus = value;
            }
        }

        internal override bool _isThrottled() {
            return boardStatus.CurrentSelection != null && boardStatus.PreviousSelection != null;
        }

        public override string ToString() {
            return string.Format("{1}: [BoardUpdateAction: boardStatus={0}]", boardStatus, Invocation);
        }
    }
}