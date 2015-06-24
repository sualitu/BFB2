using System.Collections.Generic;
using System;
using System.Collections;
using BattleForBetelgeuse.GameElements.Combat.Events;

namespace BattleForBetelgeuse.GameElements.Combat {

  public class CombatLog : IEnumerator<CombatEvent> {

    private CombatEvent[] _log;

    private int _startPosition;
    private int _currentPosition;

    public CombatLog(CombatEvent[] log, int startPosition = -1) {
      _startPosition = startPosition;
      _currentPosition = startPosition;
      _log = log;
    }

    public bool MoveNext() {
      _currentPosition ++;
      return _currentPosition < _log.Length;
    }

    public void Reset() {
      _currentPosition = _startPosition;
    }

    public void FromTheBeginningOfTime() {
      _currentPosition = -1;
    }

    public void Dispose() { }

    public CombatEvent Current {
      get {
        try {
          return _log[_currentPosition];
        } catch(IndexOutOfRangeException) {
          throw new InvalidOperationException(String.Format("No event in combat log at position {0}", _currentPosition));
        }
      }
    }    
    
    object IEnumerator.Current {
      get {
        return this.Current;
      }
    }

    public override string ToString() {
      return string.Format("CombatLog with length {0} at position {1}", _log.Length, _currentPosition);
    }
    
  }
}

