using UnityEngine;
using Source.SystemView;

namespace Scripts {
    namespace SystemView {
        public class SystemPlanetRenderer : MonoBehaviour {
            public  SystemPlanet     planet;

            public  SpriteRenderer   sr;
            public  OrbitRenderer    or;

            public  int              seed;
            public  Texture2D        texture;
            public  Color            basecolor;
            public  Color[]          colors;
            public  int              radius;
            public  int              layers;

            public  Color            orbitColor = new Color(0.5f, 0.7f, 1.0f, 1.0f);

            public  CameraController Camera;
            private System.Random    rng;

            public  bool             pixelate;

            public  ComputeShader    blur_noise_shader;
            public  ComputeShader    scale_noise_shader;
            public  ComputeShader    exponential_filter_shader;
            public  ComputeShader    distortion_shader;
            public  ComputeShader    circular_blur_shader;
            public  ComputeShader    circular_mask_shader;
            public  ComputeShader    pixelate_shader;

            public  bool             autoinit; // For testing

            private bool             initialized;

            private void generate() {
                // Shaders properties
                int      width_id = Shader.PropertyToID("width");
                int     height_id = Shader.PropertyToID("height");
                int      noise_id = Shader.PropertyToID("noise");
                int      scale_id = Shader.PropertyToID("scale");
                int   strength_id = Shader.PropertyToID("strength");
                int     radius_id = Shader.PropertyToID("radius");
                int distortion_id = Shader.PropertyToID("distortionnoise");
                int     output_id = Shader.PropertyToID("outputnoise");
                int    sharpen_id = Shader.PropertyToID("sharpen");

                rng = new(seed);

                Color[] pixels    = new Color[radius * radius];

                for(int i = 0; i < radius * radius; i++) pixels[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);

                // Generate base noise
                float[] base_alpha = new float[radius * radius];
                for(int i = 0; i < radius * radius; i++) base_alpha[i] = 1.0f;

                ComputeBuffer base_buffer = new ComputeBuffer(radius * radius, sizeof(float));

                base_buffer.SetData(base_alpha);

                // Apply circular mask
                circular_mask_shader.SetInt( width_id, radius);
                circular_mask_shader.SetInt(height_id, radius);

                circular_mask_shader.SetBool(sharpen_id, true);

                circular_mask_shader.SetBuffer(0, noise_id, base_buffer);

                circular_mask_shader.Dispatch(0, radius / 8, radius / 8, 1);

                base_buffer.GetData(base_alpha);
                base_buffer.Release();

                for(int x = 0; x < radius; x++)
                    for(int y = 0; y < radius; y++) {
                        pixels[x + y * radius].r = basecolor.r;
                        pixels[x + y * radius].g = basecolor.g;
                        pixels[x + y * radius].b = basecolor.b;
                        pixels[x + y * radius].a = base_alpha[x + y * radius];
                    }

                if(colors != null && colors.Length != 0) {
                    ComputeBuffer color_buffer1 = new ComputeBuffer(radius * radius, sizeof(float));

                    for(int layer = 0; layer < layers; layer++) {

                        int   scale         = 1 << layers - layer - 1;
                        float strength      = scale / layers;

                        ComputeBuffer layer_buffer = new ComputeBuffer(radius / scale * radius / scale, sizeof(float));

                        // Generate random noise
                        layer_buffer.SetData(ProceduralImages.generate_noise(rng, strength, radius / scale, radius / scale));

                        // Blur noise
                        blur_noise_shader.SetInt( width_id, radius / scale);
                        blur_noise_shader.SetInt(height_id, radius / scale);

                        blur_noise_shader.SetBuffer(0, noise_id, layer_buffer);

                        blur_noise_shader.Dispatch(0, radius / scale / 8, radius / scale / 8, 1);

                        // Scale noise
                        scale_noise_shader.SetInt( width_id, radius);
                        scale_noise_shader.SetInt(height_id, radius);
                        scale_noise_shader.SetInt( scale_id, scale);

                        scale_noise_shader.SetBuffer(0,  noise_id, layer_buffer);
                        scale_noise_shader.SetBuffer(0, output_id, color_buffer1);

                        scale_noise_shader.Dispatch(0, radius / 8, radius / 8, 1);

                        layer_buffer.Release();

                    }

                    // Apply exponential filter
                    exponential_filter_shader.SetInt( width_id, radius);
                    exponential_filter_shader.SetInt(height_id, radius);

                    exponential_filter_shader.SetBuffer(0, noise_id, color_buffer1);

                    exponential_filter_shader.Dispatch(0, radius / 8, radius / 8, 1);

                    // Generate distortion noise
                    ComputeBuffer color_buffer2 = new ComputeBuffer(radius * radius / 4096, sizeof(float));
                    color_buffer2.SetData(ProceduralImages.generate_noise(rng, 1.0f, radius / 64, radius / 64));

                    // Scale distortion noise
                    ComputeBuffer distortion_noise = new ComputeBuffer(radius * radius, sizeof(float));

                    scale_noise_shader.SetInt( width_id, radius);
                    scale_noise_shader.SetInt(height_id, radius);
                    scale_noise_shader.SetInt( scale_id, 64);

                    scale_noise_shader.SetBuffer(0,  noise_id, color_buffer2);
                    scale_noise_shader.SetBuffer(0, output_id, distortion_noise);

                    scale_noise_shader.Dispatch(0, radius / 8, radius / 8, 1);

                    color_buffer2.Release();

                    // Apply distortion
                    color_buffer2 = new ComputeBuffer(radius * radius, sizeof(float));

                    distortion_shader.SetInt( width_id, radius);
                    distortion_shader.SetInt(height_id, radius);

                    distortion_shader.SetFloat(strength_id, 8.0f);

                    distortion_shader.SetBuffer(0, distortion_id, distortion_noise);
                    distortion_shader.SetBuffer(0,      noise_id, color_buffer1);
                    distortion_shader.SetBuffer(0,     output_id, color_buffer2);

                    distortion_shader.Dispatch(0, radius / 8, radius / 8, 1);

                    distortion_noise.Release();

                    // Apply circular blur
                    circular_blur_shader.SetInt( width_id, radius);
                    circular_blur_shader.SetInt(height_id, radius);

                    circular_blur_shader.SetFloat(radius_id, 4.0f);

                    circular_blur_shader.SetBuffer(0, noise_id,  color_buffer2);
                    circular_blur_shader.SetBuffer(0, output_id, color_buffer1);

                    circular_blur_shader.Dispatch(0, radius / 8, radius / 8, 1);

                    float[] alpha = new float[radius * radius];

                    if(pixelate) {
                        pixelate_shader.SetInt( width_id, radius);
                        pixelate_shader.SetInt(height_id, radius);
                        pixelate_shader.SetInt(radius_id, 16);

                        pixelate_shader.SetBuffer(0,  noise_id, color_buffer1);
                        pixelate_shader.SetBuffer(0, output_id, color_buffer2);

                        pixelate_shader.Dispatch(0, radius / 8, radius / 8, 1);

                        color_buffer2.GetData(alpha);
                        color_buffer2.Release();
                        color_buffer1.Release();
                    } else {
                        color_buffer1.GetData(alpha);
                        color_buffer1.Release();
                        color_buffer2.Release();
                    }

                    for(int id = 0; id < radius * radius; id++) {
                        if(base_alpha[id] == 0.0f) continue;

                        float r, g, b, a;

                        float colorid = alpha[id] * colors.Length;
                        if(colorid <  0.0f)          colorid *= -1;
                        if(colorid >= colors.Length) colorid = colors.Length - 1;

                        if(!pixelate) {
                            int color0id = (int)colorid;
                            int color1id = (color0id + 1) % colors.Length;

                            float dx = colorid - color0id;

                            r = Tools.smoothstep(colors[color0id].r, colors[color1id].r, dx);
                            g = Tools.smoothstep(colors[color0id].g, colors[color1id].g, dx);
                            b = Tools.smoothstep(colors[color0id].b, colors[color1id].b, dx);
                            a = Tools.smoothstep(colors[color0id].a, colors[color1id].a, dx);
                        } else {
                            r = colors[(int)colorid].r;
                            g = colors[(int)colorid].g;
                            b = colors[(int)colorid].b;
                            a = colors[(int)colorid].a;
                        }

                        float blend  =      a +               base_alpha[id] * (1.0f - a);
                        pixels[id].r = (r * a + basecolor.r * base_alpha[id] * (1.0f - a)) / blend;
                        pixels[id].g = (g * a + basecolor.g * base_alpha[id] * (1.0f - a)) / blend;
                        pixels[id].b = (b * a + basecolor.b * base_alpha[id] * (1.0f - a)) / blend;
                        pixels[id].a = base_alpha[id];
                    }
                } else
                    for(int id = 0; id < radius * radius; id++) {
                        pixels[id].r = basecolor.r;
                        pixels[id].g = basecolor.g;
                        pixels[id].b = basecolor.b;
                        pixels[id].a = base_alpha[id];
                    }

                texture = new Texture2D(radius, radius);
                texture.filterMode = FilterMode.Trilinear;
                texture.SetPixels(pixels);
                texture.Apply();
            }

            private void Start() {
                if(autoinit) init();
            }

            public void init() {
                or = gameObject.AddComponent<OrbitRenderer>();
                sr = gameObject.AddComponent<SpriteRenderer>();

                Camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
                
                if(planet != null) or.descriptor = planet.descriptor;

                generate();

                sr.sprite = Sprite.Create(texture,
                                          new Rect(0, 0, radius, radius),
                                          new Vector2(0.5f, 0.5f));

                initialized = true;
            }
             
            void LateUpdate() {
                if(!initialized) return;

                sr.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);

                if(planet != null) {
                    float[] pos = planet.descriptor.get_position();

                    sr.transform.position   = new Vector3(pos[0], pos[1], -0.1f);

                    or.color = orbitColor;

                    or.update_renderer(128);
                }
            }

            void OnDestroy() {
                if(sr != null) GameObject.Destroy(sr);
                if(or != null) GameObject.Destroy(or);
            }
        }
    }
}
