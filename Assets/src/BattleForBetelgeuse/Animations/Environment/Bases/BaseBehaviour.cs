﻿namespace Assets.Animations.Environment.Bases {
    using Assets.Flux.Views;
    using Assets.GameManagement;
    
    using UnityEngine;

    public class BaseBehaviour : ViewBehaviour<BaseView>, IBase {
        public GameObject Animation;

        public void DoSpawnAnimation() {
            Instantiate(Animation, transform.position, transform.rotation);
        }

        private void Start()
        {
            BehaviourManager.Behaviours.Add(this);
            gameObject.name = "Unit:" + UniqueId();
            gameObject.tag = "Unit";
            Companion = new BaseView();
        }

        public override void PushUpdate() {
            DoSpawnAnimation();
        }
    }
}