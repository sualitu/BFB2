namespace Assets.BattleForBetelgeuse.Management {
    using System.Collections.Generic;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Utilities;

    using UnityEngine;

    public class UnitManager : MonoBehaviour {
        public static List<Tuple<HexCoordinate, string>> UnitsToCreate;

        void Awake() {
            UnitsToCreate = new List<Tuple<HexCoordinate, string>>();
        }

        private void Start() {
            UnitStore.Init();
        }

        private void Update() {
            foreach (var unit in UnitsToCreate) {
                var location = GridManager.CalculateLocationFromHexCoordinate(unit.First);
                var prefab = Resources.Load(unit.Second);
                var go = (GameObject)Instantiate(prefab, location, UnitHoloManager.CurrentHolo.transform.rotation);
                var unitBehaviour = go.AddComponent<UnitBehaviour>();
                unitBehaviour.Coordinate = unit.First;
            }
            UnitsToCreate.Clear();
        }
    }
}