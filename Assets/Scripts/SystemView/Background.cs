using System;
using System.Threading;

namespace Scripts {
    namespace SystemView {
        public class Background : UnityEngine.MonoBehaviour {
            public int            seed;
            public int            stars;
            public int            width;
            public int            height;

            public UnityEngine.Texture2D      texture;
            public UnityEngine.SpriteRenderer renderer;

            private System.Random rng;

            private void generate() {
                const int threads = 8;

                rng = new(seed);

                UnityEngine.Color[] pixels    = new UnityEngine.Color[width * height];

                // Split random star generation into multiple threads for increased performance.
                Thread[] Ts       = new Thread[threads];
                for(int i = 0; i < threads; i++) {
                    Ts[i] = new Thread((object data) => {
                        System.Random rand = new System.Random((int)data);
                        
                        for(int j = 0; j < stars / threads; j++) {
                            int x = rng.Next(width);
                            int y = rng.Next(height);

                            pixels[x + y * width] = new UnityEngine.Color(0.75f + (float)rand.NextDouble() * 0.25f,
                                                              0.75f + (float)rand.NextDouble() * 0.25f,
                                                              0.75f + (float)rand.NextDouble() * 0.25f,
                                                              0.25f + (float)rand.NextDouble() * 0.75f);
                        }
                    });
                    Ts[i].Start(rng.Next());
                }
                foreach(Thread T in Ts) T.Join();

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

                renderer.transform.Translate(new UnityEngine.Vector3(0.0f, 0.0f, 10.0f));
            }
        }
    }
}
