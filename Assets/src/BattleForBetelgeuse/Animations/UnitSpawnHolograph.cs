namespace Assets.BattleForBetelgeuse.Animations {
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BattleForBetelgeuse.Management;
    
    using UnityEngine;

    public class UnitSpawnHolograph : MonoBehaviour {
        private readonly float floatDownSpeed = .01f;

        //Andromeda Model System JavaScript Version 1.0 @ Copyright
        //Black Horizon Studios

        private readonly float floatUpSpeed = .01f;

        private readonly float maxFlicker = 1;

        private readonly float minFlicker = 0;

        private readonly float shakeIntensity = .1f;

        private readonly float xSpeed = 1;

        private readonly float ySpeed = 1;

        private float flickerSpeed;

        private bool floatup;

        public GameObject hologramModel;

        private float offsetX;

        private float offsetY;

        private float rotateSpeed = 0;

        private float fade;

        private void Start() {
            floatup = false;
            fade = 0f;
            FadeIn();
            if (hologramModel == null) {
                Debug.LogError(
                               "You need to apply a model to the Hologram Model slot. The model must contain 1st the the Hologram Static Shader then the Hologram Solid Shader. Refer to the demo scene for an example if needed.");
            }
        }

        public void FadeOut(float fadeOutDelay = 0f, float fadeOutDuration = 1.0f)
        {
            var paramOut = new Hashtable {
                { "from", 1.0f },
                { "to", 0.0f },
                { "time", fadeOutDuration},
                { "onupdate", "UpdateFade" },
                { "delay", fadeOutDelay }
            };
            iTween.ValueTo(gameObject, paramOut);
        }

        public void UpdateFade(float val)
        {
            fade = val;
        }

        public void FadeIn(float fadeInTime = Settings.Animations.Units.FadeInTime)
        {
            var param = new Hashtable {
                { "from", 0.0f },
                { "to", 1.0f },
                { "time", fadeInTime },
                { "onupdate", "UpdateFade" },
            };
            iTween.ValueTo(gameObject, param);
        }

        private void Update() {
            offsetX = Time.time * xSpeed;
            offsetY = Time.time * ySpeed;

            if (floatup) {
                Floatingup();
            } else if (!floatup) {
                Floatingdown();
            }

            flickerSpeed = Random.Range(minFlicker, maxFlicker);

            if (fade > .99f) {
                SetMaterialColor(hologramModel.GetComponent<Renderer>().material);
            } else {
                foreach (var mat in hologramModel.GetComponent<SkinnedMeshRenderer>().materials) {
                    SetMaterialColor(mat);
                }
            }
            if (maxFlicker > 2) {
                Debug.LogError("Max flicker amount should not exceed 2");
            }

            if (minFlicker < -1) {
                Debug.LogError("Min flicker amount should not go below -1");
            }

            hologramModel.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }

        private void SetMaterialColor(Material material) {
            var newColor = material.color;

            newColor.a = flickerSpeed * fade;

            material.color = newColor;
        }

        private IEnumerator Floatingup() {
            var newPosition = transform.position;
            newPosition.y += shakeIntensity * Time.deltaTime;
            transform.position = newPosition;
            yield return new WaitForSeconds(floatUpSpeed);
            floatup = false;
        }

        private IEnumerator Floatingdown() {
            var newPosition = transform.position;
            newPosition.y -= shakeIntensity * Time.deltaTime;
            transform.position = newPosition;
            yield return new WaitForSeconds(floatDownSpeed);
            floatup = true;
        }
    }
}