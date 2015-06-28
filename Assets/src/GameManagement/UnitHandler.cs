namespace BattleForBetelgeuse {
    using System.Collections.Generic;

    using BattleForBetelgeuse.Cards.UnitCards;
    using BattleForBetelgeuse.GameElements.Cards;
    using BattleForBetelgeuse.GameElements.Units;
    using BattleForBetelgeuse.GUI.Hex;
    using BattleForBetelgeuse.Utilities;

    using UnityEngine;

    public class UnitHandler : MonoBehaviour {
        public static List<Tuple<HexCoordinate, string>> unitsToCreate = new List<Tuple<HexCoordinate, string>>();

        private void Start() {
            UnitStore.Init();
            new UnitCardPlayedAction(new HexCoordinate(1, 1), new TestUnit());
            new UnitCardPlayedAction(new HexCoordinate(2, 3), new TestUnit());
            new UnitCardPlayedAction(new HexCoordinate(5, 5), new BeamTestUnit());
        }

        private void Update() {
            lock (unitsToCreate) {
                foreach (var unit in unitsToCreate) {
                    var location = GridManager.CalculateLocationFromHexCoordinate(unit.First);
                    var prefab = Resources.Load(unit.Second);
                    var go = (GameObject)Instantiate(prefab, location, Quaternion.identity);
                    var unitBehaviour = go.AddComponent<UnitBehaviour>();
                    unitBehaviour.Coordinate = unit.First;
                }
                unitsToCreate.Clear();
            }
        }
    }
}