namespace Assets.Flux.Views {
    using System;

    using UnityEngine;

    public abstract class ViewBehaviour<T> : MonoBehaviour, IUpdatableView
        where T : IView {
        private T companion;

        private Guid uniqueId;

        public T Companion {
            get {
                return companion;
            }
            internal set {
                value.SetId(uniqueId);
                value.SetupSubscriptions();
                companion = value;
            }
        }

        public Guid UniqueId() {
            return uniqueId;
        }

        public abstract void PushUpdate();

        public virtual void Awake() {
            uniqueId = Guid.NewGuid();
            CustomAwake();
        }

        protected virtual void CustomAwake() {}
    }
}