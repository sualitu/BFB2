namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;
    using Assets.Utilities;

    public class CardStore : PublishingStore<CardUpdate> {
        private static CardStore instance;

        private static readonly Dictionary<Type, CardStatus> StatusToActionMap = new Dictionary<Type, CardStatus> {
            { typeof(CardCreatingAction), CardStatus.Creating },
            { typeof(CardCreatedAction), CardStatus.JustCreated },
            { typeof(CardToHandAction), CardStatus.InHand },
            { typeof(CardHovederedAction), CardStatus.Hovered },
            { typeof(CardUnhovederedAction), CardStatus.Unhovered },
            { typeof(CardPickedUpdAction), CardStatus.PickedUp }
        };

        private CardUpdate currentUpdate;

        private CardStore() {}

        public static CardStore Instance {
            get {
                if (instance == null) {
                    instance = new CardStore();
                }
                return instance;
            }
        }

        public static int cardActions = 0;

        public static int messagesSend = 0;

        public static int somenewtotalffs = 0;

        public override void UpdateStore(Dispatchable action) {
            if (action is CardAction) {
                if (action is CardCreatingAction) {
                    cardActions++;
                }
                var cardAction = action as CardAction;
                CardStatus status;
                if (cardAction is CardPutDownAction) {
                    var cardPutDownAction = cardAction as CardPutDownAction;
                    status = cardPutDownAction.LeftClick && cardPutDownAction.Position.y > Settings.Animations.Cards.CardDropZoneCutOff.y ? CardStatus.Played : CardStatus.InHand;
                } else {
                    status = StatusToActionMap[cardAction.GetType()];
                }
                currentUpdate = new CardUpdate { Id = cardAction.Id, Status = status };
                Publish();
            }
        }

        public static int hufl = 0;

        internal override void SendMessage(Message msg) {
            if (currentUpdate.Status == CardStatus.Creating) {
                messagesSend++;
            }
            msg(currentUpdate);
            if (currentUpdate.Status == CardStatus.Creating)
            {
                hufl++;
            }
        }

        public void CardDrawn(int id, Card card) {
            CardManager.CardsToCreate.Add(new Tuple<Card, int>(card, id));
        }
    }

    public class CardUpdate {
        public CardStatus Status { get; set; }

        public int Id { get; set; }
    }

    public enum CardStatus {
        InHand,

        Creating,

        JustCreated,

        DiscardingFromDraw,

        DiscardingFromHand,

        Played,

        Unknown,

        Hovered,

        Unhovered,

        PickedUp
    }
}