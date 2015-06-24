namespace BattleForBetelgeuse.GameElements.Units {

  public abstract class Fighter {

    public int Health { get; set; }
    
    public int DamageTaken { get; set; }    
    
    public int CurrentHealth() { return Health - DamageTaken; }
    
    public int Attack { get; set; }
    
    public int AttackChanged { get; private set; }
    
    public int CurrentAttack() { return Attack + AttackChanged; }

    public delegate void CombatAction(Fighter me, Fighter other);

    public CombatAction OnAttack = StandardOnAttack;
    public CombatAction OnDefend = StandardOnDefend;

    private static void StandardOnAttack(Fighter attacker, Fighter defender) {
      attacker.DamageTaken += defender.Attack;
    }
    private static void StandardOnDefend(Fighter attacker, Fighter defender) {
      defender.DamageTaken += attacker.Attack;
    }

    public void FightAgainst(Fighter defender) {
      OnAttack(this, defender);
      defender.DefendAgainst(this);
    }

    public void DefendAgainst(Fighter attacker) {
      OnDefend(attacker, this);

    }
  }
}

