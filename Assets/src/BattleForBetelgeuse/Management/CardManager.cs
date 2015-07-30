namespace Assets.BattleForBetelgeuse.Management {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.Player;
    using Assets.Utilities;

    using UnityEngine;

    public class CardManager : MonoBehaviour {
        public static List<Guid> CardsToCreate;

        public GameObject CardPrefab;

        private void Awake() {
            CardsToCreate = new List<Guid>();
        }

        private void Start() {
            HandManager.Init();
        }

        private void Update() {
            while(CardsToCreate.Count > 0) {
                var cardId = CardsToCreate.GetAndRemoveRandom();
                var card = CardStore.Instance.Cards[cardId];
                var view = new CardView(cardId);
                var cardObject = Instantiate(CardPrefab);
                cardObject.transform.parent = transform;
                var cardBehaviour = cardObject.GetComponent<CardBehaviour>() ?? cardObject.AddComponent<CardBehaviour>();
                cardBehaviour.Id = cardId;
                cardBehaviour.Companion = view;
                cardBehaviour.SetCard(card);
                new CardCreatingAction(cardId);
            }
        }
    }
}