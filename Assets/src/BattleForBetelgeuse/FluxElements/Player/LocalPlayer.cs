namespace Assets.BattleForBetelgeuse.FluxElements.Player {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.Flux;

    public class LocalPlayer : Player {

        public List<Card> Deck { get; set; }
        public List<int> Hand{ get; set; }

        public override int DeckCount {
            get {
                return Deck.Count;
            }
            // ReSharper disable once ValueParameterNotUsed
            set {
                ErrorHandling.InvalidOpration(this);
            }
        }

        public override int HandCount {
            get {
                return Hand.Count;
            }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                ErrorHandling.InvalidOpration(this);
            }
        }

        public override int ManaCount { get; set; }
    }
}