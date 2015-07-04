using UnityEngine;

namespace Assets.BattleForBetelgeuse.Animations.MeshExplosions {
    public class DestroyOnFadeCompletion : MonoBehaviour {

        void FadeCompleted() {
            Object.Destroy(gameObject);
        }

    }
}
