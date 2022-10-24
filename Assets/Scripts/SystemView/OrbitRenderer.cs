//imports UnityEngine

using System;
using System.Collections.Generic;
using Source.SystemView;

namespace Scripts {
    namespace SystemView {
        public class OrbitRenderer : UnityEngine.MonoBehaviour {
            private UnityEngine.LineRenderer line_renderer;

            public OrbitingObjectDescriptor descriptor;

            public UnityEngine.Material mat;

            public UnityEngine.Color color = new UnityEngine.Color(0.5f, 0.7f, 1.0f, 1.0f);

            public float line_width = 0.1f;

            public CameraController camera;

            public void update_renderer(int segments) {
                if(line_renderer == null) return;
                if(descriptor == null) {
                    line_renderer.positionCount = 0;
                    line_renderer.startWidth = line_renderer.endWidth = 0.0f;
                    return;
                }

                List<UnityEngine.Vector3> vertices = new();
                
                for(int i = 0; i < segments; i++) {
                    float true_anomaly;

                    if(descriptor.eccentricity >= 1.0f)
                        // 0.0002f "safety" margin otherwise there is an unpleasant flickering
                        true_anomaly = -descriptor.true_anomaly_asymptote + 0.0001f
                                     + (descriptor.true_anomaly_asymptote - 0.0002f) * 2.0f * i / segments;
                    else
                        true_anomaly = descriptor.get_true_anomaly(
                            descriptor.get_eccentric_anomaly_at(
                                i * Tools.twopi / segments
                            )
                        );

                    float[] pos = descriptor.get_position_at(
                        true_anomaly,
                        descriptor.get_distance_from_center_at(true_anomaly)
                    );

                    vertices.Add(new UnityEngine.Vector3(pos[0], pos[1], 0.0f));
                }

                line_renderer.startWidth    =
                line_renderer.endWidth      = line_width == 0.1f ? line_width / camera.scale : line_width;
                line_renderer.startColor    =
                line_renderer.endColor      = color;

                line_renderer.SetPositions(vertices.ToArray());

                line_renderer.positionCount = vertices.Count;
                line_renderer.loop          = descriptor.eccentricity < 1.0f;
            }

            // Start is called before the first frame update
            void Start() {
                camera = UnityEngine.GameObject.Find("Main Camera").GetComponent<CameraController>();

                line_renderer = gameObject.AddComponent<UnityEngine.LineRenderer>();

                // Load unity test shader
                // this could alternatively also be our GLTestShader

                UnityEngine.Shader shader = UnityEngine.Shader.Find("Hidden/Internal-Colored");
                mat = new UnityEngine.Material(shader);
                mat.hideFlags = UnityEngine.HideFlags.HideAndDontSave;

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                // Turn off backface culling, depth writes, depth test.
                mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                mat.SetInt("_ZWrite", 0);
                mat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);

                line_renderer.material = mat;

                line_renderer.useWorldSpace = true;
            }

            void OnDestroy() {
                UnityEngine.GameObject.Destroy(line_renderer);
            }
        }
    }
}
