namespace BattleForBetelgeuse.View.Clickable {
    using UnityEngine;

    [RequireComponent(typeof(Renderer))]
    public class RandomColorOnClickBehavior : ClickableViewBehaviour<RandomColorOnClickView> {
        private Renderer rend;

        public void UpdateColor(Color color) {
            this.rend.material.SetColor("_Color", color);
        }

        private void Start() {
            BehaviourUpdater.Behaviours.Add(this);
            this.Companion = new RandomColorOnClickView();
            this.rend = this.GetComponent<Renderer>();
        }

        public override void PushUpdate() {
            this.UpdateColor(this.Companion.Color);
        }
    }
}