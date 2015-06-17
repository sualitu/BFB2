using System;
using BattleForBetelgeuse.Stores;

namespace BattleForBetelgeuse.Actions {

  public abstract class ClickAction : UnpausableAction {
    public Click Click { get; set; }

    public ClickAction(Click click) : base() {
      Click = click;
    }
  }
}

