namespace Assets.Flux.Stores {
    using Assets.Flux.Actions;
    using Assets.Flux.Dispatchers;

    public abstract class PublishingStore<TTopic> : Publisher<TTopic>, IStore {
        public PublishingStore() {
            Dispatcher.Instance.Register(this);
        }

        public abstract void Update(Dispatchable action);
    }
}