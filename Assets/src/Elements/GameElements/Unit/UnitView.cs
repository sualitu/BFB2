namespace BattleForBetelgeuse.GameElements.Units {
    using System.Collections.Generic;

    using BattleForBetelgeuse.GameElements.Combat;
    using BattleForBetelgeuse.GameElements.Combat.Events;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.View;

    public class UnitView : BehaviourUpdatingView {
        private bool hasMoved;

        public UnitView(HexCoordinate coordinate) {
            this.Alive = true;
            this.Coordinate = coordinate;
            UnitStore.Instance.Subscribe(this.Move);
            CombatStore.Instance.Subscribe(this.Combat);
        }

        internal HexCoordinate Coordinate { get; set; }
        public List<HexCoordinate> Path { get; set; }
        public HexCoordinate AttackTarget { get; set; }
        internal bool Alive { get; set; }

        internal bool HasMoved {
            get {
                var old = this.hasMoved;
                this.hasMoved = false;
                return old;
            }
        }

        private void EngageCombat(HexCoordinate opponentPosition, Unit unit) {
            this.Alive = unit.CurrentHealth() > 0;
        }

        public void Combat(CombatLog log) {
            while (log.MoveNext()) {
                var current = log.Current;
                if (current is UnitCombatEvent) {
                    var unitCombatEvent = current as UnitCombatEvent;

                    if (unitCombatEvent.AttackerLocation.Equals(this.Coordinate)) {
                        this.EngageCombat(unitCombatEvent.DefenderLocation, unitCombatEvent.Attacker);
                    } else if (unitCombatEvent.DefenderLocation.Equals(this.Coordinate)) {
                        this.EngageCombat(unitCombatEvent.AttackerLocation, unitCombatEvent.Defender);
                    }
                }
            }
            this.UpdateBehaviour();
        }

        public void Move(List<UnitChange> changes) {
            foreach (var change in changes) {
                if (change.From.Equals(this.Coordinate)) {
                    if (change.To != null) {
                        this.Coordinate = change.To;
                        this.Path = change.Path;
                        this.hasMoved = true;
                    }
                    if (change.Attack != null) {
                        this.AttackTarget = change.Attack;
                    } else {
                        this.AttackTarget = null;
                    }
                }
                this.UpdateBehaviour();
            }
        }
    }
}