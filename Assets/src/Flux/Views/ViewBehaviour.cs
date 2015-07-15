namespace Assets.Flux.Views {
    using System;

    using UnityEngine;

    public abstract class ViewBehaviour<T> : MonoBehaviour, IUpdatableView
        where T : IView {
        private T companion;

        private readonly Guid uniqueId = Guid.NewGuid();

        public T Companion {
            get {
                return companion;
            }
            internal set {
                companion = value;
                companion.SetId(uniqueId);
            }
        }

        public Guid UniqueId() {
            return uniqueId;
        }

        public abstract void PushUpdate();
    }
}