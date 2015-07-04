namespace Assets.src.Animations.Environment.Bases {
    using BattleForBetelgeuse;
    using BattleForBetelgeuse.View;

    using UnityEngine;

    internal class BaseBehaviour : ViewBehaviour<BaseView>, IBase {
        public GameObject Animation;

        public void DoSpawnAnimation() {
            Instantiate(Animation, transform.position, transform.rotation);
        }

        private void Start()
        {
            BehaviourUpdater.Behaviours.Add(this);
            gameObject.name = "Unit:" + UniqueId();
            gameObject.tag = "Unit";
            Companion = new BaseView();
        }

        public override void PushUpdate() {
            DoSpawnAnimation();
        }
    }
}