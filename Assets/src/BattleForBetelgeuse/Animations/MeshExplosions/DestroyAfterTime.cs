using System.Collections;

using UnityEngine;

namespace Assets.BattleForBetelgeuse.Animations.MeshExplosions {
    public class DestroyAfterTime : MonoBehaviour {
	
        public float waitTime;
	
        IEnumerator Start() {
            yield return new WaitForSeconds(waitTime);
            GameObject.Destroy(gameObject);
        }
	
    }
}
