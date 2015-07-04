namespace Assets.Flux.Views {
    public interface IUpdatableView {
        int UniqueId();

        void PushUpdate();
    }
}