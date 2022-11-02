using KMath;
using System;

namespace Collisions
{



    public static partial class Collisions
    {
        public static int[] BroadphaseParticleCircleTest(Vec2f point, float distance)
        {
            int[] result = new int[128];
            int resultCount = 0;

            Particle.ParticleList list = GameState.Planet.ParticleList;


            for(int i = 0; i < list.Length; i++)
            {
                ParticleEntity entity = list.Get(i);

                if (entity.hasParticleBox2DCollider)
                {
                    var physicsState = entity.particlePhysicsState;
                    var collider = entity.particleBox2DCollider;

                    Vec2f position = physicsState.Position + collider.Offset;



                    var closestX = KMath.KMath.Clamp(point.X, position.X, position.X + collider.Size.X);
                    var closestY = KMath.KMath.Clamp(point.Y, position.Y, position.Y + collider.Size.Y);

                    var distanceX = point.X - closestX;
                    var distanceY = point.Y - closestY;

                    var distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);
                    if (distanceSquared < (distance * distance))
                    {
                        result[resultCount++] = i;
                        if (resultCount == result.Length)
                        {
                            Array.Resize(ref result, result.Length + 128);
                        }
                    }
                }                
            }

            int[] finalResult = new int [resultCount];
            for(int i = 0; i < resultCount; i++)
            {
                finalResult[i] = result[i];
            }

            return finalResult;
        }

    }
}
