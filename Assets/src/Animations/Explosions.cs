using UnityEngine;

namespace BattleForBetelgeuse.Animations {

  public class Explosions {
    public static void TinyExplosion(GameObject go) {
      var resource = Resources.Load("Animations/Detonator-Tiny");
      GameObject.Instantiate(resource, go.transform.position, go.transform.localRotation);
    }

    public static void MeshExplosion(GameObject go) {
      var explosions = go.GetComponentsInChildren<MeshExploder>();
      foreach(var explosion in explosions) {
        explosion.Explode();
      }
    }
  }
}

