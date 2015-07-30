namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using System;
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class UnitHoloManager : MonoBehaviour {
        public static List<Guid> HolosToCreate;

        private void Awake() {
            HolosToCreate = new List<Guid>();
        }

        private void Start() {
            UnitStore.Init();
        }

        public static UnitHoloBehaviour CurrentHolo { get; private set; }

        private void Update() {
            foreach (var id in HolosToCreate) {
                var card = CardStore.Instance.Cards[id];
                var prefab = Resources.Load(card.PreSpawnAnimationPrefab);
                var location = GridManager.CalculateLocationFromHexCoordinate(MouseStore.RecentMouseOver);
                var go = (GameObject)Instantiate(prefab, location, Quaternion.identity);
                var holo = go.AddComponent<UnitHoloBehaviour>();
                holo.Id = id;
                CurrentHolo = holo;
            }
            HolosToCreate.Clear();
        }
    }
}