namespace Assets.BattleForBetelgeuse.FluxElements.Unit {
    using System;
    using System.Collections;

    using Assets.BattleForBetelgeuse.Animations;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Views;

    using UnityEngine;

    public class UnitHoloBehaviour : ViewBehaviour<UnitHoloView> {
        private UnitSpawnHolograph holo;

        public Guid Id { get; set; }

        private void Start() {
            holo = GetComponentInChildren<UnitSpawnHolograph>();
            BehaviourManager.Behaviours.Add(this);
            Companion = new UnitHoloView(Id);
        }

        public override void PushUpdate() {
            if (Companion.Location != null) {
                UpdateLocation();
            }
            if (!Companion.Alive) {
                GetComponent<TweenRotation>().enabled = false;
                holo.FadeOut();

                StartCoroutine(DestroyIn(1f));
            }
        }

        private IEnumerator DestroyIn(float f) {
            yield return new WaitForSeconds(f);
            Destroy(gameObject);
        }

        private void UpdateLocation() {
            transform.localPosition = GridManager.CalculateLocationFromHexCoordinate(Companion.Location);
        }
    }
}