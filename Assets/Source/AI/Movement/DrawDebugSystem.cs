using System;
using KMath;
using UnityEngine;

namespace AI.Movement
{
    public class DrawDebugSystem
    {
        const int MAX_DRAWABLE_PATHS = 16;

        Vec2f[,] Path;
        int[] Length;
        int PathsToDraw = 0;

        Texture2D Texture;

        public DrawDebugSystem()
        {
            Path = new Vec2f[MAX_DRAWABLE_PATHS, PathFinding.MAX_NUM_NODES];
            Length = new int[MAX_DRAWABLE_PATHS];
            Texture = new Texture2D(1, 1);
            Texture.SetPixel(0, 0, new Color(1f, 0f, 0f, 0.4f));
            Texture.Apply();

        }

        public void AddPath(ref Vec2f[] path, int pathLength)
        {
            Array.Copy(path, Path, pathLength);
            Length[PathsToDraw] = pathLength;

            PathsToDraw++;
        }

        public void Draw()
        {
            for (int j = 0; j < PathsToDraw; j++)
            {
                for (int i = 0; i < Length[j]; i++)
                {
                    Gizmos.DrawGUITexture(new Rect(Path[j, i].X, Path[j, i].Y, 1, 1), Texture);

                    if (i > 0)
                        Gizmos.DrawLine(new Vector3(Path[j, i - 1].X, Path[j, i - 1].Y + 0.5f, 0), new Vector3(Path[j, i].X, Path[j, i].Y + 0.5f, 0f));
                }
            }

            Array.Fill(Length, 0);
            PathsToDraw = 0;
        }
    }
}
