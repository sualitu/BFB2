namespace Assets.BattleForBetelgeuse.Cards.UnitCards {
    using Assets.Flux;

    public abstract class UnitCard : CombatCard
    {
        private const string PrefabLocation = "Units/";

        private const string CardPreSpawnLocation = "Animations/UnitHolos/";

        public virtual int Movement { get; private set; }
        internal virtual string PrefabName { get; private set; }

        public override CardType Type {
            get {
                return CardType.Unit;
            }
        }

        public override string PreSpawnAnimationPrefab {
            get {
                return CardPreSpawnLocation + PrefabName;
            }
        }

        public string PrefabPath {
            get {
                return PrefabLocation + PrefabName;
            }
            internal set {
                ErrorHandling.InvalidOpration(this);
            }
        }
    }
}