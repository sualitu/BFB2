namespace Assets.Elements.GameElements.Cards {
    using Assets.Cards.UnitCards;
    using Assets.Elements.GUI.Grid.HexTile;

    public class UnitCardPlayedAction : CardPlayedAction<UnitCard> {
        public UnitCardPlayedAction(HexCoordinate location, UnitCard card)
            : base(location, card) {}
    }
}