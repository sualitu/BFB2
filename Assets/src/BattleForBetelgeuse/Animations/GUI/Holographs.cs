namespace Assets.BattleForBetelgeuse.Animations.GUI {
    using System.Collections;

    using Assets.BattleForBetelgeuse.Management;

    using UnityEngine;

    internal class Holographs : MonoBehaviour {
        public delegate void HolographCallBack();

        private const float floatDownSpeed = .01f;

        private const float floatUpSpeed = .01f;

        public bool doesRotate;

        public float fade;

        public HolographCallBack FadeInCallBack;

        private float flickerSpeed;

        private bool floatup;

        public GameObject hologramPlane1;

        public GameObject hologramPlane2;

        public float maxFlicker = 1f;

        public float minFlicker = 0f;

        private float offsetX;

        private float offsetY;

        private float rotateSpeed = 0f;

        public float shakeIntensity = .1f;

        private float xSpeed = 1f;

        private float ySpeed = 1f;

        private void Start() {
            floatup = false;
            FadeIn(callback: "CallBackOnFadeIn");
            FadeOut(Settings.Animations.Cards.FadeInTime + .5f);
        }

        public void FadeOut(float fadeOutDelay = 0f) {
            var paramOut = new Hashtable {
                { "from", 1.0f },
                { "to", 0.0f },
                { "time", 1.0f },
                { "onupdate", "UpdateFade" },
                { "delay", fadeOutDelay }
            };
            iTween.ValueTo(gameObject, paramOut);
        }

        public void FadeIn(float fadeInTime = Settings.Animations.Cards.FadeInTime, string callback = "")
        {
            var param = new Hashtable {
                { "from", 0.0f },
                { "to", 1.0f },
                { "time", fadeInTime },
                { "onupdate", "UpdateFade" },
            };
            if (!string.IsNullOrEmpty(callback)) {
                param.Add("oncomplete", callback);
            }
            iTween.ValueTo(gameObject, param);
        }

        public void CallBackOnFadeIn() {
            if (FadeInCallBack != null)
            {
                FadeInCallBack();
            }
        }

        public void UpdateFade(float val) {
            fade = val;
        }

        private void Update() {
            if (floatup) {
                StartCoroutine(Floatingup());
            } else if (!floatup) {
                StartCoroutine(Floatingdown());
            }

            flickerSpeed = Random.Range(minFlicker, maxFlicker);

            hologramPlane1.GetComponent<UITexture>().alpha = flickerSpeed * fade;
            hologramPlane2.GetComponent<UITexture>().alpha = flickerSpeed * fade;

            if (maxFlicker > 2) {
                Debug.LogError("Max flicker amount should not exceed 2");
            }

            if (minFlicker < -1) {
                Debug.LogError("Min flicker amount should not go below -1");
            }
        }

        private IEnumerator Floatingup() {
            var y = transform.position.y + shakeIntensity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return new WaitForSeconds(floatUpSpeed);
            floatup = false;
        }

        private IEnumerator Floatingdown() {
            var y = transform.position.y - shakeIntensity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return new WaitForSeconds(floatDownSpeed);
            floatup = true;
        }
    }
}