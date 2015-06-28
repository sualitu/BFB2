namespace BattleForBetelgeuse.GameElements.Combat {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using BattleForBetelgeuse.GameElements.Combat.Events;

    public class CombatLog : IEnumerator<CombatEvent> {
        private readonly CombatEvent[] _log;

        private readonly int startPosition;

        private int currentPosition;

        public CombatLog(CombatEvent[] log, int startPosition = -1) {
            this.startPosition = startPosition;
            this.currentPosition = startPosition;
            this._log = log;
        }

        public bool MoveNext() {
            this.currentPosition ++;
            return this.currentPosition < this._log.Length;
        }

        public void Reset() {
            this.currentPosition = this.startPosition;
        }

        public void Dispose() {}

        public CombatEvent Current {
            get {
                try {
                    return this._log[this.currentPosition];
                } catch (IndexOutOfRangeException) {
                    throw new InvalidOperationException(string.Format("No event in combat log at position {0}",
                                                                      this.currentPosition));
                }
            }
        }

        object IEnumerator.Current {
            get {
                return this.Current;
            }
        }

        public void FromTheBeginningOfTime() {
            this.currentPosition = -1;
        }

        public override string ToString() {
            return string.Format("CombatLog with length {0} at position {1}", this._log.Length, this.currentPosition);
        }
    }
}