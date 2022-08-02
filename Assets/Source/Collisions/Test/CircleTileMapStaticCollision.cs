using KMath;

namespace CollisionsTest
{
    public static class CircleTileMapStaticCollision
    {
        //Check to see if the sphere overlaps the AABB
        public static bool AABBOverlapsSphere(ref AABox2D box, float radius, Vec2f center)
        {
            float s, d = 0;
            //find the square of the distance
            //from the sphere to the box

            if (center.X < box.xmin)
            {
                s = center.X - box.xmin;
                d += s * s;
            }
            else if (center.X > box.xmax)
            {

                s = center.X - box.xmax;
                d += s * s;
            }
            
            if (center.Y < box.ymin)
            {
                s = center.Y - box.ymin;
                d += s * s;
            }
            else if (center.Y > box.ymax)
            {

                s = center.Y - box.ymax;
                d += s * s;
            }

            return d <= radius * radius;
        }
    }
}

