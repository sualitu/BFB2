namespace Assets.Flux.Views {
    using UnityEngine;

    public abstract class ViewBehaviour<T> : MonoBehaviour, IUpdatableView
        where T : IView {
        private T companion;

        private int uniqueId;

        public T Companion {
            get {
                return companion;
            }
            internal set {
                companion = value;
                companion.SetId(uniqueId);
            }
        }

        public int UniqueId() {
            return uniqueId;
        }

        public abstract void PushUpdate();

        private void Awake() {
            uniqueId = gameObject.GetInstanceID();
        }
    }
}