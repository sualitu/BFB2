using BattleForBetelgeuse.Actions;

namespace BattleForBetelgeuse.Stores {

  public interface IStore {
    void Update(Dispatchable action);
  }
}