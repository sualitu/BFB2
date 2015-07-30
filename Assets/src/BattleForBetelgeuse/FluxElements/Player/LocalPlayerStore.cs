namespace Assets.BattleForBetelgeuse.FluxElements.Player {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Actions;
    using Assets.Utilities;

    public class LocalPlayerStore : PlayerStore<LocalPlayer> {
        private static LocalPlayerStore instance;

        private int maxMana;

        private LocalPlayerStore() {
            maxMana = 0;
            Player = new LocalPlayer {
                Hand = new List<Guid>(),
                Deck = new List<Card> { new TestUnit(), new BeamTestUnit(), new TestUnitGreen(), new TestUnit(), new BeamTestUnit(), new TestUnitGreen(), new TestUnit(), new BeamTestUnit(), new TestUnitGreen(), new TestUnit(), new BeamTestUnit(), new TestUnitGreen(), new TestUnit(), new BeamTestUnit(), new TestUnitGreen() }
            };
        }

        public static LocalPlayerStore Instance {
            get {
                if (instance == null) {
                    instance = new LocalPlayerStore();
                }
                return instance;
            }
        }

        public override void UpdateStore(Dispatchable action) {
            if (action is CardDrawnAction) {
                var cardDrawnAction = (CardDrawnAction) action;
                if (!cardDrawnAction.Opponent) {
                    Draw();
                }
                Publish();
            }
            else if (action is CardPlayedAction) {
                var cardPlayedAction = action as CardPlayedAction;
                Player.Hand.Remove(cardPlayedAction.Id);
            }
        }

        internal override void SendMessage(Message msg) {
            msg(Player);
        }

        public override void Draw() {
            if (Player.DeckCount > 0) {
                var drawn = Player.Deck.GetAndRemoveRandom();
                if (Player.HandCount < Settings.GameSettings.MaxHandSize) {
                    var guid = Guid.NewGuid();
                    Player.Hand.Add(guid);
                    CardStore.Instance.CardDrawn(guid, drawn);
                } else {
                    UnityEngine.Debug.Log("Players hand is full!");
                    // Animate card being discarded.
                }
            } else {
                UnityEngine.Debug.Log("Player has no cards!");
                // Penalize player for having no cards
            }
        }

        public override void NewTurn() {
            if (Settings.GameSettings.MaxMana > maxMana) {
                maxMana++;
            }
            Player.ManaCount = maxMana;
            Draw();
        }
    }
}