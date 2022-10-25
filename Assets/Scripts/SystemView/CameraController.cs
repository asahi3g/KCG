using UnityEngine;

namespace Scripts {
    namespace SystemView {
        public class CameraController : MonoBehaviour {
            public float scale = 1.0f;
            public bool DisableDragging = false;

            private float last_aspect;
            public  Background background;

            private Camera currentCamera;

            private void Awake() {
                currentCamera = GetComponent<Camera>();
            }

            private void Update() {
                if(Input.GetMouseButton(1) && Input.mousePosition.x < Screen.width * 0.75) {
                    transform.position += Vector3.right * Input.GetAxis("Mouse X") * -0.28f / scale;
                    transform.position += Vector3.up    * Input.GetAxis("Mouse Y") * -0.28f / scale;
                }

                scale += Input.GetAxis("Mouse ScrollWheel") * 0.5f * scale;
                currentCamera.orthographicSize = 20.0f / scale;

                // Scale background with camera to keep background at always the same size
                if(background != null)
                    background.transform.localScale = new Vector3(5.0f / scale, 5.0f / scale, 1.0f);
            }

            public void set_position(float x, float y, float s) {
                scale = s;
                transform.position = new Vector3(-x, -y, -10);
                currentCamera.orthographicSize = 20.0f / scale;
            }

            public Vector3 get_rel_pos(Vector3 absolute) { return currentCamera.WorldToScreenPoint(absolute); }
            public Vector3 get_abs_pos(Vector3 relative) { return currentCamera.ScreenToWorldPoint(relative); }
            public float get_aspect_ratio()              { return currentCamera.aspect; }
            public float get_width()                     { return Screen.width; }
            public float get_height()                    { return Screen.height; }

            public bool size_changed() {
                if(last_aspect == currentCamera.aspect) return false;

                last_aspect = currentCamera.aspect;

                return true;
            }
        }
    }
}
