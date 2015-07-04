namespace Assets.Animations {
    using UnityEngine;

    public class Explosions {
        public static void TinyExplosion(GameObject go) {
            var resource = Resources.Load("Animations/Detonator-Tiny");
            Object.Instantiate(resource, go.transform.position, go.transform.localRotation);
        }

        public static void MeshExplosion(GameObject go) {
            var explosions = go.GetComponentsInChildren<MeshExploder>();
            foreach (var explosion in explosions) {
                explosion.Explode();
            }
        }
    }
}