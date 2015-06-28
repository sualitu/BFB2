namespace BattleForBetelgeuse.View.Clickable {
    using BattleForBetelgeuse.Actions;
    using BattleForBetelgeuse.Stores;

    using UnityEngine;

    public class RandomColorOnClickView : ClickableView {
        public RandomColorOnClickView() {
            RandomColorStore.Instance.Subscribe(this.ChangeColor);
            this.Color = Color.black;
        }

        public Color Color { get; set; }

        public void ChangeColor(Color color) {
            this.Color = color;
            BehaviourUpdater.Updated.Add(this._id);
        }

        public override void LeftClicked() {
            new RandomColorOnClickAction();
        }

        public override void RightClicked() {
            new RandomColorOnClickAction();
        }
    }
}