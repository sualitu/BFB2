namespace Assets.Flux.Stores {
    using Assets.Flux.Actions;

    public interface IStore {
        void Update(Dispatchable action);
    }
}