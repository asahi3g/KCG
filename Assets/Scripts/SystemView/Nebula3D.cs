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
            public SpriteRenderer renderer;

            public bool           pixelate;
            public int            pixelation_size;

            private System.Random rng;
            private float         last_time;

            public  ComputeShader         gen3dnoise_shader;
            public  ComputeShader         blur_noise_shader;
            public  ComputeShader        scale_noise_shader;
            public  ComputeShader exponential_filter_shader;
            public  ComputeShader         distortion_shader;
            public  ComputeShader      circular_blur_shader;
            public  ComputeShader      circular_mask_shader;
            public  ComputeShader           pixelate_shader;

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
                ComputeBuffer base_buffer = new ComputeBuffer(width * height * depth, sizeof(float));

                for(int layer = 0; layer < layers && layer < 7; layer++) {
                    int  size = 1   << (layers - 1 - layer);
                    int scale = 128 >>               layer;

                    gen3dnoise_shader.SetInt( width_id, width);
                    gen3dnoise_shader.SetInt(height_id, height);
                    gen3dnoise_shader.SetInt( depth_id, depth);
                    gen3dnoise_shader.SetInt(  seed_id, rng.Next());
                    gen3dnoise_shader.SetInt(  size_id, size);
                    gen3dnoise_shader.SetInt( scale_id, scale);

                    gen3dnoise_shader.SetBuffer(0, noise_id, base_buffer);

                    gen3dnoise_shader.Dispatch(0, width / size / 4, height / size / 4, depth / size / 4);
                }

                base_buffer.GetData(alpha);
                base_buffer.Release();

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

                renderer.sprite = Sprite.Create(texture,
                                                new Rect(0, 0, width, height),
                                                new Vector2(0.5f, 0.5f));
            }

            private void Start() {
                generate();

                renderer        = gameObject.AddComponent<SpriteRenderer>();

                renderer.transform.Translate(new Vector3(0.0f, 0.0f, renderer.transform.position.z + 5.0f));

                last_time = Time.time;
            }

            private void Update() {
                float current_time = Time.time - last_time;

                update_texture(current_time);

                renderer.transform.RotateAround(center, new Vector3(0.0f, 0.0f, 1.0f), -current_time * spin);

                last_time = Time.time;
            }
        }
    }
}
    