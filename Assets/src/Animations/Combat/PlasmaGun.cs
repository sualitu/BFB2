﻿namespace BattleForBetelgeuse.Animations.Combat {
    using UnityEngine;

    public class PlasmaGun : CombatAnimation {
        
        internal override int FramesBetweenShots {
            get {
                return Random.Range(7,9);
            }
        }

        internal override int TotalShots {
            get {
                return 9;
            }
        }

        protected override void AnimateShot() {
            F3DPool.instance.Spawn(BehaviourUpdater.Prefabs.PlasmaMuzzle,
                                   CurrentSocket.position,
                                   CurrentSocket.rotation,
                                   null);

            F3DAudioController.instance.PlasmaGunShot(CurrentSocket.position);
        }
    }
}