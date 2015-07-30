namespace Assets.BattleForBetelgeuse.Management {
    using System.Collections;
    using System.Linq;

    using Assets.BattleForBetelgeuse.FluxElements.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.Player;
    using Assets.Flux.Dispatchers;

    using UnityEngine;

    public class Main : MonoBehaviour {
        public static PrefabManager PrefabManager;

        private void Awake() {
            var x = Dispatcher.Instance;
            var y = LocalPlayerStore.Instance;
            var z = CardStore.Instance;
        }

        private void Start() {
            SU_SpaceSceneSwitcher.SwitchToRandom();
            PrefabManager = GetComponent<PrefabManager>();
            GridManager.Init();
            StartCoroutine(DrawCardIn(1f));
            StartCoroutine(DrawCardIn(1.25f));
            StartCoroutine(DrawCardIn(1.5f));
        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Moar cards pliz.."))
            {
                new CardDrawnAction();
            }
        }

        private IEnumerator DrawCardIn(float i) {
            yield return new WaitForSeconds(i);
            new CardDrawnAction();
        }
    }
}