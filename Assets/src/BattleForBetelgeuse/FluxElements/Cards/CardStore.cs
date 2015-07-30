namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;

    public class CardStore : PublishingStore<CardUpdate> {
        private static CardStore instance;

        private static readonly Dictionary<Type, CardStatus> StatusToActionMap = new Dictionary<Type, CardStatus> {
            { typeof(CardCreatingAction), CardStatus.Creating },
            { typeof(CardCreatedAction), CardStatus.JustCreated },
            { typeof(CardToHandAction), CardStatus.InHand },
            { typeof(CardHovederedAction), CardStatus.Hovered },
            { typeof(CardUnhovederedAction), CardStatus.Unhovered },
            { typeof(CardOnBoardAction), CardStatus.OnBoard },
            { typeof(CardPickedUpAction), CardStatus.PickedUp },
            { typeof(CardPlayedAction), CardStatus.Removed }
        };

        public Dictionary<Guid, Card> Cards { get; private set; }

        private CardUpdate currentUpdate;

        private CardStore() {
            Cards = new Dictionary<Guid, Card>();
        }

        public static CardStore Instance {
            get {
                if (instance == null) {
                    instance = new CardStore();
                }
                return instance;
            }
        }

        public override void UpdateStore(Dispatchable action) {
            if (action is CardAction) {
                var cardAction = action as CardAction;
                CardStatus status;
                if (cardAction is CardPutDownAction) {
                    var cardPutDownAction = cardAction as CardPutDownAction;
                    status = IsPutDownOnBoard(cardPutDownAction) ? CardStatus.OnBoard : CardStatus.InHand;
                } else {
                    status = StatusToActionMap[cardAction.GetType()];
                }
                currentUpdate = new CardUpdate { Id = cardAction.Id, Status = status };
                Publish();
            }
        }

        public static bool IsPutDownOnBoard(CardPutDownAction cardPutDownAction) {
            return cardPutDownAction.LeftClick
                   && cardPutDownAction.Position.y > Settings.Animations.Cards.CardDropZoneCutOff.y;
        }

        internal override void SendMessage(Message msg) {
            msg(currentUpdate);
        }

        public void CardDrawn(Guid id, Card card) {
            Cards[id] = card;
            CardManager.CardsToCreate.Add(id);
        }
    }

    public class CardUpdate {
        public CardStatus Status { get; set; }
        public Guid Id { get; set; }
    }

    public enum CardStatus {
        InHand,

        Creating,

        JustCreated,

        DiscardingFromDraw,

        DiscardingFromHand,

        OnBoard,

        Unknown,

        Hovered,

        Unhovered,

        PickedUp,

        Removed
    }
}