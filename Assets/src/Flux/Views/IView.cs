namespace Assets.Flux.Views {
    using System;

    public interface IView {
        void SetId(Guid guid);

        void SetupSubscriptions();
    }
}