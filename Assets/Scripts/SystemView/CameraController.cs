//import UnityEngine


namespace Scripts {
    namespace SystemView {
        public class CameraController : UnityEngine.MonoBehaviour {
            public float scale = 1.0f;
            public bool DisableDragging = false;

            private float last_aspect;
            public  Background background;

            private UnityEngine.Camera camera;

            private void Awake() {
                camera = GetComponent<UnityEngine.Camera>();
            }

            private void Update() {
                if(UnityEngine.Input.GetMouseButton(1) && UnityEngine.Input.mousePosition.x < UnityEngine.Screen.width * 0.75) {
                    transform.position += UnityEngine.Vector3.right * UnityEngine.Input.GetAxis("Mouse X") * -0.28f / scale;
                    transform.position += UnityEngine.Vector3.up    * UnityEngine.Input.GetAxis("Mouse Y") * -0.28f / scale;
                }

                scale += UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 0.5f * scale;
                camera.orthographicSize = 20.0f / scale;

                // Scale background with camera to keep background at always the same size
                if(background != null)
                    transform.localScale = new UnityEngine.Vector3(5.0f / scale, 5.0f / scale, 1.0f);
            }

            public void set_position(float x, float y, float s) {
                scale = s;
                transform.position = new UnityEngine.Vector3(-x, -y, -10);
                camera.orthographicSize = 20.0f / scale;
            }

            public UnityEngine.Vector3 get_rel_pos(UnityEngine.Vector3 absolute) { return camera.WorldToScreenPoint(absolute); }
            public UnityEngine.Vector3 get_abs_pos(UnityEngine.Vector3 relative) { return camera.ScreenToWorldPoint(relative); }
            public float get_aspect_ratio()              { return camera.aspect; }
            public float get_width()                     { return UnityEngine.Screen.width; }
            public float get_height()                    { return UnityEngine.Screen.height; }
        }
    }
}
