using System;
using BattleForBetelgeuse.Stores;
using BattleForBetelgeuse.View.Clickable;

namespace BattleForBetelgeuse.Actions {

  public enum ClickType { 
    RIGHTCLICK, 
    LEFTCLICK, 
    DOUBLELEFTCLICK 
  }

  public class Click {
    public ClickType Type { get; private set; }

    public Click(ClickType type) {
      Type = type;
    }
  }  
}
