namespace Assets.BattleForBetelgeuse.Interactable.Clickable {
    using Assets.Flux.Views;

    public abstract class ClickableView : BehaviourUpdatingView {
        public virtual void LeftClicked() {}

        public virtual void RightClicked() {}

        public virtual void MouseOver() {}
    }
}