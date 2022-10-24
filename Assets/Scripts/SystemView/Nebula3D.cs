using System;


using Source.SystemView;

namespace Scripts {
    namespace SystemView {
        public class Nebula3D : UnityEngine.MonoBehaviour {
            public int            seed;
            public int            layers;
            public UnityEngine.Color          basecolor;
            public UnityEngine.Color[]        colors;
            public int            width;
            public int            height;
            public int            depth;
            public float          opacity;
            public float          contrast;
            public float          cutoff;           // Also sometimes called black level
            public float          spin;             // Angular velocity in degrees/second
            public UnityEngine.Vector3        center;           // Center position to spin around

            public UnityEngine.Texture2D      texture;
            public UnityEngine.SpriteRenderer renderer;

            public bool           pixelate;
            public int            pixelation_size;

            private System.Random rng;
            private float         last_time;

            public  UnityEngine.ComputeShader         gen3dnoise_shader;
            public  UnityEngine.ComputeShader        scale_noise_shader;
            public UnityEngine.ComputeShader         distortion_shader;

            private float[]       alpha;

            private void generate() {
                // Shaders properties
                int      width_id = UnityEngine.Shader.PropertyToID("width");
                int     height_id = UnityEngine.Shader.PropertyToID("height");
                int      depth_id = UnityEngine.Shader.PropertyToID("depth");
                int      noise_id = UnityEngine.Shader.PropertyToID("noise");
                int      scale_id = UnityEngine.Shader.PropertyToID("scale");
                int       size_id = UnityEngine.Shader.PropertyToID("size");
                int       seed_id = UnityEngine.Shader.PropertyToID("seed");
                int   strength_id = UnityEngine.Shader.PropertyToID("strength");
                int     radius_id = UnityEngine.Shader.PropertyToID("radius");
                int distortion_id = UnityEngine.Shader.PropertyToID("distortionnoise");
                int     output_id = UnityEngine.Shader.PropertyToID("outputnoise");
                int    sharpen_id = UnityEngine.Shader.PropertyToID("sharpen");
                
                rng = new(seed);

                alpha = new float[width * height * depth];

                // Generate base noise
                UnityEngine.ComputeBuffer base_buffer1 = new UnityEngine.ComputeBuffer(width * height * depth, sizeof(float));

                for(int layer = 0; layer < layers && layer < 7; layer++) {
                    int  size = 1   << (layers - 1 - layer);
                    int scale = 128 >>               layer;

                    gen3dnoise_shader.SetInt( width_id, width);
                    gen3dnoise_shader.SetInt(height_id, height);
                    gen3dnoise_shader.SetInt( depth_id, depth);
                    gen3dnoise_shader.SetInt(  seed_id, rng.Next());
                    gen3dnoise_shader.SetInt(  size_id, size);
                    gen3dnoise_shader.SetInt( scale_id, scale);

                    gen3dnoise_shader.SetBuffer(0, noise_id, base_buffer1);

                    gen3dnoise_shader.Dispatch(0, width / size / 4, height / size / 4, depth / size / 4);
                }

                // Generate distortion noise
                UnityEngine.ComputeBuffer distortion_buffer_unscaled = new UnityEngine.ComputeBuffer(width * height * depth / 32768, sizeof(float));
                UnityEngine.ComputeBuffer distortion_buffer          = new UnityEngine.ComputeBuffer(width * height * depth, sizeof(float));

                for(int layer = 0; layer < 3; layer++) {
                    int  size = 1   << (2 - layer);
                    int scale = 128 >>      layer;
                    
                    gen3dnoise_shader.SetInt( width_id, width  / 32);
                    gen3dnoise_shader.SetInt(height_id, height / 32);
                    gen3dnoise_shader.SetInt( depth_id, depth  / 32);
                    gen3dnoise_shader.SetInt(  seed_id, rng.Next());
                    gen3dnoise_shader.SetInt(  size_id, size);
                    gen3dnoise_shader.SetInt( scale_id, scale);

                    gen3dnoise_shader.SetBuffer(0, noise_id, distortion_buffer_unscaled);

                    gen3dnoise_shader.Dispatch(0, width / size / 128, height / size / 128, depth / size / 128);
                }

                // Scale distortion noise
                scale_noise_shader.SetInt( width_id, width);
                scale_noise_shader.SetInt(height_id, height);
                scale_noise_shader.SetInt( depth_id, depth);
                scale_noise_shader.SetInt( scale_id, 32);

                scale_noise_shader.SetBuffer(0,  noise_id, distortion_buffer_unscaled);
                scale_noise_shader.SetBuffer(0, output_id, distortion_buffer);

                scale_noise_shader.Dispatch(0, width / 4, height / 4, depth / 4);

                distortion_buffer_unscaled.Release();

                // Apply distortion
                UnityEngine.ComputeBuffer base_buffer2 = new UnityEngine.ComputeBuffer(width * height * depth, sizeof(float));

                distortion_shader.SetInt( width_id, width);
                distortion_shader.SetInt(height_id, height);

                distortion_shader.SetFloat(strength_id, 24.0f);

                distortion_shader.SetBuffer(0, distortion_id, distortion_buffer);
                distortion_shader.SetBuffer(0,      noise_id, base_buffer1);
                distortion_shader.SetBuffer(0,     output_id, base_buffer2);

                distortion_shader.Dispatch(0, width / 4, height / 4, depth / 4);

                distortion_buffer.Release();
                base_buffer1.Release();

                base_buffer2.GetData(alpha);
                base_buffer2.Release();

                texture = new UnityEngine.Texture2D(width, height);
                texture.filterMode = UnityEngine.FilterMode.Trilinear;
            }

            private void update_texture(float dt) {
                UnityEngine.Color[] pixels = new UnityEngine.Color[width * height];

                float curz  = (last_time + dt) % depth;

                int   z0    =  (int)curz;
                int   z1    = ((int)curz +  1) % depth;

                float dz    = curz - z0;

                int offset0 = z0 * width * height;
                int offset1 = z1 * width * height;

                // TODO: EDIT THIS LATER
                for(int i = 0; i < width * height; i++)
                    pixels[i] = new UnityEngine.Color(basecolor.r, basecolor.g, basecolor.b, Tools.smoothstep(alpha[offset0 + i], alpha[offset1 + i], dz));

                texture.SetPixels(pixels);
                texture.Apply();

                renderer.sprite = UnityEngine.Sprite.Create(texture,
                                                new UnityEngine.Rect(0, 0, width, height),
                                                new UnityEngine.Vector2(0.5f, 0.5f));
            }

            private void Start() {
                generate();

                renderer        = gameObject.AddComponent<UnityEngine.SpriteRenderer>();

                renderer.transform.Translate(new UnityEngine.Vector3(0.0f, 0.0f, renderer.transform.position.z + 5.0f));

                last_time = UnityEngine.Time.time;
            }

            private void Update() {
                float current_time = UnityEngine.Time.time - last_time;

                update_texture(current_time);

                renderer.transform.RotateAround(center, new UnityEngine.Vector3(0.0f, 0.0f, 1.0f), -current_time * spin);

                last_time = UnityEngine.Time.time;
            }
        }
    }
}
    