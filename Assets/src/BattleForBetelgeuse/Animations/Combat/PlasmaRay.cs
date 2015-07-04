namespace Assets.BattleForBetelgeuse.Animations.Combat {
    using Assets.BattleForBetelgeuse.Management;
    using Assets.GameManagement;

    internal class PlasmaRay : CombatAnimation {
        internal override int FramesBetweenShots {
            get {
                return 0;
            }
        }

        internal override int TotalShots {
            get {
                return 1;
            }
        }

        internal override CallBackStrategy CallBackStrategy {
            get {
                return new CallBackStrategy { Time = 2f, Timed = true };
            }
        }

        protected override void AnimateShot() {
            F3DPool.instance.Spawn(Main.PrefabManager.PlasmaRay,
                                   CurrentSocket.position,
                                   CurrentSocket.rotation,
                                   null);
            F3DAudioController.instance.PlasmaBeamLoop(CurrentSocket.position, CurrentSocket);
        }
    }
}