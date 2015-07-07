namespace Assets.BattleForBetelgeuse.Cards.UnitCards {
    using Assets.GameManagement;

    public abstract class UnitCard : CombatCard
    {
        private readonly string prefabLocation = "Units/";

        public virtual int Movement { get; private set; }
        internal virtual string PrefabName { get; private set; }

        public override CardType Type {
            get {
                return CardType.Unit;
            }
        }

        public string PrefabPath {
            get {
                return prefabLocation + PrefabName;
            }
            internal set {
                ErrorHandling.InvalidOpration(this);
            }
        }
    }
}