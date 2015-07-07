namespace Assets.BattleForBetelgeuse.Cards.UnitCards {
    public abstract class CombatCard : Card {
        public virtual int Health { get; private set; }
        public virtual int Attack { get; private set; }
    }
}