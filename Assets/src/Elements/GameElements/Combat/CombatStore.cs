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

    UnitCombatEvent PerformCombat(UnitCombatAction action) {
      var attacker = action.Attacker;
      var attackerDamageDone = attacker.DealDamageAttacking();
      var defender = action.Defender;
      var defenderDamageDone = defender.DealDamageDefending();

      attacker.TakeDamageAttacking(defenderDamageDone);

      defender.TakeDamageDefending(attackerDamageDone);

      return new UnitCombatEvent(action.Invocation) {
        Attacker = attacker,
        AttackerLocation = action.From,
        Defender = defender,
        DefenderLocation = action.To
      };
    }

    private void HandleUnitCombatAction(UnitCombatAction action) {
      action.Wait();
      var logEvent = PerformCombat(action);
      _events.Add(logEvent);
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

