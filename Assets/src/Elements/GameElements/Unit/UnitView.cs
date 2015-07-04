namespace BattleForBetelgeuse.GameElements.Units {
    using System.Collections.Generic;

    using BattleForBetelgeuse.GameElements.Combat;
    using BattleForBetelgeuse.GameElements.Combat.Events;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.View;

    public class UnitView : BehaviourUpdatingView {
        private bool hasMoved;

        public UnitView(HexCoordinate coordinate) {
            Alive = true;
            Coordinate = coordinate;
            UnitStore.Instance.Subscribe(Move);
            CombatStore.Instance.Subscribe(Combat);
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
        }

        public void Combat(CombatLog log) {
            while (log.MoveNext()) {
                var current = log.Current;
                if (current is UnitCombatEvent) {
                    var unitCombatEvent = current as UnitCombatEvent;

                    if (unitCombatEvent.AttackerLocation.Equals(Coordinate)) {
                        EngageCombat(unitCombatEvent.DefenderLocation, unitCombatEvent.Attacker);
                    } else if (unitCombatEvent.DefenderLocation.Equals(Coordinate)) {
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
    }
}