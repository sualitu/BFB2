namespace Assets.BattleForBetelgeuse.Management {
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
        }
    }
}