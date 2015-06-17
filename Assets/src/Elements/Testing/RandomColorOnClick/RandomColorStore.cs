using BattleForBetelgeuse.View;
using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.Dispatching;
using System.Collections.Generic;
using UnityEngine;

using Rnd = System.Random;
using BattleForBetelgeuse.Publishing;

namespace BattleForBetelgeuse.Stores {

  public class RandomColorStore : PublishingStore<Color> {

    Color color;
    private static RandomColorStore instance;
    
    public static RandomColorStore Instance { 
      get {
        if(instance == null) {
          instance = new RandomColorStore();
        }
        return instance;
      } 
    }

    Rnd rnd;

    private RandomColorStore() : base() {
      color = Color.black;
      rnd = new Rnd();
    }

    internal override void SendMessage(Message msg) {
      msg(color);
    }

    public override void Update(Dispatchable action) {
      if(action is RandomColorOnClickAction) {
        color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
        Publish();
      }
    }
  }
}

