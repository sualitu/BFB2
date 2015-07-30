namespace Assets.BattleForBetelgeuse.Cards {
    using System.Collections.Generic;

    public abstract class Card {
        public static List<Card> AllCards = new List<Card>();

        public abstract string PreSpawnAnimationPrefab { get; }

        public abstract int Cost { get; }

        public abstract string Name { get;  }

        public abstract CardType Type { get; }

        public Card() {
            AllCards.Add(this);
        }

        public virtual CardFaction Faction {
            get {
                return CardFaction.Neutral;
            }
        }
    }
}