namespace BattleForBetelgeuse.Cards {
    using System.Collections.Generic;

    public abstract class Card {
        public static List<Card> AllCards = new List<Card>();

        public Card() {
            AllCards.Add(this);
        }

        public virtual int ManaCost { get; private set; }
    }
}