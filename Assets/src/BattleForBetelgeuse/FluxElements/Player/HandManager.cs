namespace Assets.BattleForBetelgeuse.FluxElements.Player {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Utilities;

    using UnityEngine;

    public class HandManager {
        private readonly Guid guid;

        private const int CardRoom = 100;

        private const float CardRotationRate = -10f;

        private const int CardScalePoint = 3;

        private const float CardScaleRate = 0.05f;

        private const float LoweringRate = 10f;

        private static HandManager instance;

        private readonly float YPosition;

        private Guid? hovered;

        private HandManager() {
            guid = Guid.NewGuid();
            LocalPlayerStore.Instance.Subscribe(guid, UpdateHand);
            CardStore.Instance.Subscribe(guid, UpdateHovered);
            YPosition = -Screen.height / 2 * .6f;
        }

        public List<Guid> Hand { get; private set; }

        public static HandManager Instance {
            get {
                if (instance == null) {
                    instance = new HandManager();
                }
                return instance;
            }
        }

        private void UpdateHovered(CardUpdate input) {
            if (input.Status == CardStatus.Hovered) {
                hovered = input.Id;
            } else if (input.Status != CardStatus.Hovered && hovered.HasValue && input.Id == hovered.Value) {
                hovered = null;
            }
        }

        private void UpdateHand(LocalPlayer input) {
            Hand = input.Hand;
        }

        public static void Init() {
            instance = new HandManager();
        }

        public Triple<Vector3, Vector3, Vector3> GetTransformOfCard(Guid id)
        {
            return CalculateTransformForCardIndex(Hand.IndexOf(id));
        }

        private Triple<Vector3, Vector3, Vector3> CalculateTransformForCardIndex(int index) {
            // The changes in vectors based on number of cards.
            var ratio = Hand.Count > CardScalePoint ? (Hand.Count - CardScalePoint) * CardScaleRate : 0f;
            // Does the hand have an event number of cards?
            var eventHandCount = Hand.Count % 2 == 0;
            // The index relative to the center of the hand
            var relativeIndex = index - (Hand.Count / 2);
            // x position
            var x = relativeIndex * CardRoom;
            // Adjusted if we have an event number of cards
            var adjustedX = x + (eventHandCount ? 50 : 0);
            // Scale down the x if we have too many cards
            var scaledX = adjustedX * (1 - ratio);

            // y position
            var y = YPosition;
            // We need to lower the position of the y.
            float yAdjustment;
            if (!eventHandCount) {
                // If we have an uneven number of cards, we skip the center card(index 0) and lower the rest by according to their relative index
                yAdjustment = relativeIndex != 0 ? (Math.Abs(relativeIndex)) * (-LoweringRate) : 0;
            } else {
                // If we have an even number of cards we skip the two center cards (index 0 and -1).
                // Cards with negative index are brought one closer (as the center is 0 and -1).
                var adjustmentIndex = Math.Abs(relativeIndex) - (relativeIndex < 0 ? 1 : 0);
                yAdjustment = relativeIndex != 0 && relativeIndex != -1 ? (adjustmentIndex) * (-LoweringRate) : 0;
            }
            // Apply the adjustment
            var adjustedY = y + yAdjustment;
            // We have found the position! Z is just 0 as this is a 2D interface.
            var position = new Vector3(scaledX, adjustedY, 0f);

            // rotation base is according to the relative index
            var rotationZ = relativeIndex * CardRotationRate;
            // We need to adjust it if rotation slightly if we have an even number of cards
            var adjustedRotationZ = rotationZ + (eventHandCount ? CardRotationRate / 2 : 0);
            // Scaling down if we have too many cards
            var scaledRotationZ = adjustedRotationZ * (1 - (2 * ratio));
            var rotation = new Vector3(0f, 0f, scaledRotationZ);

            // Scale is a setting, adjusted by the ratio.
            var scale = new Vector3(Settings.Animations.Cards.CardSize - ratio,
                                    Settings.Animations.Cards.CardSize - ratio,
                                    Settings.Animations.Cards.CardSize - ratio);

            // If a card is hovered we need to adjust further.
            if (hovered.HasValue) {
                var hoveredIndex = Hand.IndexOf(hovered.Value);
                if (hoveredIndex != index) {
                    // If the hovered card is not this card
                    // The difference between this index, and the card hovered
                    float indexDiff = Mathf.Abs(hoveredIndex - index);
                    // Move the card to the side.
                    // The closer to the hovered card we are teh furhter we move.
                    var adjustment = ((Hand.Count - indexDiff) * 2 / Hand.Count) * (CardRoom / 2);
                    // Apply the adjustment.
                    position += new Vector3((hoveredIndex > index ? -1f : 1f) * adjustment, 0f, 0f);
                } else {
                    // Otherwise bring forward the card
                    position += new Vector3(0f, 100f, 0f);
                    rotation = new Vector3(0f, 0f, 0f);
                    scale = new Vector3(.8f, .8f, .8f);
                }
            }
            return new Triple<Vector3, Vector3, Vector3>(position, rotation, scale);
        }
    }
}