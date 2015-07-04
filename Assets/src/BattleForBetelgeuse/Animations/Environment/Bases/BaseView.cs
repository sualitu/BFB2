namespace Assets.BattleForBetelgeuse.Animations.Environment.Bases {
    using System.Collections.Generic;

    using Assets.BattleForBetelgeuse.FluxElements.Unit;
    using Assets.Flux.Views;

    public class BaseView : BehaviourUpdatingView {
        public BaseView() {
            UnitStore.Instance.Subscribe(CheckSpawned);
        }

        public void CheckSpawned(List<UnitChange> changes) {
            foreach (var change in changes) {
                if (change.Owner != null) {
                    UpdateBehaviour();
                }
            }
        }
    }
}