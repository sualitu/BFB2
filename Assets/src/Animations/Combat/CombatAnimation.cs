namespace BattleForBetelgeuse.Animations.Combat {
    using System.Collections.Generic;
    using System.Linq;

    using BattleForBetelgeuse.Constants;

    using UnityEngine;

    public abstract class CombatAnimation : MonoBehaviour {
        public delegate void PostCombatCallBack();

        private int currentSocket;

        private int frameCount;

        protected int ShotsFired;

        private List<Transform> sockets;

        protected Vector3 Target;

        internal bool Shooting { get; set; }
        internal abstract int FramesBetweenShots { get; }
        internal abstract int TotalShots { get; }

        public Transform CurrentSocket {
            get {
                return sockets[currentSocket];
            }
        }

        public PostCombatCallBack CallBack { get; set; }

        private void Awake() {
            Shooting = false;
            currentSocket = 0;
            frameCount = 0;
            sockets = new List<Transform>();
            sockets.AddRange(
                             transform.Cast<Transform>()
                                      .Where(child => child.tag == Constants.Tags.CombatAnimationSocket));
        }

        public void AdvanceSocket() {
            currentSocket = (currentSocket + 1) % sockets.Count;
        }

        public void CombatWith(Vector3 target, PostCombatCallBack callBack) {
            CallBack = callBack;
            Target = target;
            Reset();
            Setup();
        }

        private void Reset() {
            Shooting = true;
            ShotsFired = 0;
        }

        protected virtual void Setup() {}

        internal bool ShootAgain() {
            frameCount ++;
            if (FramesBetweenShots > frameCount) {
                return false;
            }
            frameCount = 0;
            return true;
        }

        protected abstract void AnimateShot();

        private void Update() {
            if (Shooting && ShootAgain()) {
                ShotsFired++;
                if (ShotsFired >= TotalShots) {
                    Shooting = false;
                    CallBack();
                }
                AnimateShot();
                AdvanceSocket();
            }
        }
    }
}