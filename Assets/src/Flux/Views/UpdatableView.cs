namespace Assets.Flux.Views {
    using System;

    public interface IUpdatableView {
        Guid UniqueId();

        void PushUpdate();
    }
}