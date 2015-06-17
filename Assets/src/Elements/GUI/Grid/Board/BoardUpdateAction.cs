using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.GUI.Board {

  public class BoardUpdateAction : ThrottledAction {
    BoardStatus boardStatus;
    public BoardStatus BoardStatus { 
      get {
        _ReadyToGo.WaitOne();
        return boardStatus;
      }
      private set {
        _ReadyToGo.Set();
        boardStatus = value;
      }
    }

    public BoardUpdateAction(BoardStatus boardStatus) : base() {
      BoardStatus = boardStatus;
    }

    public override string ToString() {
      return string.Format("{1}: [BoardUpdateAction: boardStatus={0}]", boardStatus, invocation);
    }
    
  }


  
}

