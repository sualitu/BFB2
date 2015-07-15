namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using Assets.Flux.Actions;

    internal class CardDrawnAction : Dispatchable {
        public CardDrawnAction(bool opponent = false) {
            Opponent = opponent;
        }

        public bool Opponent { get; private set; }
    }
}