using KMath;
using System;

namespace Collisions
{



    public partial class SAT
    {


        public static MinimumMagnitudeVector CollisionDetection(Vec2f[] verticesA, Vec2f[] verticesB)
        {
            float overlap = 99999999.0f;
            Vec2f[] axes1 = GetShapeAxis(verticesA);
            Vec2f[] axes2 = GetShapeAxis(verticesB);

            Vec2f smallestAxis = axes1[0];
            // loop over the axes1
            for (int i = 0; i < axes1.Length; i++) 
            {
                Vec2f axis = axes1[i];
                // project both shapes onto the axis

                Projection p1 = ProjectShape(verticesA, axis);
                Projection p2 = ProjectShape(verticesB, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    return new MinimumMagnitudeVector();
                }
                else
                {
                    float overlapValue = p1.OverlapValue(p2);

                    if (p1.Contains(p2) || p2.Contains(p1)) 
                    {
                        // get the overlap plus the distance from the minimum end points
                        float mins = Math.Abs(p1.Min - p2.Min);
                        float maxs = Math.Abs(p1.Max - p2.Max);
                        // NOTE: depending on which is smaller you may need to
                        // negate the separating axis!!
                        if (mins < maxs) 
                        {
                            overlapValue += mins;
                        } else 
                        {
                            overlapValue += maxs;
                        }
                    }

                    if (overlapValue < overlap)
                    {
                        overlap = overlapValue;
                        smallestAxis = axis;
                    }
                }
            }
            // loop over the axes2
            for (int i = 0; i < axes2.Length; i++) 
            {
                Vec2f axis = axes2[i];
                // project both shapes onto the axis
                Projection p1 = ProjectShape(verticesA, axis);
                Projection p2 = ProjectShape(verticesB, axis);
                // do the projections overlap?
                if (!p1.Overlap(p2)) 
                {
                    // then we can guarantee that the shapes do not overlap
                    return new MinimumMagnitudeVector();
                }
                else
                {
                    float overlapValue = p1.OverlapValue(p2);

                    if (p1.Contains(p2) || p2.Contains(p1)) 
                    {
                        // get the overlap plus the distance from the minimum end points
                        float mins = Math.Abs(p1.Min - p2.Min);
                        float maxs = Math.Abs(p1.Max - p2.Max);
                        // NOTE: depending on which is smaller you may need to
                        // negate the separating axis!!
                        if (mins < maxs) 
                        {
                            overlapValue += mins;
                        } else 
                        {
                            overlapValue += maxs;
                        }
                    }

                    if (overlapValue < overlap)
                    {
                        overlap = overlapValue;
                        smallestAxis = axis;
                    }
                }
            }

            
            MinimumMagnitudeVector mtv = new MinimumMagnitudeVector{Axis=smallestAxis, Value=overlap};
            // if we get here then we know that every axis had overlap on it
            // so we can guarantee an intersection
            return mtv;
        }
    }
}