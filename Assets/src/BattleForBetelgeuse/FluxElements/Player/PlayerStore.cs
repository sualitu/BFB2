namespace Assets.BattleForBetelgeuse.FluxElements.Player {
    using Assets.Flux.Stores;

    public abstract class PlayerStore<T> : PublishingStore<T>
        where T : Player {
        internal T Player { get; set; }
        public abstract void Draw();

        public abstract void NewTurn();

        private int nextCardId = 200;

        internal int GetFreshCardId() {
            return nextCardId++;
        }
    }
}