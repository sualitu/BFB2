using UnityEngine;
using System.Collections.Generic;
using BattleForBetelgeuse.TweenInteraction;

namespace BattleForBetelgeuse.View {

  public abstract class ViewBehaviour<T> : MonoBehaviour, UpdatableView where T : IView {
    private T companion;

    public T Companion { 
      get { return companion; } 
      internal set {
        companion = value;
        companion.SetId(uniqueId);
      } }
          
    int uniqueId;

    public int UniqueId() {
      return uniqueId;
    }

    void Awake() {      
      uniqueId = gameObject.GetInstanceID();
    }

    public abstract void PushUpdate();


  }
}

