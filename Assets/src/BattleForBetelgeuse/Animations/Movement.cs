namespace Assets.Animations {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Elements.GUI.Grid.HexTile;
    using Assets.Flux.TweenInteraction;
    using Assets.GameManagement;

    using UnityEngine;

    public class Movement {
        public static void MoveAlongPath<T>(List<HexCoordinate> path, T tweenableBehaviour)
            where T : MonoBehaviour, ITweenable {
            tweenableBehaviour.BeforeTween();
            path.Reverse();
            var vectorPath = (from hex in path select GridManager.CalculateLocationFromHexCoordinate(hex)).ToArray();
            var hash = CallBackingTween("AfterTween");
            hash.Add("path", vectorPath);
            hash.Add("orienttopath", true);
            hash.Add("time", path.Count);
            hash.Add("easetype", "easeInOutQuad");

            iTween.MoveTo(tweenableBehaviour.gameObject, hash);
        }

        private static Hashtable CallBackingTween(string method) {
            return new Hashtable { { "oncomplete", method } };
        }

        public static void FaceHex<T>(HexCoordinate hex, T tweenableBehaviour, string callBack = null)
            where T : MonoBehaviour, ITweenable {
            tweenableBehaviour.BeforeTween();
            var target = GridManager.CalculateLocationFromHexCoordinate(hex);
            var hash = !string.IsNullOrEmpty(callBack) ? CallBackingTween(callBack) : new Hashtable();
            hash.Add("looktarget", target);
            hash.Add("time", 1);

            iTween.LookTo(tweenableBehaviour.gameObject, hash);
        }
    }
}