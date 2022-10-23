
using KMath;
using System;

namespace Collisions
{


    public struct Projection
    {
        public float Min;
        public float Max;

        public bool Overlap(Projection other)
        {

            return !((other.Min > Max) ||
                (Min > other.Max)); 
        }

        public float OverlapValue(Projection other)
        {
            // make sure they overlap
            if (this.Overlap(other)) {
                return this.Max - other.Min;
            }
            return 0;
        }

        public bool Contains(Projection other)
        {
            return other.Min > Min && other.Max < Max;
        }
    }


    public partial class SAT
    {
        public static Projection ProjectShape(Vec2f[] Vertices, Vec2f axis)
        {
            if (Vertices.Length > 0)
            {
                float min = axis.Dot(Vertices[0]);
                float max = min;

                for (int i = 1; i < Vertices.Length; i++) {
                    // NOTE: the axis must be normalized to get accurate projections
                    float p = axis.Dot(Vertices[i]);
                    if (p < min) {
                        min = p;
                    } else if (p > max) {
                        max = p;
                    }
                }

                Projection proj = new Projection{Min = min, Max = max};

                return proj;
            }
            else
            {
                return new Projection();
            }
        }
    }
}