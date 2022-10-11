using KMath;
using System;

namespace Collisions
{


    public class PolygonSweepTest
    {


        public static float TestCollision(Vec2f[] polygonA, Vec2f velocity, Vec2f[] polygonB)
        {

            float result = 1.0f;
            for(int i = 0; i < polygonA.Length; i++)
            {
                Line2D line = new Line2D(polygonA[i], polygonA[(i == (polygonA.Length - 1)) ? 0 : (i + 1)]);
                for(int j = 0; j < polygonB.Length; j++)
                {
                    Line2D testLine = new Line2D(polygonB[j], polygonB[(j == (polygonB.Length - 1)) ? 0 : (j + 1)]);

                    float distance = LineLineSweepTest.TestCollision(line, velocity, testLine.A, testLine.B);

                    result = MathF.Min(result, distance);
                }
            }


            return result;
        }
    }
}