namespace Assets.Cards.UnitCards {
    public class TestUnit : UnitCard {
        public override int ManaCost {
            get {
                return 7;
            }
        }

        public override int Health {
            get {
                return 8;
            }
        }

        public override int Attack {
            get {
                return 10;
            }
        }

        public override int Movement {
            get {
                return 9;
            }
        }

        internal override string PrefabName {
            get {
                return "Fighter";
            }
        }
    }
}