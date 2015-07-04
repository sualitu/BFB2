namespace Assets.Flux.Actions.DispatcherActions {
    public class DelayedAction : Dispatchable {
        private readonly Dispatchable _innerAction;

        public DelayedAction(Dispatchable innerAction) {
            _innerAction = innerAction;
        }

        public Dispatchable Unwrap() {
            return _innerAction;
        }
    }
}