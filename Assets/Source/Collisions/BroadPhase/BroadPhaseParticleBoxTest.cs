using KMath;
using System;

namespace Collisions
{


   

    public static partial class Collisions
    {

        public static int[] BroadphaseParticleBoxTest(AABox2D box)
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

                    AABox2D testBox = new AABox2D(position, collider.Size);

                    if (RectOverlapRect(testBox.xmin, testBox.xmax, testBox.ymin, testBox.ymax,
                    box.xmin, box.xmax, box.ymin, box.ymax))
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
