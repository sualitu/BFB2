namespace Assets.GameManagement {
    using System.Collections.Generic;

    using Assets.Cards.UnitCards;
    using Assets.Elements.GameElements.Cards;
    using Assets.Elements.GameElements.Unit;
    using Assets.Elements.GUI.Grid.HexTile;
    using Assets.Utilities;

    using UnityEngine;

    public class UnitManager : MonoBehaviour {
        public static List<Tuple<HexCoordinate, string>> unitsToCreate = new List<Tuple<HexCoordinate, string>>();

        private void Start() {
            UnitStore.Init();
            new UnitCardPlayedAction(new HexCoordinate(14, 7), new TestUnit());
            new UnitCardPlayedAction(new HexCoordinate(5, 14), new TestUnit());
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