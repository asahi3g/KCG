//imports UnityEngine

using System;
using Source.SystemView;

namespace Scripts {
    namespace SystemView {
        public class Nebula : UnityEngine.MonoBehaviour {
            public int            seed;
            public int            layers;
            public UnityEngine.Color          basecolor;
            public UnityEngine.Color[]        colors;
            public int            width;
            public int            height;
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

            public UnityEngine.ComputeShader         blur_noise_shader;
            public  UnityEngine.ComputeShader        scale_noise_shader;
            public  UnityEngine.ComputeShader exponential_filter_shader;
            public  UnityEngine.ComputeShader         distortion_shader;
            public  UnityEngine.ComputeShader      circular_blur_shader;
            public  UnityEngine.ComputeShader      circular_mask_shader;
            public  UnityEngine.ComputeShader           pixelate_shader;

            private void generate() {
                // Shaders properties
                int      width_id = UnityEngine.Shader.PropertyToID("width");
                int     height_id = UnityEngine.Shader.PropertyToID("height");
                int      noise_id = UnityEngine.Shader.PropertyToID("noise");
                int      scale_id = UnityEngine.Shader.PropertyToID("scale");
                int   strength_id = UnityEngine.Shader.PropertyToID("strength");
                int     radius_id = UnityEngine.Shader.PropertyToID("radius");
                int distortion_id = UnityEngine.Shader.PropertyToID("distortionnoise");
                int     output_id = UnityEngine.Shader.PropertyToID("outputnoise");
                int    sharpen_id = UnityEngine.Shader.PropertyToID("sharpen");
                
                rng = new(seed);

                UnityEngine.Color[] pixels    = new UnityEngine.Color[width * height];

                for(int i = 0; i < width * height; i++) pixels[i] = new UnityEngine.Color(0.0f, 0.0f, 0.0f, 0.0f);

                // Generate base noise
                UnityEngine.ComputeBuffer base_buffer1 = new UnityEngine.ComputeBuffer(width * height, sizeof(float));
                UnityEngine.ComputeBuffer base_buffer2 = new UnityEngine.ComputeBuffer(width * height / 4096, sizeof(float));

                base_buffer2.SetData(ProceduralImages.generate_noise(rng, 1.0f, width / 64, height / 64));
                    
                // Scale noise  
                scale_noise_shader.SetInt( width_id, width);
                scale_noise_shader.SetInt(height_id, height);
                scale_noise_shader.SetInt( scale_id, 64);

                scale_noise_shader.SetBuffer(0,  noise_id, base_buffer2);
                scale_noise_shader.SetBuffer(0, output_id, base_buffer1);

                scale_noise_shader.Dispatch(0, width / 8, height / 8, 1);

                // Generate distortion noise
                base_buffer2.Release();
                base_buffer2 = new UnityEngine.ComputeBuffer(width * height / 1024, sizeof(float));
                base_buffer2.SetData(ProceduralImages.generate_noise(rng, 1.0f, width / 32, height / 32));

                UnityEngine.ComputeBuffer distortion_buffer = new UnityEngine.ComputeBuffer(width * height, sizeof(float));

                // Scale distortion noise
                scale_noise_shader.SetInt( width_id, width);
                scale_noise_shader.SetInt(height_id, height);
                scale_noise_shader.SetInt( scale_id, 32);

                scale_noise_shader.SetBuffer(0,  noise_id, base_buffer2);
                scale_noise_shader.SetBuffer(0, output_id, distortion_buffer);

                scale_noise_shader.Dispatch(0, width / 8, height / 8, 1);

                base_buffer2.Release();

                // Apply distortion
                base_buffer2 = new UnityEngine.ComputeBuffer(width * height, sizeof(float));

                distortion_shader.SetInt( width_id, width);
                distortion_shader.SetInt(height_id, height);

                distortion_shader.SetFloat(strength_id, 24.0f);

                distortion_shader.SetBuffer(0, distortion_id, distortion_buffer);
                distortion_shader.SetBuffer(0,      noise_id, base_buffer1);
                distortion_shader.SetBuffer(0,     output_id, base_buffer2);

                distortion_shader.Dispatch(0, width / 8, height / 8, 1);

                distortion_buffer.Release();

                float[] base_alpha = new float[width * height];

                base_buffer2.GetData(base_alpha);
                base_alpha = ProceduralImages.soften(base_alpha, 4, width, height);
                base_buffer2.SetData(base_alpha);

                // Apply circular blur
                circular_blur_shader.SetInt( width_id, width);
                circular_blur_shader.SetInt(height_id, height);

                circular_blur_shader.SetFloat(radius_id, 16.0f);

                circular_blur_shader.SetBuffer(0,  noise_id, base_buffer2);
                circular_blur_shader.SetBuffer(0, output_id, base_buffer1);

                circular_blur_shader.Dispatch(0, width / 8, height / 8, 1);

                // Apply circular mask
                circular_mask_shader.SetInt( width_id, width);
                circular_mask_shader.SetInt(height_id, height);

                circular_mask_shader.SetBool(sharpen_id, false);

                circular_mask_shader.SetBuffer(0, noise_id, base_buffer1);

                circular_mask_shader.Dispatch(0, width / 8, height / 8, 1);
                
                if(pixelate) {
                    pixelate_shader.SetInt( width_id, width);
                    pixelate_shader.SetInt(height_id, height);
                    pixelate_shader.SetInt(radius_id, pixelation_size);

                    pixelate_shader.SetBuffer(0,  noise_id, base_buffer1);
                    pixelate_shader.SetBuffer(0, output_id, base_buffer2);

                    pixelate_shader.Dispatch(0, width / 8, height / 8, 1);

                    base_buffer2.GetData(base_alpha);
                    base_buffer2.Release();
                    base_buffer1.Release();
                } else {
                    base_buffer1.GetData(base_alpha);
                    base_buffer1.Release();
                    base_buffer2.Release();
                }

                for(int x = 0; x < width; x++)
                    for(int y = 0; y < height; y++) {
                        pixels[x + y * width].r = basecolor.r;
                        pixels[x + y * width].g = basecolor.g;
                        pixels[x + y * width].b = basecolor.b;
                        pixels[x + y * width].a = base_alpha[x + y * width];
                    }

                float max_dist = Tools.get_distance(0, 0, width, height);

                foreach(UnityEngine.Color color in colors) {
                    float[] alpha = new float[width * height];
                    UnityEngine.ComputeBuffer color_buffer1 = new UnityEngine.ComputeBuffer(width * height, sizeof(float));

                    for(int layer = 0; layer < layers; layer++) {

                        int   scale         = 1 << layers - layer - 1;
                        float strength      = scale / layers;

                        UnityEngine.ComputeBuffer layer_buffer = new UnityEngine.ComputeBuffer(width / scale * height / scale, sizeof(float));

                        // Generate random noise
                        layer_buffer.SetData(ProceduralImages.generate_noise(rng, strength, width / scale, height / scale));

                        // Blur noise
                        blur_noise_shader.SetInt( width_id, width  / scale);
                        blur_noise_shader.SetInt(height_id, height / scale);

                        blur_noise_shader.SetBuffer(0, noise_id, layer_buffer);

                        blur_noise_shader.Dispatch(0, width / scale / 8, height / scale / 8, 1);

                        // Scale noise
                        scale_noise_shader.SetInt( width_id, width);
                        scale_noise_shader.SetInt(height_id, height);
                        scale_noise_shader.SetInt( scale_id, scale);

                        scale_noise_shader.SetBuffer(0,  noise_id, layer_buffer);
                        scale_noise_shader.SetBuffer(0, output_id, color_buffer1);

                        scale_noise_shader.Dispatch(0, width / 8, height / 8, 1);

                        layer_buffer.Release();
                    }

                    // Apply exponential filter
                    exponential_filter_shader.SetInt( width_id, width);
                    exponential_filter_shader.SetInt(height_id, height);

                    exponential_filter_shader.SetBuffer(0, noise_id, color_buffer1);

                    exponential_filter_shader.Dispatch(0, width / 8, height / 8, 1);

                    // Apply mask
                    color_buffer1.GetData(alpha);
                    color_buffer1.SetData(ProceduralImages.mask(rng, alpha, 8, width, height));

                    // Generate distortion noise
                    UnityEngine.ComputeBuffer color_buffer2 = new UnityEngine.ComputeBuffer(width * height / 4096, sizeof(float));
                    color_buffer2.SetData(ProceduralImages.generate_noise(rng, 1.0f, width / 64, height / 64));

                    // Scale distortion noise
                    UnityEngine.ComputeBuffer distortion_noise = new UnityEngine.ComputeBuffer(width * height, sizeof(float));

                    scale_noise_shader.SetInt( width_id, width);
                    scale_noise_shader.SetInt(height_id, height);
                    scale_noise_shader.SetInt( scale_id, 64);

                    scale_noise_shader.SetBuffer(0,  noise_id, color_buffer2);
                    scale_noise_shader.SetBuffer(0, output_id, distortion_noise);

                    scale_noise_shader.Dispatch(0, width / 8, height / 8, 1);

                    color_buffer2.Release();

                    // Apply distortion
                    color_buffer2 = new UnityEngine.ComputeBuffer(width * height, sizeof(float));

                    distortion_shader.SetInt( width_id, width);
                    distortion_shader.SetInt(height_id, height);

                    distortion_shader.SetFloat(strength_id, 32.0f);

                    distortion_shader.SetBuffer(0, distortion_id, distortion_noise);
                    distortion_shader.SetBuffer(0,      noise_id, color_buffer1);
                    distortion_shader.SetBuffer(0,     output_id, color_buffer2);

                    distortion_shader.Dispatch(0, width / 8, height / 8, 1);

                    distortion_noise.Release();

                    // Apply circular blur
                    circular_blur_shader.SetInt(width_id, width);
                    circular_blur_shader.SetInt(height_id, height);

                    circular_blur_shader.SetFloat(radius_id, 4.0f);

                    circular_blur_shader.SetBuffer(0,  noise_id, color_buffer2);
                    circular_blur_shader.SetBuffer(0, output_id, color_buffer1);

                    circular_blur_shader.Dispatch(0, width / 8, height / 8, 1);

                    // Soften noise
                    color_buffer1.GetData(alpha);
                    color_buffer1.SetData(ProceduralImages.soften(alpha, 8, width, height));

                    // Apply circular mask
                    circular_mask_shader.SetInt( width_id, width);
                    circular_mask_shader.SetInt(height_id, height);

                    circular_mask_shader.SetBool(sharpen_id, false);

                    circular_mask_shader.SetBuffer(0, noise_id, color_buffer1);

                    circular_mask_shader.Dispatch(0, width / 8, height / 8, 1);

                    if(pixelate) {
                        pixelate_shader.SetInt( width_id, width);
                        pixelate_shader.SetInt(height_id, height);
                        pixelate_shader.SetInt(radius_id, pixelation_size);

                        pixelate_shader.SetBuffer(0,  noise_id, color_buffer1);
                        pixelate_shader.SetBuffer(0, output_id, color_buffer2);

                        pixelate_shader.Dispatch(0, width / 8, height / 8, 1);

                        color_buffer2.GetData(alpha);
                        color_buffer2.Release();
                        color_buffer1.Release();
                    } else {
                        color_buffer1.GetData(alpha);
                        color_buffer1.Release();
                        color_buffer2.Release();
                    }


                    for(int x = 0; x < width; x++)
                        for(int y = 0; y < height; y++) {
                            float a                 = Tools.smootherstep(alpha[x + y * width] * 0.3f, 0.0f, 1.0f - pixels[x + y * width].a);

                            float blended_alpha     =            a +                           pixels[x + y * width].a * (1.0f - a);
                            pixels[x + y * width].r = (color.r * a + pixels[x + y * width].r * pixels[x + y * width].a * (1.0f - a)) / blended_alpha;
                            pixels[x + y * width].g = (color.g * a + pixels[x + y * width].g * pixels[x + y * width].a * (1.0f - a)) / blended_alpha;
                            pixels[x + y * width].b = (color.b * a + pixels[x + y * width].b * pixels[x + y * width].a * (1.0f - a)) / blended_alpha;
                            pixels[x + y * width].a = blended_alpha;
                        }
                }


                for(int i = 0; i < width * height; i++) {
                    // Adjust contrast
                    ProceduralImages.adjust_contrast(ref pixels[i].r, ref pixels[i].g, ref pixels[i].b, contrast);

                    // Adjust black level
                    ProceduralImages.adjust_black_level(ref pixels[i].r, ref pixels[i].g, ref pixels[i].b, ref pixels[i].a, cutoff);

                    // Adjust opacity
                    pixels[i].a *= opacity;
                }

                texture = new UnityEngine.Texture2D(width, height);
                texture.filterMode = UnityEngine.FilterMode.Trilinear;
                texture.SetPixels(pixels);
                texture.Apply();

            }

            private void Start() {
                generate();

                renderer        = gameObject.AddComponent<UnityEngine.SpriteRenderer>();
                renderer.sprite = UnityEngine.Sprite.Create(texture,
                                                new UnityEngine.Rect(0, 0, width, height),
                                                new UnityEngine.Vector2(0.5f, 0.5f));

                renderer.transform.Translate(new UnityEngine.Vector3(0.0f, 0.0f, renderer.transform.position.z + 5.0f));


                last_time = UnityEngine.Time.time;
            }

            private void Update() {
                float current_time = UnityEngine.Time.time - last_time;
                      last_time    = UnityEngine.Time.time;

                renderer.transform.RotateAround(center, new UnityEngine.Vector3(0.0f, 0.0f, 1.0f), -current_time * spin);

            }
        }
    }
}
    