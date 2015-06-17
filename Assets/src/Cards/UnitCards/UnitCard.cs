namespace BattleForBetelgeuse.Cards.UnitCards {

  public abstract class UnitCard : Card {
    public virtual int Health  { get; private set; }
    public virtual int Attack  { get; private set; }
    public virtual int Movement { get; private set; }

    internal virtual string PrefabName { get; private set; }

    private string prefabLocation = "Units/";

    public string PrefabPath { 
      get { return prefabLocation + PrefabName; } 
      internal set { 
        ErrorHandling.InvalidOpration(this);
      } 
    }
  }
}
