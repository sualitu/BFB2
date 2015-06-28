namespace BattleForBetelgeuse.GameElements.Combat {
    using System.Collections.Generic;

    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.GameElements.Combat.Events;
    using BattleForBetelgeuse.GameElements.Units;
    using BattleForBetelgeuse.Stores;

    public class CombatStore : PublishingStore<CombatLog> {
        private static CombatStore instance;

        private readonly List<CombatEvent> events;

        private int position = -1;

        private CombatStore() {
            this.events = new List<CombatEvent>();
        }

        public static CombatStore Instance {
            get {
                return instance ?? (instance = new CombatStore());
            }
        }

        private UnitCombatEvent PerformCombat(UnitCombatAction action) {
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
            var logEvent = this.PerformCombat(action);
            this.events.Add(logEvent);
        }

        public override void Update(Dispatchable action) {
            if (action is UnitCombatAction) {
                var unitCombatAction = (UnitCombatAction)action;
                this.HandleUnitCombatAction(unitCombatAction);
                this.Publish();
            }
        }

        internal override void Publish() {
            base.Publish();
            this.position++;
        }

        internal override void SendMessage(Message msg) {
            msg(new CombatLog(this.events.ToArray(), this.position));
        }
    }
}