namespace BattleForBetelgeuse.GameElements.Cards {
    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.Cards;
    using BattleForBetelgeuse.GUI.Hex;

    public abstract class CardPlayedAction<T> : Dispatchable
        where T : Card {
        public CardPlayedAction(HexCoordinate location, T card) {
            this.Location = location;
            this.Card = card;
        }

        public HexCoordinate Location { get; private set; }
        public T Card { get; private set; }
    }
}