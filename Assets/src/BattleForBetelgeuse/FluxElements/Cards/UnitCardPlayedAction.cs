namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;

    public class UnitCardPlayedAction : CardPlayedAction<UnitCard> {
        public UnitCardPlayedAction(HexCoordinate location, UnitCard card)
            : base(location, card) {}
    }
}