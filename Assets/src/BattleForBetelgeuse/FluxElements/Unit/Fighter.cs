namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    public abstract class Fighter {
        public delegate int DealDamage();

        public delegate void TakeDamage(int damage);

        public DealDamage DealDamageAttacking;

        public DealDamage DealDamageDefending;

        public TakeDamage TakeDamageAttacking;

        public TakeDamage TakeDamageDefending;

        protected Fighter() {
            DealDamageAttacking = StandardDealDamage;
            DealDamageDefending = StandardDealDamage;
            TakeDamageAttacking = StandardTakeDamage;
            TakeDamageDefending = StandardTakeDamage;
        }

        public int Health { get; set; }
        public int DamageTaken { get; set; }
        public int Attack { get; set; }
        public int AttackChanged { get; set; }

        public virtual int CurrentHealth() {
            return Health - DamageTaken;
        }

        public int CurrentAttack() {
            return Attack + AttackChanged;
        }

        private int StandardDealDamage() {
            return CurrentAttack();
        }

        private void StandardTakeDamage(int damage) {
            DamageTaken += damage;
        }
    }
}