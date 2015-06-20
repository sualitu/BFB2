using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.Dispatching;
using UnityEngine;
using System.Threading;
using System;

namespace BattleForBetelgeuse.Actions {

  public abstract class Dispatchable : IComparable<Dispatchable> {

    public long invocation { get; private set; }

    public void Delay() {
      new DelayedAction(this);
    }

    internal AutoResetEvent _readyToGo = new AutoResetEvent(false);

    public int CompareTo(Dispatchable other) {
      return invocation.CompareTo(other.invocation);
    }

    public Dispatchable() {
      invocation = DateTime.Now.Ticks;
      Dispatcher.Instance.Signup(this);
    }
  }
}

