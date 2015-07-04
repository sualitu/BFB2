namespace Assets.Elements.Playground.RandomColorOnClick {
    using Assets.Flux.Interactable.Clickable;
    using Assets.GameManagement;

    using UnityEngine;

    [RequireComponent(typeof(Renderer))]
    public class RandomColorOnClickBehavior : ClickableViewBehaviour<RandomColorOnClickView> {
        private Renderer rend;

        public void UpdateColor(Color color) {
            rend.material.SetColor("_Color", color);
        }

        private void Start() {
            BehaviourManager.Behaviours.Add(this);
            Companion = new RandomColorOnClickView();
            rend = GetComponent<Renderer>();
        }

        public override void PushUpdate() {
            UpdateColor(Companion.Color);
        }
    }
}