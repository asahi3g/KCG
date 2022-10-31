using KMath;

namespace Collisions
{


    public class PolygonSweepTest
    {

        public struct PolygonSweepTestResult
        {
            public Vec2f CollisionNormal; // collision normal
            public float CollisionTime; // time of collision
        }


        public static PolygonSweepTestResult TestCollision(Vec2f[] polygonA, Vec2f velocity, Vec2f[] polygonB)
        {

            PolygonSweepTestResult result = new PolygonSweepTestResult{CollisionTime=1.0f};
            for(int i = 0; i < polygonA.Length; i++)
            {
                Line2D line = new Line2D(polygonA[i], polygonA[(i == (polygonA.Length - 1)) ? 0 : (i + 1)]);
                for(int j = 0; j < polygonB.Length; j++)
                {
                    Line2D testLine = new Line2D(polygonB[j], polygonB[(j == (polygonB.Length - 1)) ? 0 : (j + 1)]);
                    Vec2f edge = testLine.B - testLine.A;
                    testLine.A += edge * 0.000001f;
                    testLine.B -= edge * 0.000001f;

                    float distance = LineLineSweepTest.TestCollision(line, velocity, testLine.A, testLine.B);

                    if (distance < result.CollisionTime)
                    {
                        result.CollisionNormal = new Vec2f(edge.Y, -edge.X).Normalize();
                        result.CollisionTime = distance;
                    }
                }
            }


            return result;
        }
    }
}