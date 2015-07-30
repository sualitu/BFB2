namespace Assets.BattleForBetelgeuse.Playground.RandomColorOnClick {
    using Assets.BattleForBetelgeuse.Interactable.Clickable;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class RandomColorOnClickView : ClickableView {
        public RandomColorOnClickView() {
            Color = Color.black;
        }

        public Color Color { get; set; }

        public void ChangeColor(Color color) {
            Color = color;
            BehaviourManager.Updated.Add(guid);
        }

        public override void LeftClicked() {
            new RandomColorOnClickAction();
        }

        public override void RightClicked() {
            new RandomColorOnClickAction();
        }

        public override void SetupSubscriptions() {
            RandomColorStore.Instance.Subscribe(guid, ChangeColor);
        }
    }
}