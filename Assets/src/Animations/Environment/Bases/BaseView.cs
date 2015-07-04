namespace Assets.src.Animations.Environment.Bases {
    using System.Collections.Generic;

    using BattleForBetelgeuse.GameElements.Units;
    using BattleForBetelgeuse.View;

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