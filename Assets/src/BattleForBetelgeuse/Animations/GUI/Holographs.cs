namespace Assets.src.BattleForBetelgeuse.Animations.GUI {
    using System.Collections;

    using UnityEngine;

    internal class Holographs : MonoBehaviour {
        private readonly float floatDownSpeed = .01f;

        //Andromeda 2D Hologram System JavaScript Version 1.0 @ Copyright
        //Black Horizon Studios

        private readonly float floatUpSpeed = .01f;

        public float maxFlicker = 1f;

        public float minFlicker = 0f;

        public float shakeIntensity = .1f;

        public bool doesRotate;

        private float flickerSpeed;

        private bool floatup;

        public GameObject hologramPlane1;

        public GameObject hologramPlane2;

        private float offsetX;

        private float offsetY;

        private float rotateSpeed = 0f;

        private float xSpeed = 1f;

        private float ySpeed = 1f;

        public float fade = 0f;

        private void Start() {
            floatup = false;
            if (hologramPlane1 == null) {
                Debug.LogError(
                               "You need to apply a plane model to the Hologram Plane 1 slot. The model must contain the Hologram Shader. Refer to the demo scene for an example if needed.");
            }

            if (hologramPlane2 == null) {
                Debug.LogError(
                               "You need to apply a plane model to the Hologram Plane 2 slot. The model must contain the Hologram Shader. Refer to the demo scene for an example if needed.");
            }
            var param = new Hashtable {
                { "from", 0.0f },
                { "to", 1.0f },
                { "time", 2.0f },
                { "onupdate", "UpdateFade" },
                { "delay", 1.0f }
            };
            iTween.ValueTo(gameObject, param);
            var paramOut = new Hashtable {
                { "from", 1.0f },
                { "to", 0.0f },
                { "time", 1.0f },
                { "onupdate", "UpdateFade" },
                { "delay", 5.0f }
            };
            iTween.ValueTo(gameObject, paramOut);
        }

        public void UpdateFade(float val) {
            fade = val;
        }

        void Update() {
            if (floatup) {
                StartCoroutine(floatingup());
            } else if (!floatup) {
                StartCoroutine(floatingdown());
            }

            flickerSpeed = Random.Range(minFlicker, maxFlicker);



            hologramPlane1.GetComponent<TweenAlpha>().value = flickerSpeed * fade;
            hologramPlane2.GetComponent<TweenAlpha>().value = flickerSpeed * fade;


            if (maxFlicker > 2) {
                Debug.LogError("Max flicker amount should not exceed 2");
            }

            if (minFlicker < -1) {
                Debug.LogError("Min flicker amount should not go below -1");
            }
        }

        private IEnumerator floatingup() {
            var y = transform.position.y + shakeIntensity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return new WaitForSeconds(floatUpSpeed);
            floatup = false;
        }

        private IEnumerator floatingdown() {
            var y = transform.position.y - shakeIntensity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            ;
            yield return new WaitForSeconds(floatDownSpeed);
            floatup = true;
        }
    }
}