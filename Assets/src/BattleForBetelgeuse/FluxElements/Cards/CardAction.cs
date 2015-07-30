namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    using Assets.Flux.Actions;

    public abstract class CardAction : UnpausableAction {
        protected CardAction(Guid id) {
            Id = id;
        }

        private Guid id;

        private bool idSet;

        public Guid Id
        {
            get
            {
                if (!idSet) {
                    _readyToGo.WaitOne();
                }
                return id;
            }
            private set
            {
                _readyToGo.Set();
                idSet = true;
                id = value;
            }
        }
    }
}