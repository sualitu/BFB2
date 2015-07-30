namespace Assets.Flux.Views {
    using System;

    using Assets.BattleForBetelgeuse.Management;

    public abstract class BehaviourUpdatingView : IView {
        internal Guid guid;

        public void SetId(Guid guid) {
            this.guid = guid;
        }

        public abstract void SetupSubscriptions();

        internal void UpdateBehaviour() {
            BehaviourManager.Updated.Add(guid);
        }
    }
}