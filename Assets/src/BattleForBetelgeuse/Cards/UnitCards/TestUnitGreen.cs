namespace Assets.BattleForBetelgeuse.Cards.UnitCards
{
    public class TestUnitGreen : UnitCard
    {
        public override CardFaction Faction
        {
            get
            {
                return CardFaction.Green;
            }
        }

        public override int Cost
        {
            get
            {
                return 2;
            }
        }

        public override string Name
        {
            get
            {
                return "Test Card Unit Green";
            }
        }

        public override int Health
        {
            get
            {
                return 2;
            }
        }

        public override int Attack
        {
            get
            {
                return 7;
            }
        }

        public override int Movement
        {
            get
            {
                return 4;
            }
        }

        internal override string PrefabName
        {
            get
            {
                return "Fighter";
            }
        }
    }
}