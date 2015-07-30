namespace Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Flux.Actions;
    using Assets.Flux.Stores;
    using Assets.Utilities.PathFinding;

    public class BoardStore : PublishingStore<BoardStatus> {
        private static BoardStore instance;

        private readonly HashSet<Guid> blockingCards = new HashSet<Guid>();

        private readonly BoardStatus status;

        private BoardStore() {
            status = new BoardStatus();
        }

        public static BoardStore Instance {
            get {
                if (instance == null) {
                    instance = new BoardStore();
                }
                return instance;
            }
        }

        internal override void SendMessage(Message msg) {
            msg(status);
        }

        private void UpdateStatus(HexTileAction action) {
            if (action is HexTileClickedAction) {
                HandleClickAction(action);
            }
        }

        private void HandleClickAction(HexTileAction action) {
            var coordinate = action == null ? null : action.Coordinate;
            if (coordinate != null && status.CardOnBoard.HasValue) {
                PutCardOnBoardIntoPlay(coordinate);
                return;
            }
            if (coordinate != null && coordinate.Equals(status.CurrentSelection)) {
                return;
            }
            status.PreviousSelection = status.CurrentSelection;
            status.CurrentSelection = coordinate;
            if (status.PreviousSelection != null && status.CurrentSelection != null) {
                try {
                    status.Path = AStar<HexCoordinate>.FindPath(status.PreviousSelection, status.CurrentSelection);
                } catch {
                    status.Path = new List<HexCoordinate>();
                }
            }
            new BoardUpdateAction(status.Copy());
        }

        public override void UpdateStore(Dispatchable action) {
            if (action is CardAction) {
                HandleCardAction(action);
            }
            if (blockingCards.Count < 1) {
                if (action is HexTileAction) {
                    var hexTileAction = (HexTileAction)action;
                    UpdateStatus(hexTileAction);
                    Publish();
                } else if (action is RightClickAction) {
                    Deselect();
                }
            }
        }

        private void PutCardOnBoardIntoPlay(HexCoordinate coordinate) {
            if (status.CardOnBoard != null) {
                var cardOnBoard = CardStore.Instance.Cards[status.CardOnBoard.Value];
                if (cardOnBoard is UnitCard) {
                    new UnitSpawnedAction(coordinate, cardOnBoard as UnitCard);
                    new CardPlayedAction(status.CardOnBoard.Value);
                    status.CardOnBoard = null;
                    Publish();
                }
            }
        }

        private void HandleCardAction(Dispatchable action) {
            var cardAction = (CardAction)action;
            if (cardAction is CardHovederedAction || cardAction is CardPickedUpAction) {
                blockingCards.Add(cardAction.Id);
            } else if (cardAction is CardUnhovederedAction) {
                blockingCards.Remove(cardAction.Id);
            } else if (cardAction is CardPutDownAction) {
                var putDownAction = (CardPutDownAction)cardAction;
                if (!CardStore.IsPutDownOnBoard(putDownAction)) {
                    blockingCards.Remove(cardAction.Id);
                }
            } else if (cardAction is CardOnBoardAction) {
                if (status.CardOnBoard != null) {
                    new CardToHandAction(status.CardOnBoard.Value);
                }
                blockingCards.Remove(cardAction.Id);
                status.CardOnBoard = (cardAction as CardOnBoardAction).Id;
                UnitHoloManager.HolosToCreate.Add((cardAction as CardOnBoardAction).Id);
                Deselect();
            } else if (cardAction is CardToHandAction) {
                if (cardAction.Id == status.CardOnBoard) {
                    status.CardOnBoard = null;
                    Publish();
                }
            }
        }

        internal void PublishBoard() {
            Publish();
        }

        public void Deselect() {
            HandleClickAction(null);
            Publish();
        }
    }
}