namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Actions;

    public class CardPlayedAction : CardAction {
        public CardPlayedAction(Guid id)
            : base(id) {}
    }
}