using System.Collections.Generic;

namespace BattleForBetelgeuse.Cards {

  public abstract class Card {

    public static List<Card> AllCards = new List<Card>();

    public virtual int ManaCost { get; private set; }

    public Card() {
      AllCards.Add(this);
    }    
  }
}

