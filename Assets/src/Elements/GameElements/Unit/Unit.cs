namespace BattleForBetelgeuse.GameElements.Units {
    using BattleForBetelgeuse.Cards.UnitCards;

    public class Unit : Fighter {
        public int Movement { get; set; }
        public int MovementSpend { get; set; }

        public static Unit FromCard(UnitCard card) {
            return new Unit { Health = card.Health, Attack = card.Attack, Movement = card.Movement };
        }

        public override int CurrentHealth() {
            var curr = base.CurrentHealth();
            if (curr < 0) {
                UnitStore.Instance.RemoveUnit(this);
            }
            return curr;
        }

        public int CurrentMovement() {
            return Movement - MovementSpend;
        }
    }
}