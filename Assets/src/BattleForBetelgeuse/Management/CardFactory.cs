namespace Assets.BattleForBetelgeuse.Management {
    using System.ComponentModel;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Cards.UnitCards;

    using UnityEngine;

    public class CardFactory {
        private const string CardPath = "Cards/";

        public static Texture2D GetTexture(Card card) {
            // Set basetexture
            var baseTexture = GetBaseTexture(card);
            var texture = new Texture2D(baseTexture.width, baseTexture.height);
            var pixels = baseTexture.GetPixels(0, 0, baseTexture.width, baseTexture.height);
            texture.SetPixels(pixels);

            // Set stats for units and buildings
            switch (card.Type) {
                case CardType.Unit:
                    SetTextureStats(texture, card as UnitCard);
                    break;
                case CardType.Building:
                    SetTextureStats(texture, card as BuildingCard);
                    break;
            }

            // Set mana cost
            SetManaCost(texture, card);

            return texture;
        }

        private static void SetManaCost(Texture2D texture, Card card) {
            var costTexture =
                (Texture2D)
                Resources.Load(string.Format("{2}costs/{0}/cost{1}",
                                             TranslateFactionToColourString(card.Faction),
                                             card.Cost,
                                             CardPath));
            SetSubTexture(texture, costTexture, 88, 309);
        }

        private static Texture2D GetBaseTexture(Card card) {
            switch (card.Type) {
                case CardType.Unit:
                    return
                        (Texture2D)
                        Resources.Load(string.Format("{0}base/baseunitcard_{1}",
                                                     CardPath,
                                                     TranslateFactionToColourString(card.Faction)));
                case CardType.Spell:
                    return
                        (Texture2D)
                        Resources.Load(string.Format("{0}base/basecard_{1}",
                                                     CardPath,
                                                     TranslateFactionToColourString(card.Faction)));
                case CardType.Building:
                    return
                        (Texture2D)
                        Resources.Load(string.Format("{0}/base/basebuildingcard_{1}",
                                                     CardPath,
                                                     TranslateFactionToColourString(card.Faction)));
            }
            throw new InvalidEnumArgumentException(string.Format("Card Type for card {0} not found.", card));
        }

        private static string TranslateFactionToColourString(CardFaction faction) {
            switch (faction) {
                case CardFaction.Blue:
                    return "blue";
                case CardFaction.Red:
                    return "red";
                case CardFaction.Green:
                    return "green";
                default:
                    return "grey";
            }
        }

        private static void SetTextureStats(Texture2D texture, BuildingCard card) {
            SetHealthAndAttack(texture, card);
        }

        private static void SetTextureStats(Texture2D texture, UnitCard card) {
            SetHealthAndAttack(texture, card);
            var movementTexture =
                (Texture2D)
                Resources.Load(string.Format("{0}movement/{1}/movement{2}",
                                             CardPath,
                                             TranslateFactionToColourString(card.Faction),
                                             card.Movement));
            SetSubTexture(texture, movementTexture, 95, 167);
        }

        private static void SetHealthAndAttack(Texture2D texture, CombatCard card) {
            var healthTexture =
                (Texture2D)
                Resources.Load(string.Format("{0}health/{1}/health{2}",
                                             CardPath,
                                             TranslateFactionToColourString(card.Faction),
                                             card.Health));
            SetSubTexture(texture, healthTexture, 154, 165);
            var attackTexture =
                (Texture2D)
                Resources.Load(string.Format("{0}attack/{1}/attack{2}",
                                             CardPath,
                                             TranslateFactionToColourString(card.Faction),
                                             card.Attack));
            SetSubTexture(texture, attackTexture, 46, 164);
        }

        private static void SetSubTexture(Texture2D mainTexture, Texture2D subTexture, int x, int y) {
            var height = subTexture.height;
            var width = subTexture.width;
            var pixels = subTexture.GetPixels(0, 0, width, height);
            mainTexture.SetPixels(x, y, width, height, pixels);
        }
    }
}