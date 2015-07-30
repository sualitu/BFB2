namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Combat;
    using Assets.BattleForBetelgeuse.FluxElements.Combat.Events;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Views;
    using Assets.Utilities;

    public class UnitView : BehaviourUpdatingView {
        private bool hasMoved;

        public UnitView(HexCoordinate coordinate) {
            Alive = true;
            Coordinate = coordinate;
        }

        internal HexCoordinate Coordinate { get; set; }
        public List<HexCoordinate> Path { get; set; }
        public HexCoordinate AttackTarget { get; set; }
        public HexCoordinate CombatTarget { get; set; }
        internal bool Alive { get; set; }

        internal bool HasMoved {
            get {
                var old = hasMoved;
                hasMoved = false;
                return old;
            }
        }

        private void EngageCombat(HexCoordinate opponentPosition, Fighter unit) {
            Alive = unit.CurrentHealth() > 0;
            CombatTarget = opponentPosition;
            if (!Alive) {
                UnitStore.Instance.Unsubscribe(guid);
                CombatStore.Instance.Unsubscribe(guid);
            }
        }

        public void Combat(CombatLog log) {
            while (log.MoveNext()) {
                var current = log.Current;
                if (current is UnitCombatEvent) {
                    var unitCombatEvent = current as UnitCombatEvent;
                    if (unitCombatEvent.AttackerLocation.Equals(Coordinate)) {
                        Logger.Log("I'm attacking!");
                        EngageCombat(unitCombatEvent.DefenderLocation, unitCombatEvent.Attacker);
                    } else if (unitCombatEvent.DefenderLocation.Equals(Coordinate)) {
                        Logger.Log("I'm depending!");
                        EngageCombat(unitCombatEvent.AttackerLocation, unitCombatEvent.Defender);
                    }
                }
            }
            UpdateBehaviour();
        }

        public void Move(List<UnitChange> changes) {
            foreach (var change in changes) {
                if (change.From.Equals(Coordinate)) {
                    if (change.To != null) {
                        Coordinate = change.To;
                        Path = change.Path;
                        hasMoved = true;
                    }
                    if (change.Attack != null) {
                        AttackTarget = change.Attack;
                    } else {
                        AttackTarget = null;
                    }
                }
                UpdateBehaviour();
            }
        }

        public override void SetupSubscriptions() {
            UnitStore.Instance.Subscribe(guid, Move);
            CombatStore.Instance.Subscribe(guid, Combat);
        }
    }
}