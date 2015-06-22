using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.Actions;
using System.Collections.Generic;

namespace BattleForBetelgeuse.GameElements.Unit {

  public class CombatStore : PublishingStore<IEnumerable<CombatEvent>> {

    private static CombatStore instance;
        
    public static CombatStore Instance { 
      get {
        if(instance == null) {
          instance = new CombatStore();
        }
        return instance;
      } 
    }

    private CombatStore() : base() {

    }

    public override void Update(Dispatchable action) {
      if(action is UnitCombatAction) {
        var unitCombatAction = (UnitCombatAction)action;
        Publish();
      }
    }

    internal override void SendMessage(Message msg) {
      msg(null);
    }
  }
}

