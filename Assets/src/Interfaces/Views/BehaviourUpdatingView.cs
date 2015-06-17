using UnityEngine;

namespace BattleForBetelgeuse.View {

  public class BehaviourUpdatingView : IView {
    
    internal int _id;
    
    public void SetId(int id) {
      _id = id;
    }
    
    internal void UpdateBehaviour() {
      BehaviourUpdater.Updated.Add(_id);
    }
  }
}

