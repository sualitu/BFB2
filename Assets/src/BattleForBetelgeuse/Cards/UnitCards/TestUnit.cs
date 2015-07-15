namespace Assets.BattleForBetelgeuse.Cards.UnitCards {
    public class TestUnit : UnitCard {
        public override CardFaction Faction
        {
            get
            {
                return CardFaction.Neutral;
            }
        }

        public override int Cost {
            get {
                return 14;
            }
        }

        public override string Name {
            get {
                return "Test Card Unit";
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