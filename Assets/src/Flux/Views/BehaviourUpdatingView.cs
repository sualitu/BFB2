namespace Assets.Flux.Views {
    using System;

    using Assets.BattleForBetelgeuse.Management;

    public class BehaviourUpdatingView : IView {
        internal Guid Id;

        public void SetId(Guid id)
        {
            Id = id;
        }

        internal void UpdateBehaviour() {
            BehaviourManager.Updated.Add(Id);
        }
    }
}