namespace BattleForBetelgeuse.GameElements.Cards {
    using BattleForBetelgeuse.Cards.UnitCards;
    using BattleForBetelgeuse.GUI.Hex;

    public class UnitCardPlayedAction : CardPlayedAction<UnitCard> {
        public UnitCardPlayedAction(HexCoordinate location, UnitCard card)
            : base(location, card) {}
    }
}