//imports UnityEngine

using Collisions;
using KMath;
using System.Collections.Generic;


namespace Particle
{
    public static class CircleSmoke
    {
        // Lists
        private static List<UnityEngine.MeshRenderer> Smokes = new();
        private static List<Vec2f> Velocities = new();
        private static List<Vec2f> Positions = new();
        private static List<Vec2f> Scales = new();
        private static List<AABox2D> Collisions = new();
        private static List<UnityEngine.Material> Materials = new();

        public static void Spawn(int count, Vec2f position, Vec2f velocity, Vec2f scaleVelocity)
        {
            // Spawn "count" Particle at "position"
            // Veloctity to give wind effect (veloicty over time or default velocity?)
            // Scale to give physics effects (scale over time)
            // Apply Toon Smoke Material

            for (int i = 0; i < count; i++) 
            {
                UnityEngine.GameObject CircleSmoke = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere);
                CircleSmoke.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
                UnityEngine.Object.Destroy(CircleSmoke.GetComponent<UnityEngine.SphereCollider>());
                UnityEngine.MeshRenderer meshRenderer = CircleSmoke.GetComponent<UnityEngine.MeshRenderer>();
                CircleSmoke.name = "CircleSmoke";

                UnityEngine.Material SmokeMaterial = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load("Materials\\ToonShader\\Smoke", typeof(UnityEngine.Material)) as UnityEngine.Material);

                meshRenderer.material = SmokeMaterial;

                AABox2D collision = new AABox2D(new Vec2f(CircleSmoke.transform.position.x, CircleSmoke.transform.position.y),
                    new Vec2f(CircleSmoke.transform.localScale.x, CircleSmoke.transform.localScale.y));

                CircleSmoke.transform.localScale = new UnityEngine.Vector3(0.5f, 1.0f, 1.0f);
                CircleSmoke.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                var color = UnityEngine.Random.Range(0.7f, 0.8f);
                SmokeMaterial.color = new UnityEngine.Color(color, color, color, 0.8f);

                Smokes.Add(meshRenderer);
                Positions.Add(velocity);
                Velocities.Add(velocity);
                Scales.Add(scaleVelocity);
                Collisions.Add(collision);
                Materials.Add(SmokeMaterial);
            }
        }

        public static void SpawnFlare(int count, Vec2f position, Vec2f velocity, Vec2f scaleVelocity)
        {
            // Spawn "count" Particle at "position"
            // Veloctity to give wind effect (veloicty over time or default velocity?)
            // Scale to give physics effects (scale over time)
            // Apply Toon Smoke Material

            for (int i = 0; i < count; i++)
            {
                UnityEngine.GameObject CircleSmoke = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere);
                CircleSmoke.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
                UnityEngine.Object.Destroy(CircleSmoke.GetComponent<UnityEngine.SphereCollider>());
                UnityEngine.MeshRenderer meshRenderer = CircleSmoke.GetComponent<UnityEngine.MeshRenderer>();
                CircleSmoke.name = "CircleSmoke";

                UnityEngine.Material SmokeMaterial = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load("Materials\\ToonShader\\Smoke", typeof(UnityEngine.Material)) as UnityEngine.Material);

                meshRenderer.material = SmokeMaterial;

                AABox2D collision = new AABox2D(new Vec2f(CircleSmoke.transform.position.x, CircleSmoke.transform.position.y),
                    new Vec2f(CircleSmoke.transform.localScale.x, CircleSmoke.transform.localScale.y));

                CircleSmoke.transform.localScale = new UnityEngine.Vector3(0.5f, 1.0f, 1.0f);
                CircleSmoke.transform.position = new UnityEngine.Vector3(position.X, position.Y, -1.0f);

                var color = UnityEngine.Random.Range(0.6f, 0.9f);
                SmokeMaterial.color = new UnityEngine.Color(color, 0, 0, 0.8f);

                Smokes.Add(meshRenderer);
                Positions.Add(velocity);
                Velocities.Add(velocity);
                Scales.Add(scaleVelocity);
                Collisions.Add(collision);
                Materials.Add(SmokeMaterial);
            }
        }

        public static void Spawn(VehicleEntity vehicle, int count, Vec2f position, Vec2f velocity, Vec2f scaleVelocity)
        {
            // Spawn "count" Particle at "position"
            // Veloctity to give wind effect (veloicty over time or default velocity?)
            // Scale to give physics effects (scale over time)
            // Apply Toon Smoke Material
             
            for (int i = 0; i < count; i++)
            {
                UnityEngine.GameObject CircleSmoke = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere);
                CircleSmoke.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
                UnityEngine.Object.Destroy(CircleSmoke.GetComponent<UnityEngine.SphereCollider>());
                UnityEngine.MeshRenderer meshRenderer = CircleSmoke.GetComponent<UnityEngine.MeshRenderer>();
                CircleSmoke.name = "CircleSmoke";

                UnityEngine.Material SmokeMaterial = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load("Materials\\ToonShader\\Smoke", typeof(UnityEngine.Material)) as UnityEngine.Material);

                meshRenderer.material = SmokeMaterial;

                AABox2D collision = new AABox2D(new Vec2f(CircleSmoke.transform.position.x, CircleSmoke.transform.position.y),
                    new Vec2f(CircleSmoke.transform.localScale.x, CircleSmoke.transform.localScale.y));

                CircleSmoke.transform.localScale = new UnityEngine.Vector3(0.5f, 1.0f, 1.0f);
                CircleSmoke.transform.position = new UnityEngine.Vector3(position.X, position.Y, 1.0f);

                var colorR = UnityEngine.Random.Range(0.7f, 0.8f);
                var colorG = UnityEngine.Random.Range(0.35f, 0.45f);
                SmokeMaterial.color = new UnityEngine.Color(colorR, colorG, 0.0f, 1.0f);

                Smokes.Add(meshRenderer); 
                Positions.Add(position);
                Velocities.Add(velocity);
                Scales.Add(scaleVelocity);
                Collisions.Add(collision);
                Materials.Add(SmokeMaterial);
            }
        }

        public static void Update()
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

                        Materials[i].color = new UnityEngine.Color(Materials[i].color.r, Materials[i].color.g, Materials[i].color.b,
                            UnityEngine.Mathf.Lerp(Materials[i].color.a, 0.0f, UnityEngine.Random.Range(0.05f, 0.4f) * UnityEngine.Time.deltaTime));

                        Smokes[i].transform.position += new UnityEngine.Vector3(UnityEngine.Random.Range(0.0f, Velocities[i].X + UnityEngine.Random.Range(-7, 7)), UnityEngine.Random.Range(0.0f, Velocities[i].Y + UnityEngine.Random.Range(0, 2)), 0.0f) * UnityEngine.Time.deltaTime;
                        Smokes[i].transform.localScale += new UnityEngine.Vector3(UnityEngine.Random.Range(0.0f, Scales[i].X), UnityEngine.Random.Range(0.0f, Scales[i].Y), 0.0f) * UnityEngine.Time.deltaTime;

                        AABox2D tempCollision = Collisions[i];
                        if (tempCollision.IsCollidingTop(Velocities[i]))
                        {
                            Smokes[i].transform.position += new UnityEngine.Vector3(0f, UnityEngine.Random.Range(0.0f, -Velocities[i].Y - UnityEngine.Random.Range(0, 12)), 0.0f) * UnityEngine.Time.deltaTime;
                        }

                        if(tempCollision.IsCollidingRight(Velocities[i]))
                        {
                            Smokes[i].transform.position += new UnityEngine.Vector3(UnityEngine.Random.Range(0.0f, -Velocities[i].X - UnityEngine.Random.Range(-1, 12)), 0f) * UnityEngine.Time.deltaTime;
                        }
                        else if(tempCollision.IsCollidingLeft(Velocities[i]))
                        {
                            Smokes[i].transform.position += new UnityEngine.Vector3(UnityEngine.Random.Range(0.0f, Velocities[i].X + UnityEngine.Random.Range(-1, 12)), 0f) * UnityEngine.Time.deltaTime;
                        }
                        Collisions[i] = tempCollision;

                        if (Materials[i].color.a <= 0.5f)
                            UnityEngine.Object.Destroy(Smokes[i].gameObject);
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
                        UnityEngine.Gizmos.DrawCube(new UnityEngine.Vector3(Collisions[i].center.X, Collisions[i].center.Y, 0.0f), new UnityEngine.Vector3(Collisions[i].halfSize.X, Collisions[i].halfSize.Y, 0.0f));
                    }
                }
            }
        }
    }
}
