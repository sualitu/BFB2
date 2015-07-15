namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    using Assets.BattleForBetelgeuse.FluxElements.Player;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Views;
    using Assets.Utilities;

    using UnityEngine;

    public class CardView : BehaviourUpdatingView {
        public static string msg = "";

        public static int j;

        public static int i;

        public readonly int cardId;
        public string s = "Nothing yet";

        public CardStatus Status = CardStatus.Unknown;

        public CardView(int cardId) {
            this.cardId = cardId;
            CardStore.Instance.Subscribe(CheckUpdate);
            PositionChange = new Tuple<bool, Vector3>(false, Vector3.zero);
            RotationChange = new Tuple<bool, Vector3>(false, Vector3.zero);
            ScaleChange = new Tuple<bool, Vector3>(false, Vector3.zero);
        }

        internal bool InHand {
            get {
                return Status == CardStatus.InHand || Status == CardStatus.Hovered;
            }
        }

        internal bool PickedUp {
            get {
                return Status == CardStatus.PickedUp;
            }
        }

        public Tuple<bool, Vector3> ScaleChange { get; private set; }
        public Tuple<bool, Vector3> RotationChange { get; private set; }
        public Tuple<bool, Vector3> PositionChange { get; private set; }

        internal bool Played {
            get {
                return Status == CardStatus.Played;
            }
        }

        internal bool IsHovered {
            get {
                return Status == CardStatus.Hovered;
            }
        }

        private void UpdateLocation() {
            if (InHand) {
                var transformVectors = HandManager.Instance.GetTransformOfCard(cardId);
                PositionChange = new Tuple<bool, Vector3>(true, transformVectors.First);
                RotationChange = new Tuple<bool, Vector3>(true, transformVectors.Second);
                ScaleChange = new Tuple<bool, Vector3>(true, transformVectors.Third);
            } else if (Played) {
                PositionChange = new Tuple<bool, Vector3>(true, Settings.Animations.Cards.CardPlayedPosition);
                RotationChange = new Tuple<bool, Vector3>(true, Vector3.zero);
                ScaleChange = new Tuple<bool, Vector3>(true, Vector3.one);
            }
            UpdateBehaviour();
        }

        private void CheckUpdate(CardUpdate cardUpdate) {
            if (cardUpdate.Status == CardStatus.Creating) {
            }
            j = cardUpdate.Id;
            i = cardId;
            if (cardUpdate.Id == cardId)
            {
                Status = cardUpdate.Status == CardStatus.Unhovered ? CardStatus.InHand : cardUpdate.Status;
            }

            UpdateLocation();
            if (cardUpdate.Status == CardStatus.Creating) {
            }
        }
    }
}