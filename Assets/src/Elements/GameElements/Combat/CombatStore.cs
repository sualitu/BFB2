using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.Actions;
using System.Collections.Generic;
using BattleForBetelgeuse.GameElements.Units;
using BattleForBetelgeuse.GameElements.Combat.Events;

namespace BattleForBetelgeuse.GameElements.Combat {

  public class CombatStore : PublishingStore<CombatLog> {

    private static CombatStore instance;
        
    public static CombatStore Instance { 
      get {
        if(instance == null) {
          instance = new CombatStore();
        }
        return instance;
      } 
    }

    private void HandleUnitCombatAction(UnitCombatAction action) {
      action.Wait();
      _events.Add(new UnitCombatEvent(action));
    }

    private List<CombatEvent> _events; 
    private int _position = -1;

    private CombatStore() : base() {
      _events = new List<CombatEvent>();
    }

    public override void Update(Dispatchable action) {
      if(action is UnitCombatAction) {
        var unitCombatAction = (UnitCombatAction)action;
        HandleUnitCombatAction(unitCombatAction);
        Publish();
      }
    }

    internal override void Publish() {
      base.Publish();
      _position++;
    }

    internal override void SendMessage(Message msg) {
      msg(new CombatLog(_events.ToArray(), _position));
    }
  }
}

