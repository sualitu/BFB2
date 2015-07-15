namespace Assets.BattleForBetelgeuse.FluxElements.Player {
    

    public abstract class Player {
        public abstract int DeckCount { get; set; }
        public abstract int HandCount { get; set; }
        public abstract int ManaCount { get; set; }
    }
}