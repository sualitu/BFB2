﻿namespace Assets.BattleForBetelgeuse.Management {
    using UnityEngine;

    public class Main : MonoBehaviour {
        // Use this for initialization
        public static PrefabManager PrefabManager { get; private set; }

        private void Start() {
            SU_SpaceSceneSwitcher.SwitchToRandom();
            PrefabManager = GetComponent<PrefabManager>();
            GridManager.Init();
        }

        // Update is called once per frame
        private void Update() {}
    }
}