using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.Stores;
using UnityEngine;

namespace BattleForBetelgeuse.View.Clickable {

  public class RandomColorOnClickView : ClickableView {
    public Color Color { get; set; }

    public RandomColorOnClickView() {
      RandomColorStore.Instance.Subscribe(ChangeColor);
      Color = Color.black;
    }

    public void ChangeColor(Color color) {
      this.Color = color;
      BehaviourUpdater.Updated.Add(_id);
    }

    public override void LeftClicked() {
      new RandomColorOnClickAction(new Click(ClickType.LEFTCLICK));
    }

    public override void RightClicked() {
      new RandomColorOnClickAction(new Click(ClickType.RIGHTCLICK));
    }
  }
}

