namespace Assets.GameManagement {
    using System.Collections.Generic;

    using Assets.Flux.Actions;
    using Assets.Flux.Interactable.MouseInteraction;

    using UnityEngine;

    public class MouseManager : MonoBehaviour {
        private List<IMouseOverable> lastMouseovers = new List<IMouseOverable>();

        private void Update() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                // Mouse Over
                MouseOver(hit);
                // Mouse Click
                if (Input.GetMouseButtonDown(0)) {
                    Click(hit, ClickType.LeftClick);
                } else if (Input.GetMouseButtonDown(1)) {
                    Click(hit, ClickType.RightClick);
                }
            }
        }

        private void MouseOver(RaycastHit hit) {
            var mouseOverables = hit.transform.gameObject.GetComponents<IMouseOverable>();

            foreach (var mouseover in mouseOverables) {
                if (lastMouseovers.Contains(mouseover)) {
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

            switch (clickType) {
                case ClickType.LeftClick:
                    foreach (var clickable in clickables) {
                        clickable.LeftClicked();
                    }
                    break;
                case ClickType.RightClick:
                    new RightClickAction();
                    break;
            }
        }
    }
}