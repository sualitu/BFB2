using BattleForBetelgeuse.View.Clickable;
using BattleForBetelgeuse;
using UnityEngine;
using BattleForBetelgeuse.Interactable.MouseInteraction;

namespace BattleForBetelgeuse.GUI.Hex {

  public class HexTileBehaviour : ClickableViewBehaviour<HexTileView>, IMouseOverable {
    Renderer rend;
    Color NonTempColor = Settings.ColorSettings.TileBaseColor;

    public HexCoordinate Coordinate { get; set; }
    
    public void UpdateColor(Color color) {
      NonTempColor = color;
      rend.material.SetColor("_Color", color);
    }

    public void ColorTemporarily(Color color) {
      rend.material.SetColor("_Color", color);
    }

    public void MouseOver() {
      ColorTemporarily(Color.red);
    }

    public void MouseOut() {
      UpdateColor(NonTempColor);
    }

    void Start() {
      BehaviourUpdater.Behaviours.Add(this);
      Companion = new HexTileView();
      Companion.Coordinate = Coordinate;
      rend = GetComponent<Renderer>();
      gameObject.name = "Hex:" + UniqueId();
      gameObject.tag = "HexTile";
    }

    public override void PushUpdate() {
      UpdateColor(Companion.Color);
    }
  }
}