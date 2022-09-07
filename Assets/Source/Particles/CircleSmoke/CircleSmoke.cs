using KMath;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Particle
{
    public static class CircleSmoke
    {
        private static List<SpriteRenderer> Smokes = new();
        private static List<Vec2f> Velocities = new();
        private static List<Vec2f> Positions = new();
        private static List<Vec2f> Scales = new();
        private static Sprite sprite;

        public static void Initialize()
        {
            Vector2Int iconPngSize = new Vector2Int(256, 256);

            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_circle.png", iconPngSize.x, iconPngSize.y);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Particle);
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Particle);
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);
            sprite = Sprite.Create(iconTex, new Rect(0, 0, iconPngSize.x, iconPngSize.y), new Vector2(0.5f, 0.5f));
        }

        public static void Spawn(int count, Vec2f position, Vec2f velocity, Vec2f scaleVelocity)
        {
            for(int i = 0; i < count; i++)
            {
                GameObject CircleSmoke = new GameObject("SmokeParticle");
                SpriteRenderer spriteRenderer = CircleSmoke.AddComponent<SpriteRenderer>();

                CircleSmoke.transform.localScale = new Vector2(0.1f, 0.1f);
                CircleSmoke.transform.position = new Vector2(position.X, position.Y);

                spriteRenderer.sprite = sprite;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, 0.8f);

                Smokes.Add(spriteRenderer);
                Positions.Add(velocity);
                Velocities.Add(velocity);
                Scales.Add(scaleVelocity);
            }
        }

        public static void Update()
        {
            if(Smokes.Count > 0)
            {
                for(int i = 0; i < Smokes.Count; i++)
                {
                    if(Smokes[i] != null)
                    {
                        Smokes[i].color = new Color(Smokes[i].color.r, Smokes[i].color.g, Smokes[i].color.b,
                            Mathf.Lerp(Smokes[i].color.a, 0.0f, Random.Range(0.2f, 0.8f) * Time.deltaTime));

                        Smokes[i].transform.position += new Vector3(Random.Range(0.0f, Velocities[i].X + Random.Range(-1, 3)), Random.Range(0.0f, Velocities[i].Y + Random.Range(0, 3)), 0.0f) * Time.deltaTime;
                        Smokes[i].transform.localScale += new Vector3(Random.Range(0.0f, Scales[i].X), Random.Range(0.0f, Scales[i].Y), 0.0f) * Time.deltaTime;

                        if (Smokes[i].color.a <= 0.1f)
                            GameObject.Destroy(Smokes[i].gameObject);
                    }
                }
            }
        }
    }
}
