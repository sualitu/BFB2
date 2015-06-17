using BattleForBetelgeuse.Publishing;
using BattleForBetelgeuse.Dispatching;
using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.Stores {

  public abstract class PublishingStore<Topic> : Publisher<Topic>, IStore {
    public PublishingStore() {
      Dispatcher.Instance.Register(this);
    }

    public abstract void Update(Dispatchable action);
  }
}

