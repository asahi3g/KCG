using KMath;

namespace Collisions
{


    public class CirclePolygonSweepTest
    {

        public struct PolygonSweepTestResult
        {
            public Vec2f CollisionNormal; // collision normal
            public float CollisionTime; // time of collision
        }


        public static PolygonSweepTestResult TestCollision(Vec2f circleCenter, float circleRadius, Vec2f velocity, Vec2f[] polygonB)
        {

            PolygonSweepTestResult result = new PolygonSweepTestResult{CollisionTime=1.0f};
            for(int j = 0; j < polygonB.Length; j++)
            {
                Line2D testLine = new Line2D(polygonB[j], polygonB[(j == (polygonB.Length - 1)) ? 0 : (j + 1)]);
                Vec2f edge = testLine.B - testLine.A;
                testLine.A += 0.001f;
                testLine.B -= 0.001f;

                var res = CircleLineCollision.TestCollision(circleCenter, circleRadius, velocity, testLine.A, testLine.B);

                if (res.Time < result.CollisionTime)
                {
                    result.CollisionNormal = res.Normal;
                    result.CollisionTime = res.Time;
                }
            }
            


            return result;
        }
    }
}