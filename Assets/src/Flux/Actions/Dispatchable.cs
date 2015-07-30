namespace Assets.Flux.Actions {
    using System;
    using System.Threading;
    using Assets.Flux.Dispatchers;

    public abstract class Dispatchable : IComparable<Dispatchable> {
        protected AutoResetEvent _readyToGo = new AutoResetEvent(false);

        protected Dispatchable()
        {
            Invocation = DateTime.Now.Ticks;
            Dispatcher.Instance.Signup(this);
        }

        public long Invocation { get; private set; }

        public int CompareTo(Dispatchable other) {
            return Invocation.CompareTo(other.Invocation);
        }
    }
}