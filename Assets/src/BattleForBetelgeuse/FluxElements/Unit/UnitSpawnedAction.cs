namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using Assets.BattleForBetelgeuse.Cards.UnitCards;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Actions;

    using UnityEngine;

    public class UnitSpawnedAction : Dispatchable {
        public HexCoordinate Location { get; set; }
        public UnitCard Card { get; set; }

        public UnitSpawnedAction(HexCoordinate location, UnitCard card) {
            Location = location;
            Card = card;
        }
    }
}