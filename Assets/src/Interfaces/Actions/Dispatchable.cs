using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.Dispatching;
using UnityEngine;
using System.Threading;
using System;

namespace BattleForBetelgeuse.Actions {

  public abstract class Dispatchable : IComparable<Dispatchable> {

    public long Invocation { get; private set; }

    public void Delay() {
      new DelayedAction(this);
    }

    internal AutoResetEvent _readyToGo = new AutoResetEvent(false);

    public int CompareTo(Dispatchable other) {
      return Invocation.CompareTo(other.Invocation);
    }

    public Dispatchable() {
      Invocation = DateTime.Now.Ticks;
      Dispatcher.Instance.Signup(this);
    }
  }
}

