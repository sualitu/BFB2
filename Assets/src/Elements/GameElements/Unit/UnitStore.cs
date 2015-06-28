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
            units = new Dictionary<HexCoordinate, Unit>();
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
            if (units.ContainsKey(status.PreviousSelection)) {
                MoveUnit(status.PreviousSelection, status.CurrentSelection, units[status.PreviousSelection], status.Path);
            }
            Publish();
        }

        private void UpdateUnitLocation(HexCoordinate oldLocation, HexCoordinate newLocation) {
            var unit = units[oldLocation];
            units.Remove(oldLocation);
            units.Add(newLocation, unit);
        }

        public HexCoordinate LocationFromUnit(Unit unit) {
            return units.FirstOrDefault(pair => pair.Value == unit).Key;
        }

        private void MoveUnit(HexCoordinate from, HexCoordinate to, Unit unit, List<HexCoordinate> path) {
            if (IsUnitAtTile(to)) {
                UnitCollision(from, to, path);
            } else {
                UpdateUnitLocation(from, to);
                changes.Add(new UnitChange { From = from, To = to, Path = path });
            }
        }

        public void UnitCollision(HexCoordinate from, HexCoordinate to, List<HexCoordinate> path) {
            if (path.Count > 0) {
                path.RemoveFirst();
                if (path.Count > 0) {
                    var moveTo = path.GetFirst();
                    UpdateUnitLocation(from, moveTo);
                    changes.Add(new UnitChange {
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
            return units.ContainsKey(tile);
        }

        public Unit UnitAtTile(HexCoordinate tile) {
            return units.ContainsKey(tile) ? units[tile] : null;
        }

        internal override void SendMessage(Message msg) {
            msg(changes);
        }

        internal override void Publish() {
            lock (changes) {
                base.Publish();
                changes = new List<UnitChange>();
            }
        }

        private void UnitPlayed(HexCoordinate coordinate, UnitCard card) {
            UnitHandler.unitsToCreate.Add(new Tuple<HexCoordinate, string>(coordinate, card.PrefabPath));
        }

        public void HandleAction(Dispatchable action) {
            if (action is UnitCardPlayedAction) {
                var unitCardPlayedAction = (UnitCardPlayedAction)action;
                UnitPlayed(unitCardPlayedAction.Location, unitCardPlayedAction.Card);
                units.Add(unitCardPlayedAction.Location, (Unit.FromCard(unitCardPlayedAction.Card)));
            } else if (action is BoardUpdateAction) {
                var boardUpdateAction = (BoardUpdateAction)action;
                BoardUpdate(boardUpdateAction.BoardStatus);
            }
        }

        public override void Update(Dispatchable action) {
            HandleAction(action);
        }

        public static UnitStore Init() {
            return Instance;
        }

        public void RemoveUnit(Unit unit) {
            var toRemove = units.Where(pair => pair.Value.Equals(unit)).Select(pair => pair.Key).ToList();
            foreach (var key in toRemove) {
                units.Remove(key);
            }
        }
    }
}