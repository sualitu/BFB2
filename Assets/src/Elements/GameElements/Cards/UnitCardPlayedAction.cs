using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Cards.UnitCards;

namespace BattleForBetelgeuse.GameElements.Cards {
  
  public class UnitCardPlayedAction : CardPlayedAction<UnitCard> {

    public UnitCardPlayedAction(HexCoordinate location, UnitCard card) : base(location, card) {
      
    }
  }
}
