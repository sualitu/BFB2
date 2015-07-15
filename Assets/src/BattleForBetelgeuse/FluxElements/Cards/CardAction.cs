namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    using Assets.Flux.Actions;

    public abstract class CardAction : Dispatchable {
        protected CardAction(int id) {
            Id = id;
        }

        private int id;

        public int Id
        {
            get
            {
                _readyToGo.WaitOne();
                return id;
            }
            private set
            {
                id = value;
                _readyToGo.Set();
            }
        }
    }
}