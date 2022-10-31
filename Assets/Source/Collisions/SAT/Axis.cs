using KMath;

namespace Collisions
{



    public partial class SAT
    {
        public static Vec2f[] GetShapeAxis(Vec2f[] Vertices)
        {
            Vec2f[] axes = new Vec2f[Vertices.Length];
            // loop over the vertices
            for (int i = 0; i < Vertices.Length; i++) 
            {
                // get the current vertex
                Vec2f p1 = Vertices[i];
                // get the next vertex
                Vec2f p2 = Vertices[(i + 1 == Vertices.Length) ? 0 : i + 1];
                // subtract the two to get the edge vector
                Vec2f edge = p2 - p1;
                // get either perpendicular vector
                Vec2f normal = new Vec2f(edge.Y, -edge.X);

                // the perp method is just (x, y) =&gt; (-y, x) or (y, -x)
                axes[i] = normal.Normalize();
            }

            return axes;
        }
    }
}