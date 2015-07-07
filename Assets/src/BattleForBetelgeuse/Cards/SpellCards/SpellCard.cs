namespace Assets.BattleForBetelgeuse.Cards.SpellCards {
    public abstract class SpellCard : Card {
        public override CardType Type {
            get {
                return CardType.Spell;
            }
        }
    }
}