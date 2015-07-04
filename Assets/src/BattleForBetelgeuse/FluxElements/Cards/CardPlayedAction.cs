namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Actions;

    public abstract class CardPlayedAction<T> : Dispatchable
        where T : Card {
        public CardPlayedAction(HexCoordinate location, T card) {
            Location = location;
            Card = card;
        }

        public HexCoordinate Location { get; private set; }
        public T Card { get; private set; }
    }
}