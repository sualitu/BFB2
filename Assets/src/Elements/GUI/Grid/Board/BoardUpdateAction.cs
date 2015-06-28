namespace BattleForBetelgeuse.GUI.Board {
    using BattleForBetelgeuse.Actions;

    public class BoardUpdateAction : ThrottledAction {
        private BoardStatus boardStatus;

        public BoardUpdateAction(BoardStatus boardStatus) {
            this.BoardStatus = boardStatus;
        }

        public BoardStatus BoardStatus {
            get {
                this._readyToGo.WaitOne();
                return this.boardStatus;
            }
            private set {
                this._readyToGo.Set();
                this.boardStatus = value;
            }
        }

        internal override bool _isThrottled() {
            return this.boardStatus.CurrentSelection != null && this.boardStatus.PreviousSelection != null;
        }

        public override string ToString() {
            return string.Format("{1}: [BoardUpdateAction: boardStatus={0}]", this.boardStatus, this.Invocation);
        }
    }
}