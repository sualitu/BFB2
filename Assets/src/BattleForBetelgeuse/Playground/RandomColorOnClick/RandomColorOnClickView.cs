namespace Assets.Elements.Playground.RandomColorOnClick {
    using Assets.Flux.Interactable.Clickable;
    using Assets.GameManagement;

    using UnityEngine;

    public class RandomColorOnClickView : ClickableView {
        public RandomColorOnClickView() {
            RandomColorStore.Instance.Subscribe(ChangeColor);
            Color = Color.black;
        }

        public Color Color { get; set; }

        public void ChangeColor(Color color) {
            Color = color;
            BehaviourManager.Updated.Add(_id);
        }

        public override void LeftClicked() {
            new RandomColorOnClickAction();
        }

        public override void RightClicked() {
            new RandomColorOnClickAction();
        }
    }
}