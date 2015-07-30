namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using System;

    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.Board;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.Flux.Views;

    public class UnitHoloView : BehaviourUpdatingView {
        private readonly Guid id;

        public UnitHoloView(Guid id) {
            this.id = id;
            Alive = true;
        }

        internal HexCoordinate Location { get; set; }
        internal bool Alive { get; set; }

        private void CardOnBoard(BoardStatus input) {
            if (input.CardOnBoard != id) {
                Alive = false;
                MouseStore.Instance.Unsubscribe(guid);
                BoardStore.Instance.Unsubscribe(guid);
            }
            UpdateBehaviour();
        }

        private void MouseMovement(MouseOverStatus input) {
            Location = input.CurrentMouseOver;
            UpdateBehaviour();
        }

        public override void SetupSubscriptions() {
            MouseStore.Instance.Subscribe(guid, MouseMovement);
            BoardStore.Instance.Subscribe(guid, CardOnBoard);
        }
    }
}