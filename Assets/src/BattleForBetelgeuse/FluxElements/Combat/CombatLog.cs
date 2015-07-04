namespace Assets.Elements.GameElements.Combat {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Assets.Elements.GameElements.Combat.Events;

    public class CombatLog : IEnumerator<CombatEvent> {
        private readonly CombatEvent[] _log;

        private readonly int startPosition;

        private int currentPosition;

        public CombatLog(CombatEvent[] log, int startPosition = -1) {
            this.startPosition = startPosition;
            currentPosition = startPosition;
            _log = log;
        }

        public bool MoveNext() {
            currentPosition ++;
            return currentPosition < _log.Length;
        }

        public void Reset() {
            currentPosition = startPosition;
        }

        public void Dispose() {}

        public CombatEvent Current {
            get {
                try {
                    return _log[currentPosition];
                } catch (IndexOutOfRangeException) {
                    throw new InvalidOperationException(string.Format("No event in combat log at position {0}",
                                                                      currentPosition));
                }
            }
        }

        object IEnumerator.Current {
            get {
                return Current;
            }
        }

        public void FromTheBeginningOfTime() {
            currentPosition = -1;
        }

        public override string ToString() {
            return string.Format("CombatLog with length {0} at position {1}", _log.Length, currentPosition);
        }
    }
}