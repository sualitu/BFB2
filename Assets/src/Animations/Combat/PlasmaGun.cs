namespace BattleForBetelgeuse.Animations.Combat {
    using UnityEngine;

    public class PlasmaGun : CombatAnimation {
        private Transform target;

        private int shotsFired;

        internal override int FramesBetweenShots {
            get {
                return 8;
            }
        }

        public override void CombatWith(Transform target) {
            this.target = target;
            Reset();
        }

        private void Reset() {
            Shooting = true;
            shotsFired = 0;
        }

        private void Update() {
            if (Shooting && ShootAgain()) {
                var offset = Quaternion.Euler(Random.onUnitSphere);
                F3DPool.instance.Spawn(BehaviourUpdater.Prefabs.PlasmaMuzzle,
                                       CurrentSocket.position,
                                       CurrentSocket.rotation,
                                       CurrentSocket);

                F3DAudioController.instance.PlasmaGunShot(CurrentSocket.position);
                AdvanceSocket();
                shotsFired++;
                if (shotsFired > 8) {
                    Shooting = false;
                }
            }
        }
    }
}