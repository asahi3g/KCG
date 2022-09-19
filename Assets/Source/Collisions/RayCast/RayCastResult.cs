    using KMath;
    
    namespace Collisions
    {
        public struct RayCastResult
        {
            public Vec2f Point; // intersection point
            public bool Intersect; // is intersecting
            public Vec2f Normal; // surface normal
        }
    }
