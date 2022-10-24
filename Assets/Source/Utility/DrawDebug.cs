using KMath;

namespace Utility
{
    public static class DrawDebug
    {
        public static void DrawBox(Vec2f position, Vec2f size)
        {
            var box = new AABox2D(position, size);
            box.DrawBox();
        }
        
        public static void DrawBox(this AABox2D aaBox2D)
        {
            var bottomLeft = new UnityEngine.Vector3(aaBox2D.xmin, aaBox2D.ymin, 0f);
            var bottomRight = new UnityEngine.Vector3(aaBox2D.xmax, aaBox2D.ymin, 0f);
            var topLeft = new UnityEngine.Vector3(aaBox2D.xmin, aaBox2D.ymax, 0f);
            var topRight = new UnityEngine.Vector3(aaBox2D.xmax, aaBox2D.ymax, 0f);
            
            UnityEngine.Debug.DrawLine(bottomLeft, bottomRight, UnityEngine.Color.red);
            UnityEngine.Debug.DrawLine(bottomRight, topRight, UnityEngine.Color.red);
            UnityEngine.Debug.DrawLine(topRight, topLeft, UnityEngine.Color.red);
            UnityEngine.Debug.DrawLine(topLeft, bottomLeft, UnityEngine.Color.red);
        }

        public static void DrawPoint(this Vec2f point)
        {
            UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(point.X, point.Y, 0.0f),
                new UnityEngine.Vector3(point.X + 0.1f, point.Y, 0.0f), UnityEngine.Color.red);
            UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(point.X + 0.1f, point.Y, 0.0f),
                new UnityEngine.Vector3(point.X + 0.1f, point.Y + 0.1f, 0.0f), UnityEngine.Color.red);
        }
    }
}

