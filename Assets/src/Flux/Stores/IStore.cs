namespace Assets.Flux.Stores {
    using Assets.Flux.Actions;

    public interface IStore {
        void UpdateStore(Dispatchable action);
    }
}