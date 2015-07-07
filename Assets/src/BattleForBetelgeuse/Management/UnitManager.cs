﻿namespace Assets.BattleForBetelgeuse.Management {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Utilities;

    using UnityEngine;

    public class UnitManager : MonoBehaviour {
        public static List<Tuple<HexCoordinate, string>> UnitsToCreate = new List<Tuple<HexCoordinate, string>>();

        private void Start() {
            UnitStore.Init();
            new UnitCardPlayedAction(new HexCoordinate(13, 7), new TestUnit());
            new UnitCardPlayedAction(new HexCoordinate(5, 13), new TestUnit());
            new UnitCardPlayedAction(new HexCoordinate(5, 5), new BeamTestUnit());
        }

        private void Update() {
            foreach (var unit in UnitsToCreate) {
                var location = GridManager.CalculateLocationFromHexCoordinate(unit.First);
                var prefab = Resources.Load(unit.Second);
                var go = (GameObject)Instantiate(prefab, location, Quaternion.identity);
                var unitBehaviour = go.AddComponent<UnitBehaviour>();
                unitBehaviour.Coordinate = unit.First;
            }
            UnitsToCreate.Clear();
        }
    }
}