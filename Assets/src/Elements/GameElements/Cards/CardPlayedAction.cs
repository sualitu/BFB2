using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.Cards;
using BattleForBetelgeuse.GUI.Hex;

namespace BattleForBetelgeuse.GameElements.Cards {

  public abstract class CardPlayedAction<T> : Dispatchable where T : Card{

    public HexCoordinate Location  { get; private set; }
    public T Card  { get; private set; }

    public CardPlayedAction(HexCoordinate location, T card) {
      Location = location;
      Card = card;
    }
  }
}

