namespace BattleForBetelgeuse.Cards.UnitCards {
    public abstract class UnitCard : Card {
        private readonly string prefabLocation = "Units/";

        public virtual int Health { get; private set; }
        public virtual int Attack { get; private set; }
        public virtual int Movement { get; private set; }
        internal virtual string PrefabName { get; private set; }

        public string PrefabPath {
            get {
                return this.prefabLocation + this.PrefabName;
            }
            internal set {
                ErrorHandling.InvalidOpration(this);
            }
        }
    }
}