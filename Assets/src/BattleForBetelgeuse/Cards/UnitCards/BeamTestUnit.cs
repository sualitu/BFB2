namespace Assets.BattleForBetelgeuse.Cards.UnitCards {
    public class BeamTestUnit : UnitCard {
        public override CardFaction Faction
        {
            get {
                return CardFaction.Blue;
            }
        }

        public override int Cost {
            get {
                return 20;
            }
        }

        public override string Name {
            get {
                return "Super Moo Poo Unit 2000 Horse";
            }
        }

        public override int Health {
            get {
                return 1;
            }
        }

        public override int Attack {
            get {
                return 1;
            }
        }

        public override int Movement {
            get {
                return 20;
            }
        }

        internal override string PrefabName {
            get {
                return "Beamer";
            }
        }
    }
}