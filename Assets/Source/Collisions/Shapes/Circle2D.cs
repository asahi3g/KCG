namespace KMath
{
    public struct Circle2D
    {
        public Vec2f Center;
        public float Radius;

        //Check to see if the sphere overlaps the AABB
        public bool InterSectionAABB(ref AABox2D box)
        {
            float s, d = 0;
            //find the square of the distance
            //from the sphere to the box

            if (Center.X < box.xmin)
            {
                s = Center.X - box.xmin;
                d += s * s;
            }
            else if (Center.X > box.xmax)
            {

                s = Center.X - box.xmax;
                d += s * s;
            }

            if (Center.Y < box.ymin)
            {
                s = Center.Y - box.ymin;
                d += s * s;
            }
            else if (Center.Y > box.ymax)
            {

                s = Center.Y - box.ymax;
                d += s * s;
            }

            return d <= Radius * Radius;
        }
    }
}
