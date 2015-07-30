namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    public class CardCreatedAction : CardAction {
        public CardCreatedAction(Guid id)
            : base(id) {}
    }
}