namespace Assets.BattleForBetelgeuse.Animations.Environment.Bases {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Flux.Views;

    public class BaseView : BehaviourUpdatingView {
        public void CheckSpawned(List<UnitChange> changes) {
            foreach (var change in changes) {
                if (change.NewUnitSpawned) {
                    UpdateBehaviour();
                }
            }
        }

        public override void SetupSubscriptions() {
            UnitStore.Instance.Subscribe(guid, CheckSpawned);
        }
    }
}