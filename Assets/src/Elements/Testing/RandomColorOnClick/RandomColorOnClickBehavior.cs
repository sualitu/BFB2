using UnityEngine;
using System;

namespace BattleForBetelgeuse.View.Clickable {

  [RequireComponent(typeof(Renderer))]
  public class RandomColorOnClickBehavior : ClickableViewBehaviour<RandomColorOnClickView> {
    Renderer rend;

    public void UpdateColor(Color color) {
      rend.material.SetColor("_Color", color);
    }

    void Start() {
      BehaviourUpdater.Behaviours.Add(this);
      Companion = new RandomColorOnClickView();
      rend = GetComponent<Renderer>();
    }

    public override void PushUpdate() {
      UpdateColor(Companion.Color);
    }

  }
}

