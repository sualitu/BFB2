namespace Assets.BattleForBetelgeuse.Cards.UnitCards
{
    using Assets.GameManagement;

    public abstract class BuildingCard : CombatCard
    {
        private readonly string prefabLocation = "Buildings/";

        internal virtual string PrefabName { get; private set; }

        public override CardType Type
        {
            get
            {
                return CardType.Building;
            }
        }

        public string PrefabPath
        {
            get
            {
                return prefabLocation + PrefabName;
            }
            internal set
            {
                ErrorHandling.InvalidOpration(this);
            }
        }
    }
}