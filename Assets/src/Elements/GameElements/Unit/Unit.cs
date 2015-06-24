using BattleForBetelgeuse.Cards.UnitCards;
using BattleForBetelgeuse.GUI.Hex;

namespace BattleForBetelgeuse.GameElements.Units {

  public class Unit : Fighter {
    public static Unit FromCard(UnitCard card) {
      return new Unit {
        Health = card.Health,
        Attack = card.Attack,
        Movement = card.Movement
      };
    }

    public int Movement { get; set; }

    public int MovementSpend { get; set; }

    public int CurrentMovement() { return Movement - MovementSpend; }
  }
}
