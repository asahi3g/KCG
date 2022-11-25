using UnityEngine;

namespace Utility
{
       // we use this helper function to generate a unity Texture2D
        // from pixels
        internal static class Texture
        {
            public static Texture2D CreateTextureFromRGBA(string name, byte[] rgba, int w, int h)
            {

                Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point
                };
                tex.LoadRawTextureData(rgba);
                tex.name = name;

                /*
                var pixels = new Color32[w * h];
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int index = (x + y * w) * 4;
                        byte r = rgba[index];
                        byte g = rgba[index + 1];
                        byte b = rgba[index + 2];
                        byte a = rgba[index + 3];

                        pixels[x + y * w] = new Color32(r, g, b, a);
                    }
                }
                
                tex.SetPixels32(pixels);
                */
                
                tex.Apply();

                return tex;
            }
        }
}