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
        public static List<Tuple<Card, int>> CardsToCreate;

        public GameObject CardPrefab;

        private void Awake() {
            CardsToCreate = new List<Tuple<Card, int>>();
        }

        private void Start() {
            HandManager.Init();
        }

        public static int i;

        private void Update() {
            while(CardsToCreate.Count > 0) {
                var card = CardsToCreate.GetAndRemoveRandom();
                i = card.Second;
               var view = new CardView(card.Second);
                var cardObject = Instantiate(CardPrefab);
                cardObject.transform.parent = transform;
                var cardBehaviour = cardObject.GetComponent<CardBehaviour>() ?? cardObject.AddComponent<CardBehaviour>();
                cardBehaviour.Companion = view;
                cardBehaviour.Id = card.Second;
                cardBehaviour.SetCard(card.First);
                new CardCreatingAction(card.Second);
            }
        }
    }
}