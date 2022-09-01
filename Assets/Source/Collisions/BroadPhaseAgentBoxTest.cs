
using Entitas;
using KMath;
using System;

namespace Collisions
{


   

    public static partial class Collisions
    {

        public static int[] BroadphaseAgentBoxTest(Planet.PlanetState planet, AABox2D box)
        {
            int[] result = new int[128];
            int resultCount = 0;

            Agent.AgentList agentList = planet.AgentList;


            for(int i = 0; i < agentList.Length; i++)
            {
                AgentEntity entity = agentList.Get(i);

                if (entity.hasPhysicsBox2DCollider)
                {
                    var physicsState = entity.agentPhysicsState;
                    var collider = entity.physicsBox2DCollider;

                    Vec2f position = physicsState.Position + collider.Offset;

                    AABox2D testBox = new AABox2D(position, collider.Size);

                    if (Collisions.RectOverlapRect(testBox.xmin, testBox.xmax, testBox.ymin, testBox.ymax,
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