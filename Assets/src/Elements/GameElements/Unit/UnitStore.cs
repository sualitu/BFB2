namespace BattleForBetelgeuse.GameElements.Units {
    using System.Collections.Generic;
    using System.Linq;

    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.Cards.UnitCards;
    using BattleForBetelgeuse.GameElements.Cards;
    using BattleForBetelgeuse.GUI.Board;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.Stores;
    using BattleForBetelgeuse.Utilities;

    using ExtensionMethods;

    public class UnitStore : PublishingStore<List<UnitChange>> {
        private static UnitStore instance;

        private readonly Dictionary<HexCoordinate, Unit> units;

        private List<UnitChange> changes = new List<UnitChange>();

        private UnitStore() {
            this.units = new Dictionary<HexCoordinate, Unit>();
        }

        public static UnitStore Instance {
            get {
                if (instance == null) {
                    instance = new UnitStore();
                }
                return instance;
            }
        }

        private void BoardUpdate(BoardStatus status) {
            if (status.PreviousSelection == null || status.CurrentSelection == null) {
                return;
            }
            if (this.units.ContainsKey(status.PreviousSelection)) {
                this.MoveUnit(status.PreviousSelection,
                              status.CurrentSelection,
                              this.units[status.PreviousSelection],
                              status.Path);
            }
            this.Publish();
        }

        private void UpdateUnitLocation(HexCoordinate oldLocation, HexCoordinate newLocation) {
            var unit = this.units[oldLocation];
            this.units.Remove(oldLocation);
            this.units.Add(newLocation, unit);
        }

        public HexCoordinate LocationFromUnit(Unit unit) {
            return this.units.FirstOrDefault(pair => pair.Value == unit).Key;
        }

        private void MoveUnit(HexCoordinate from, HexCoordinate to, Unit unit, List<HexCoordinate> path) {
            if (this.IsUnitAtTile(to)) {
                this.UnitCollision(from, to, path);
            } else {
                this.UpdateUnitLocation(from, to);
                this.changes.Add(new UnitChange { From = from, To = to, Path = path });
            }
        }

        public void UnitCollision(HexCoordinate from, HexCoordinate to, List<HexCoordinate> path) {
            if (path.Count > 0) {
                path.RemoveFirst();
                if (path.Count > 0) {
                    var moveTo = path.GetFirst();
                    this.UpdateUnitLocation(from, moveTo);
                    this.changes.Add(new UnitChange {
                        From = from,
                        To = path.Count > 1 ? moveTo : null,
                        Path = path,
                        Attack = to
                    });
                    from = moveTo;
                }
            }
        }

        public bool IsUnitAtTile(HexCoordinate tile) {
            return this.units.ContainsKey(tile);
        }

        public Unit UnitAtTile(HexCoordinate tile) {
            return this.units.ContainsKey(tile) ? this.units[tile] : null;
        }

        internal override void SendMessage(Message msg) {
            msg(this.changes);
        }

        internal override void Publish() {
            lock (this.changes) {
                base.Publish();
                this.changes = new List<UnitChange>();
            }
        }

        private void UnitPlayed(HexCoordinate coordinate, UnitCard card) {
            UnitHandler.unitsToCreate.Add(new Tuple<HexCoordinate, string>(coordinate, card.PrefabPath));
        }

        public void HandleAction(Dispatchable action) {
            if (action is UnitCardPlayedAction) {
                var unitCardPlayedAction = (UnitCardPlayedAction)action;
                this.UnitPlayed(unitCardPlayedAction.Location, unitCardPlayedAction.Card);
                this.units.Add(unitCardPlayedAction.Location, (Unit.FromCard(unitCardPlayedAction.Card)));
            } else if (action is BoardUpdateAction) {
                var boardUpdateAction = (BoardUpdateAction)action;
                this.BoardUpdate(boardUpdateAction.BoardStatus);
            }
        }

        public override void Update(Dispatchable action) {
            this.HandleAction(action);
        }

        public static UnitStore Init() {
            return Instance;
        }
    }
}