using UnityEngine;
using System.Collections.Generic;
using BattleForBetelgeuse.View;
using System.Linq;
using System;
using BattleForBetelgeuse.GUI;


namespace BattleForBetelgeuse {

  public class BehaviourUpdater : MonoBehaviour {

    public static Prefabs Prefabs { get; private set; }

    public static List<int> Updated = new List<int>();
    public static List<UpdatableView> Behaviours = new List<UpdatableView>();

    void Awake() {
      Prefabs = GetComponent<Prefabs>();
      GridManager.Init();
    }

    void Update() {
      lock(Updated) {
        foreach(var update in Updated) {
          var subjectsToChange = Behaviours.FindAll(b => b.UniqueId() == update).GetEnumerator();
          while(subjectsToChange.MoveNext()) { 
            subjectsToChange.Current.PushUpdate();
          }
        }          
        Updated.Clear();
      }

    }
  }
}