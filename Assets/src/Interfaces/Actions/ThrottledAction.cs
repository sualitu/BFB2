
namespace BattleForBetelgeuse.Actions {

  public abstract class ThrottledAction : Dispatchable {
    internal virtual bool _isThrottled() {
      return true;
    }

    public static bool IsThrottled(Dispatchable action) {
      return action is ThrottledAction && ((ThrottledAction) action)._isThrottled();
    }
  }



  
}

