using System;

namespace BattleForBetelgeuse.GameElements.Combat.Events {

  public abstract class CombatEvent {
    
    public long Time { get; private set; }

    public CombatEvent(long time) {
      Time = time;
    }
  }
}

