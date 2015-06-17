using UnityEngine;
using System.Collections;
using BattleForBetelgeuse.View.Clickable;
using BattleForBetelgeuse.GUI.Hex;
using BattleForBetelgeuse.Actions;
using BattleForBetelgeuse.Interactable.MouseInteraction;
using System.Collections.Generic;
using BattleForBetelgeuse.GUI.Board;

namespace BattleForBetelgeuse {

  public class Mouse : MonoBehaviour {
    void Update() {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
    
      if(Physics.Raycast(ray, out hit)) {
        // Mouse Over
        MouseOver(hit);
        // Mouse Click
        if(Input.GetMouseButtonDown(0)) {
          Click(hit, ClickType.LEFTCLICK);
        } else if(Input.GetMouseButtonDown(1)) {
          Click(hit, ClickType.RIGHTCLICK);
        } 
      }
    }

    List<IMouseOverable> lastMouseovers = new List<IMouseOverable>();

    private void MouseOver(RaycastHit hit) {
      var mouseOverables = hit.transform.gameObject.GetComponents<IMouseOverable>();

      foreach(var mouseover in mouseOverables) {
        if(lastMouseovers.Contains(mouseover)) {
          lastMouseovers.Remove(mouseover);
        } else {
          mouseover.MouseOver();
        }
      }

      lastMouseovers.ForEach(mouseover => mouseover.MouseOut());

      lastMouseovers = new List<IMouseOverable>(mouseOverables);
    }

    private void Click(RaycastHit hit, ClickType clickType) {
      var clickables = hit.transform.gameObject.GetComponents<IClickable>();

      switch(clickType) {
      case ClickType.LEFTCLICK:
        foreach(var clickable in clickables) {
          clickable.LeftClicked();     
        }
        break;
      case ClickType.RIGHTCLICK:
        new RightClickAction();
        break;
      }


    }
  }
}
