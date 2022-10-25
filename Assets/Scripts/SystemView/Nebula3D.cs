using System;
using UnityEngine;
using Source.SystemView;

namespace Scripts {
    namespace SystemView {
        public class Nebula3D : MonoBehaviour {
            public int            seed;
            public int            layers;
            public Color          basecolor;
            public Color[]        colors;
            public int            width;
            public int            height;
            public int            depth;
            public float          opacity;
            public float          contrast;
            public float          cutoff;           // Also sometimes called black level
            public float          spin;             // Angular velocity in degrees/second
            public Vector3        center;           // Center position to spin around

            public Texture2D      texture;
            public SpriteRenderer spriteRenderer;

            public bool           pixelate;
            public int            pixelation_size;

            private System.Random rng;
            private float         last_time;

            public  ComputeShader         gen3dnoise_shader;
            public  ComputeShader        scale_noise_shader;
            public  ComputeShader         distortion_shader;

            private float[]       alpha;

            private void generate() {
                // Shaders properties
                int      width_id = Shader.PropertyToID("width");
                int     height_id = Shader.PropertyToID("height");
                int      depth_id = Shader.PropertyToID("depth");
                int      noise_id = Shader.PropertyToID("noise");
                int      scale_id = Shader.PropertyToID("scale");
                int       size_id = Shader.PropertyToID("size");
                int       seed_id = Shader.PropertyToID("seed");
                int   strength_id = Shader.PropertyToID("strength");
                int     radius_id = Shader.PropertyToID("radius");
                int distortion_id = Shader.PropertyToID("distortionnoise");
                int     output_id = Shader.PropertyToID("outputnoise");
                int    sharpen_id = Shader.PropertyToID("sharpen");
                
                rng = new(seed);

                alpha = new float[width * height * depth];

                // Generate base noise
                ComputeBuffer base_buffer1 = new ComputeBuffer(width * height * depth, sizeof(float));

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
                ComputeBuffer distortion_buffer_unscaled = new ComputeBuffer(width * height * depth / 32768, sizeof(float));
                ComputeBuffer distortion_buffer          = new ComputeBuffer(width * height * depth, sizeof(float));

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
                ComputeBuffer base_buffer2 = new ComputeBuffer(width * height * depth, sizeof(float));

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

                texture = new Texture2D(width, height);
                texture.filterMode = FilterMode.Trilinear;
            }

            private void update_texture(float dt) {
                Color[] pixels = new Color[width * height];

                float curz  = (last_time + dt) % depth;

                int   z0    =  (int)curz;
                int   z1    = ((int)curz +  1) % depth;

                float dz    = curz - z0;

                int offset0 = z0 * width * height;
                int offset1 = z1 * width * height;

                // TODO: EDIT THIS LATER
                for(int i = 0; i < width * height; i++)
                    pixels[i] = new Color(basecolor.r, basecolor.g, basecolor.b, Tools.smoothstep(alpha[offset0 + i], alpha[offset1 + i], dz));

                texture.SetPixels(pixels);
                texture.Apply();

                spriteRenderer.sprite = Sprite.Create(texture,
                                                new Rect(0, 0, width, height),
                                                new Vector2(0.5f, 0.5f));
            }

            private void Start() {
                generate();

                spriteRenderer        = gameObject.AddComponent<SpriteRenderer>();

                spriteRenderer.transform.Translate(new Vector3(0.0f, 0.0f, spriteRenderer.transform.position.z + 5.0f));

                last_time = Time.time;
            }

            private void Update() {
                float current_time = Time.time - last_time;

                update_texture(current_time);

                spriteRenderer.transform.RotateAround(center, new Vector3(0.0f, 0.0f, 1.0f), -current_time * spin);

                last_time = Time.time;
            }
        }
    }
}
    