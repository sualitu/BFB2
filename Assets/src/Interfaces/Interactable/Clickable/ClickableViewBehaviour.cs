using UnityEngine;
using BattleForBetelgeuse.Interactable.MouseInteraction;

namespace BattleForBetelgeuse.View.Clickable {

  public abstract class ClickableViewBehaviour<T> : ViewBehaviour<T>, IClickable where T : ClickableView {
    public void LeftClicked() {
      Companion.LeftClicked();
    }
    
    public void RightClicked() {
      Companion.RightClicked();
    }
  }
}