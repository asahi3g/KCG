
using Entitas;
using KMath;
using System;

namespace Collisions
{



    public static partial class Collisions
    {
        public static int[] BroadphaseAgentCircleTest(Planet.PlanetState planet, Vec2f point, float distance)
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