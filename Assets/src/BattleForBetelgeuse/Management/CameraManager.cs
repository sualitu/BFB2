namespace Assets.BattleForBetelgeuse.Management {
    using UnityEngine;

    public class CameraManager : MonoBehaviour {
        private static Camera mainCamera;

        private void Awake() {
            mainCamera = Camera.main;
        }

        private void Update() {
            var translation = Vector3.zero;

            var zoomDelta = UnityEngine.Input.GetAxis("Mouse ScrollWheel") * Settings.Camera.ZoomSpeed * Time.deltaTime;
            if (zoomDelta != 0) {
                translation -= Vector3.up * Settings.Camera.ZoomSpeed * zoomDelta;
            }

            var pan = mainCamera.transform.eulerAngles.x - zoomDelta * Settings.Camera.PanSpeed;
            pan = Mathf.Clamp(pan, Settings.Camera.PanAngleMin, Settings.Camera.PanAngleMax);
            if (zoomDelta < 0 || mainCamera.transform.position.y < (Settings.Camera.ZoomMax / 2)) {
                mainCamera.transform.eulerAngles = new Vector3(pan, 0, 0);
            }

            translation +=
                (new Vector3(UnityEngine.Input.GetAxis("Horizontal") * Settings.Camera.KeyboardScrollSpeed,
                             0,
                             UnityEngine.Input.GetAxis("Vertical") * Settings.Camera.KeyboardScrollSpeed));

            if (UnityEngine.Input.GetMouseButton(2)) {
                // Hold button and drag camera around
                translation -= new Vector3(UnityEngine.Input.GetAxis("Mouse X") * Settings.Camera.DragSpeed * Time.deltaTime,
                                           0,
                                           UnityEngine.Input.GetAxis("Mouse Y") * Settings.Camera.DragSpeed * Time.deltaTime);
            } else {
                // Move camera if mouse pointer reaches screen borders
                if (UnityEngine.Input.mousePosition.x < Settings.Camera.ScrollArea) {
                    translation += Vector3.right * -Settings.Camera.ScrollSpeed * Time.deltaTime;
                }

                if (UnityEngine.Input.mousePosition.x >= Screen.width - Settings.Camera.ScrollArea) {
                    translation += Vector3.right * Settings.Camera.ScrollSpeed * Time.deltaTime;
                }

                if (UnityEngine.Input.mousePosition.y < Settings.Camera.ScrollArea) {
                    translation += Vector3.forward * -Settings.Camera.ScrollSpeed * Time.deltaTime;
                }

                if (UnityEngine.Input.mousePosition.y > Screen.height - Settings.Camera.ScrollArea) {
                    translation += Vector3.forward * Settings.Camera.ScrollSpeed * Time.deltaTime;
                }
            }

            var desiredPosition = mainCamera.transform.position + translation;
            if (desiredPosition.x < Settings.Camera.LevelAreaMin.x || Settings.Camera.LevelAreaMax.x < desiredPosition.x) {
                translation.x = 0;
            }
            if (desiredPosition.y < Settings.Camera.LevelAreaMin.y || Settings.Camera.LevelAreaMax.y < desiredPosition.y) {
                translation.y = 0;
            }
            if (desiredPosition.z < Settings.Camera.LevelAreaMin.z || Settings.Camera.LevelAreaMax.z < desiredPosition.z) {
                translation.z = 0;
            }

            mainCamera.transform.position += translation;
        }
    }
}