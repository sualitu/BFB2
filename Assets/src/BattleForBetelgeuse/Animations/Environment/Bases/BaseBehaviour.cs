namespace Assets.BattleForBetelgeuse.Animations.Environment.Bases {
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Views;

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