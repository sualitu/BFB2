namespace BattleForBetelgeuse.Animations.Combat {
    using System.Collections.Generic;
    using System.Linq;

    using BattleForBetelgeuse.Constants;

    using UnityEngine;

    public abstract class CombatAnimation : MonoBehaviour {
        private int currentSocket;

        private List<Transform> sockets;
        internal bool Shooting { get; set; }

        internal abstract int FramesBetweenShots { get; }

        private int frameCount;

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

        public Transform CurrentSocket {
            get {
                return sockets[currentSocket];
            }
        }

        public abstract void CombatWith(Transform target);

        internal bool ShootAgain() {
            frameCount ++;
            if (FramesBetweenShots > frameCount) {
                return false;
            }
            frameCount = 0;
            return true;
        }
    }
}