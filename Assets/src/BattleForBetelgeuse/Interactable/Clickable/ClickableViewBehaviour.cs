namespace Assets.BattleForBetelgeuse.Interactable.Clickable {
    using Assets.BattleForBetelgeuse.Interactable.MouseInteraction;
    using Assets.Flux.Views;

    public abstract class ClickableViewBehaviour<T> : ViewBehaviour<T>, IClickable
        where T : ClickableView {
        public void LeftClicked() {
            Companion.LeftClicked();
        }

        public void RightClicked() {
            Companion.RightClicked();
        }
    }
}