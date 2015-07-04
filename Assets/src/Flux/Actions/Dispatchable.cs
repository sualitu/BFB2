namespace Assets.Flux.Actions {
    using System;
    using System.Threading;

    using Assets.Flux.Actions.DispatcherActions;
    using Assets.Flux.Dispatchers;

    public abstract class Dispatchable : IComparable<Dispatchable> {
        internal AutoResetEvent _readyToGo = new AutoResetEvent(false);

        public Dispatchable() {
            Invocation = DateTime.Now.Ticks;
            Dispatcher.Instance.Signup(this);
        }

        public long Invocation { get; private set; }

        public int CompareTo(Dispatchable other) {
            return Invocation.CompareTo(other.Invocation);
        }

        public void Delay() {
            new DelayedAction(this);
        }
    }
}