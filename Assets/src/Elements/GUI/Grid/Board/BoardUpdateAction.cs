using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.GUI.Board {

  public class BoardUpdateAction : ThrottledAction {
    BoardStatus boardStatus;

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

    public BoardUpdateAction(BoardStatus boardStatus) : base() {
      BoardStatus = boardStatus;
    }

    public override string ToString() {
      return string.Format("{1}: [BoardUpdateAction: boardStatus={0}]", boardStatus, invocation);
    }
    
  }


  
}

