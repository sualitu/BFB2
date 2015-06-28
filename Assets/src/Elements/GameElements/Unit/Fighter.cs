namespace BattleForBetelgeuse.GameElements.Units {

  public abstract class Fighter {

    public int Health { get; set; }
    
    public int DamageTaken { get; set; }
    
    public int CurrentHealth() {
      return Health - DamageTaken;
    }
    
    public int Attack { get; set; }
    
    public int AttackChanged { get; private set; }
    
    public int CurrentAttack() {
      return Attack + AttackChanged;
    }

    public delegate int DealDamage();

    public delegate void TakeDamage(int damage);

    private int StandardDealDamage() {
      return CurrentAttack();
    }

    private void StandardTakeDamage(int damage) {
      DamageTaken += damage;
    }

    public DealDamage DealDamageAttacking;
    public DealDamage DealDamageDefending;
    public TakeDamage TakeDamageAttacking;
    public TakeDamage TakeDamageDefending;

    public Fighter() {
      DealDamageAttacking = StandardDealDamage;
      DealDamageDefending = StandardDealDamage;
      TakeDamageAttacking = StandardTakeDamage;
      TakeDamageDefending = StandardTakeDamage;
    }
  }
}

