namespace Assets.BattleForBetelgeuse.Management {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.Cards;

    using UnityEngine;

    public class CardManager : MonoBehaviour {
        public static List<Card> CardsToCreate = new List<Card>();

        public GameObject CardPrefab;

        private void Start() {
            CardsToCreate.Add(new BeamTestUnit());
        }

        private void Update() {
            foreach (var card in CardsToCreate) {
                var cardObject = Instantiate(CardPrefab);
                cardObject.transform.parent = transform;
                var cardBehaviour = cardObject.GetComponent<CardBehaviour>();

                cardBehaviour.SetCard(card);
            }
            CardsToCreate.Clear();
        }
    }
}