namespace Assets.BattleForBetelgeuse.FluxElements.Combat {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Combat.Events;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;

    public class CombatStore : PublishingStore<CombatLog> {
        private static CombatStore instance;

        private readonly List<CombatEvent> events;

        private int position = -1;

        private CombatStore() {
            events = new List<CombatEvent>();
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
            var logEvent = PerformCombat(action);
            events.Add(logEvent);
        }

        public override void Update(Dispatchable action) {
            if (action is UnitCombatAction) {
                var unitCombatAction = (UnitCombatAction)action;
                HandleUnitCombatAction(unitCombatAction);
                Publish();
            }
        }

        internal override void Publish() {
            base.Publish();
            position++;
        }

        internal override void SendMessage(Message msg) {
            msg(new CombatLog(events.ToArray(), position));
        }
    }
}