namespace Assets.BattleForBetelgeuse.Animations {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BattleForBetelgeuse.Animations.TweenInteraction;
    using Assets.BattleForBetelgeuse.FluxElements.GUI.Grid.HexTile;
    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    public class Movement {
        public class Unit {
            public static void MoveAlongPath<T>(List<HexCoordinate> path, T tweenableBehaviour)
                where T : MonoBehaviour, ITweenable {
                tweenableBehaviour.BeforeTween();
                path.Reverse();
                var vectorPath = (from hex in path select GridManager.CalculateLocationFromHexCoordinate(hex)).ToArray();
                var hash = CallBackingTween("AfterTween");
                hash.Add("path", vectorPath);
                hash.Add("orienttopath", true);
                hash.Add("time", path.Count * .75f);
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
                hash.Add("time", .5f);

                iTween.LookTo(tweenableBehaviour.gameObject, hash);
            }
        }

        public class Gui {
            public static Vector3 NormalizeFromVector(Vector3 from, Vector3 to) {
                var newX = AdjustCoordinate(from.x, to.x);
                var newY = AdjustCoordinate(from.y, to.y);
                var newZ = AdjustCoordinate(from.z, to.z);

                return new Vector3(newX, newY, newZ);
            }

            private static float AdjustCoordinate(float from, float to) {
                var @new = from;
                if (Mathf.Abs(from - to) > 180f) {
                    @new += (360f * ((from > to) ? -1 : 1));
                }
                return @new;
            }

            public static void ScaleTo<TComponent>(
                Vector3 from,
                Vector3 target,
                TComponent guiComponent,
                float duration,
                EventDelegate.Callback callback = null) where TComponent : MonoBehaviour, ITweenableGuiComponent
            {
                var tween = guiComponent.gameObject.GetComponent<TweenScale>()
                                 ?? guiComponent.gameObject.AddComponent<TweenScale>();
                tween.from = from;
                tween.to = target;
                tween.duration = duration;
                tween.method = UITweener.Method.EaseOut;
                tween.delay = 0f;
                if (callback != null) {
                    var callbackEvent = new EventDelegate(callback) { oneShot = true };
                    tween.onFinished.Add(callbackEvent);
                }
                tween.ResetToBeginning();
                tween.PlayForward();
            }

            public static void RotateTo<TComponent>(
                Vector3 from,
                Vector3 target,
                TComponent guiComponent,
                float duration,
                EventDelegate.Callback callback = null) where TComponent : MonoBehaviour, ITweenableGuiComponent
            {
                var tween = guiComponent.gameObject.GetComponent<TweenRotation>()
                                    ?? guiComponent.gameObject.AddComponent<TweenRotation>();
                tween.from = NormalizeFromVector(from, target);
                tween.to = target;
                tween.duration = duration;
                tween.delay = 0f;
                tween.method = UITweener.Method.EaseOut;
                tween.enabled = true;
                if (callback != null) {
                    var callbackEvent = new EventDelegate(callback) { oneShot = true };
                    tween.onFinished.Add(callbackEvent);
                }
                tween.ResetToBeginning();
                tween.PlayForward();
            }

            public static void MoveTo<TComponent>(
                Vector3 from,
                Vector3 target,
                TComponent guiComponent,
                float duration,
                EventDelegate.Callback callback = null) where TComponent : MonoBehaviour, ITweenableGuiComponent
            {
                var tween = guiComponent.gameObject.GetComponent<TweenPosition>()
                                    ?? guiComponent.gameObject.AddComponent<TweenPosition>();

                tween.from = from;
                tween.to = target;
                tween.duration = duration;
                tween.delay = 0f;
                tween.enabled = true;
                tween.method = UITweener.Method.EaseOut;
                if (callback != null) {
                    var callbackEvent = new EventDelegate(callback) { oneShot = true };
                    tween.onFinished.Add(callbackEvent);
                }
                tween.ResetToBeginning();
                tween.PlayForward();
            }
        }
    }
}