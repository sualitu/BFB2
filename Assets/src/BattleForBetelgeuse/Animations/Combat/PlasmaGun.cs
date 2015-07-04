namespace Assets.Animations.Combat {
    using Assets.GameManagement;

    using UnityEngine;

    public class PlasmaGun : CombatAnimation {
        internal override int FramesBetweenShots {
            get {
                return Random.Range(7, 9);
            }
        }

        internal override int TotalShots {
            get {
                return 15;
            }
        }

        internal override CallBackStrategy CallBackStrategy {
            get {
                return new CallBackStrategy { Time = 0f, Timed = false };
            }
        }

        protected override void AnimateShot() {
            F3DPool.instance.Spawn(Main.PrefabManager.PlasmaMuzzle,
                                   CurrentSocket.position,
                                   CurrentSocket.rotation,
                                   null);

            F3DAudioController.instance.PlasmaGunShot(CurrentSocket.position);
        }
    }
}