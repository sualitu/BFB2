namespace BattleForBetelgeuse.Actions {

  public class DelayedAction : Dispatchable {

    private Dispatchable _innerAction;

    public DelayedAction(Dispatchable innerAction) : base() {
      _innerAction = innerAction;
    }

    public Dispatchable Unwrap() {
      return _innerAction;
    }
  }
}

