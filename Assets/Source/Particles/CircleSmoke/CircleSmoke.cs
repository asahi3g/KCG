using Collisions;
using Entitas;
using KMath;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Particle
{
    public static class CircleSmoke
    {
        // Lists
        private static List<MeshRenderer> Smokes = new();
        private static List<Vec2f> Velocities = new();
        private static List<Vec2f> Positions = new();
        private static List<Vec2f> Scales = new();
        private static List<AABox2D> Collisions = new();
        private static List<Material> Materials = new();

        // Smoke Circle Sprite
        private static Sprite sprite;

        public static void Initialize()
        {
            // Create, Initialize and Copy Sprite To Atlas Once.
            // Set Created Sprite To Global Sprite
            // Because, we only have one sprite for circle smoke effect

            // Initialize Once, Use it many times

            //Vector2Int iconPngSize = new Vector2Int(300, 300);

            //var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_circle.png", iconPngSize.x, iconPngSize.y);

            //int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Particle);

            //byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            //GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Particle);

            //Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            //sprite = Sprite.Create(iconTex, new Rect(0, 0, iconPngSize.x, iconPngSize.y), new Vector2(0.5f, 0.5f));
        }

        public static void Spawn(int count, Vec2f position, Vec2f velocity, Vec2f scaleVelocity)
        {
            // Spawn "count" Particle at "position"
            // Veloctity to give wind effect (veloicty over time or default velocity?)
            // Scale to give physics effects (scale over time)

            for (int i = 0; i < count; i++) 
            {
                GameObject CircleSmoke = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                GameObject.Destroy(CircleSmoke.GetComponent<SphereCollider>());
                MeshRenderer meshRenderer = CircleSmoke.GetComponent<MeshRenderer>();
                CircleSmoke.name = "CircleSmoke";

                Material SmokeMaterial = MonoBehaviour.Instantiate(Resources.Load("Materials\\ToonShader\\Smoke", typeof(Material)) as Material);

                meshRenderer.material = SmokeMaterial;

                AABox2D collision = new AABox2D(new Vec2f(CircleSmoke.transform.position.x, CircleSmoke.transform.position.y),
                    new Vec2f(CircleSmoke.transform.localScale.x, CircleSmoke.transform.localScale.y));

                CircleSmoke.transform.localScale = new Vector3(0.5f, 1.0f, 1.0f);
                CircleSmoke.transform.position = new Vector3(position.X, position.Y, 0.0f);

                var color = Random.Range(0.7f, 0.8f);
                SmokeMaterial.color = new Color(color, color, color, 0.8f);

                Smokes.Add(meshRenderer);
                Positions.Add(velocity);
                Velocities.Add(velocity);
                Scales.Add(scaleVelocity);
                Collisions.Add(collision);
                Materials.Add(SmokeMaterial);
            }
        }

        public static void Update(ref PlanetTileMap.TileMap tileMap)
        {
            // Decrease Alpha Blending over time
            // Apply Velocity
            // Apply Scale Over Time
            // Update Collision Physics

            if(Smokes.Count > 0)
            {
                for(int i = 0; i < Smokes.Count; i++)
                {
                    if(Smokes[i] != null)
                    {
                        Collisions[i] = new AABox2D(new Vec2f(Smokes[i].gameObject.transform.position.x,
                            Smokes[i].gameObject.transform.position.y), new Vec2f(Smokes[i].gameObject.transform.localScale.x,
                                Smokes[i].gameObject.transform.localScale.y));

                        Materials[i].color = new Color(Materials[i].color.r, Materials[i].color.g, Materials[i].color.b,
                            Mathf.Lerp(Materials[i].color.a, 0.0f, Random.Range(0.05f, 0.4f) * Time.deltaTime));

                        Smokes[i].transform.position += new Vector3(Random.Range(0.0f, Velocities[i].X + Random.Range(-7, 7)), Random.Range(0.0f, Velocities[i].Y + Random.Range(0, 2)), 0.0f) * Time.deltaTime;
                        Smokes[i].transform.localScale += new Vector3(Random.Range(0.0f, Scales[i].X), Random.Range(0.0f, Scales[i].Y), 0.0f) * Time.deltaTime;

                        AABox2D tempCollision = Collisions[i];
                        if (tempCollision.IsCollidingTop(tileMap, Velocities[i]))
                        {
                            Smokes[i].transform.position += new Vector3(0f, Random.Range(0.0f, -Velocities[i].Y - Random.Range(0, 12)), 0.0f) * Time.deltaTime;
                        }

                        if(tempCollision.IsCollidingRight(tileMap, Velocities[i]))
                        {
                            Smokes[i].transform.position += new Vector3(Random.Range(0.0f, -Velocities[i].X - Random.Range(-1, 12)), 0f) * Time.deltaTime;
                        }
                        else if(tempCollision.IsCollidingLeft(tileMap, Velocities[i]))
                        {
                            Smokes[i].transform.position += new Vector3(Random.Range(0.0f, Velocities[i].X + Random.Range(-1, 12)), 0f) * Time.deltaTime;
                        }
                        Collisions[i] = tempCollision;

                        if (Materials[i].color.a <= 0.05f)
                            GameObject.Destroy(Smokes[i].gameObject);
                    }
                }
            }
        }

        public static void DrawGizmos()
        {
            if (Smokes.Count > 0)
            {
                for (int i = 0; i < Smokes.Count; i++)
                {
                    if (Smokes[i] != null)
                    {
                        Gizmos.DrawCube(new Vector3(Collisions[i].center.X, Collisions[i].center.Y, 0.0f), new Vector3(Collisions[i].halfSize.X, Collisions[i].halfSize.Y, 0.0f));
                    }
                }
            }
        }
    }
}
