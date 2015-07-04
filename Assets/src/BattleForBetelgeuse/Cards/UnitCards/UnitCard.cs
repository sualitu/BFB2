namespace Assets.Cards.UnitCards {
    using Assets.GameManagement;
    
    public abstract class UnitCard : Card {
        private readonly string prefabLocation = "Units/";

        public virtual int Health { get; private set; }
        public virtual int Attack { get; private set; }
        public virtual int Movement { get; private set; }
        internal virtual string PrefabName { get; private set; }

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