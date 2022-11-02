//imports UnityEngine

using System;
using KMath;


namespace AI.Movement
{
    public class DrawDebugSystem
    {
        const int MAX_DRAWABLE_PATHS = 16;

        Vec2f[,] Path;
        int[] Length;
        int PathsToDraw = 0;
        int CurrentFrame = 0;

        UnityEngine.Texture2D Texture;

        public DrawDebugSystem()
        {
            Path = new Vec2f[MAX_DRAWABLE_PATHS, PathFinding.MAX_NUM_NODES];
            Length = new int[MAX_DRAWABLE_PATHS];
            Texture = new UnityEngine.Texture2D(1, 1);
            Texture.SetPixel(0, 0, new UnityEngine.Color(1f, 0f, 0f, 0.4f));
            Texture.Apply();

        }

        public void AddPath(ref Vec2f[] path, int pathLength)
        {
            if (CurrentFrame != UnityEngine.Time.frameCount)
            {
                CurrentFrame = UnityEngine.Time.frameCount;
                PathsToDraw = 0;
                Array.Fill(Length, 0);
            }

            for (int i = 0; i < pathLength; i++)
            {
                Path[PathsToDraw, i] = path[i];
            }
            Length[PathsToDraw] = pathLength;

            PathsToDraw++;
        }

        public void Draw()
        {
            for (int j = 0; j < PathsToDraw; j++)
            {
                for (int i = 0; i < Length[j]; i++)
                {
                    UnityEngine.Gizmos.DrawGUITexture(new UnityEngine.Rect(Path[j, i].X, Path[j, i].Y, 1, 1), Texture);

                    if (i > 0)
                        UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Path[j, i - 1].X, Path[j, i - 1].Y + 0.5f, 0), new UnityEngine.Vector3(Path[j, i].X, Path[j, i].Y + 0.5f, 0f));
                }
            }

            PathsToDraw = 0;
            Array.Fill(Length, 0);
        }
    }
}
