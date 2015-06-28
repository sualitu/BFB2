namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Cards.UnitCards;

    public class Unit : Fighter {
        public int Movement { get; set; }
        public int MovementSpend { get; set; }

        public static Unit FromCard(UnitCard card) {
            return new Unit { Health = card.Health, Attack = card.Attack, Movement = card.Movement };
        }

        public int CurrentMovement() {
            return this.Movement - this.MovementSpend;
        }
    }
}