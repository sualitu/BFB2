namespace BattleForBetelgeuse.GameElements.Units {
    public abstract class Fighter {
        public delegate int DealDamage();

        public delegate void TakeDamage(int damage);

        public DealDamage DealDamageAttacking;

        public DealDamage DealDamageDefending;

        public TakeDamage TakeDamageAttacking;

        public TakeDamage TakeDamageDefending;

        protected Fighter() {
            this.DealDamageAttacking = this.StandardDealDamage;
            this.DealDamageDefending = this.StandardDealDamage;
            this.TakeDamageAttacking = this.StandardTakeDamage;
            this.TakeDamageDefending = this.StandardTakeDamage;
        }

        public int Health { get; set; }
        public int DamageTaken { get; set; }
        public int Attack { get; set; }
        public int AttackChanged { get; set; }

        public int CurrentHealth() {
            return this.Health - this.DamageTaken;
        }

        public int CurrentAttack() {
            return this.Attack + this.AttackChanged;
        }

        private int StandardDealDamage() {
            return this.CurrentAttack();
        }

        private void StandardTakeDamage(int damage) {
            this.DamageTaken += damage;
        }
    }
}