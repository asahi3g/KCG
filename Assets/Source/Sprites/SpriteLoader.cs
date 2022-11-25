using BigGustave;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sprites
{
    public class SpriteLoader
    {
        public SpriteSheet[] SpriteSheets;
        public Dictionary<string, int> SpriteSheetID;

        public SpriteLoader()
        {
            SpriteSheets = Array.Empty<SpriteSheet>();
            SpriteSheetID = new Dictionary<string, int>();
        }

        public void InitStage1()
        {
            
        }

        public void InitStage2()
        {
        
        }

        public int GetSpriteSheetID(string filename, int spriteWidth, int spriteHeight)
        {
            if (SpriteSheetID.ContainsKey(filename)) return SpriteSheetID[filename];
            return LoadImageFile(filename, spriteWidth, spriteHeight);
        }

        private int LoadImageFile(string filename, int spriteWidth, int spriteHeight)
        {
            int imageCount = SpriteSheets.Length + 1;

            Array.Resize(ref SpriteSheets, imageCount);

            SpriteSheetID.Add(filename, imageCount - 1);

            Png data = Png.Open(filename);

            int w = data.Header.Width;
            int h = data.Header.Height;

            //spriteWidth = w;
            //spriteHeight = h;

            bool wMatch = w % spriteWidth == 0;
            bool hMatch = h % spriteHeight == 0;

            if (!wMatch || !hMatch)
            {
                Debug.LogWarning($"{nameof(SpriteLoader)} LoadImageFile() filename[{filename.Color(Color.cyan)}] spriteWidth[{spriteWidth.ToString().Color(wMatch ? Color.green : Color.red)}] or spriteHeight[{spriteHeight.ToString().Color(hMatch ? Color.green : Color.red)}] does not match PNG w[{w.ToString().Color(wMatch ? Color.green : Color.red)}] h[{h.ToString().Color(hMatch ? Color.green : Color.red)}]");
            }
            
            SpriteSheet sheet = new SpriteSheet
            {
                Index = imageCount - 1,
                Width = w,
                Height = h,
                SpriteWidth = spriteWidth,
                SpriteHeight = spriteHeight,
                Data = new byte[4 * w * h]
            };


            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Pixel pixel = data.GetPixel(x, y);
                    //int index = y * w + x; // this made all pixels (textures, sprites, tiles, atlases) inverted on y axis
                    int index = (h - 1 - y) * w + x; // invert on y axis
                    
                    sheet.Data[4 * index + 0] = pixel.R;
                    sheet.Data[4 * index + 1] = pixel.G;
                    sheet.Data[4 * index + 2] = pixel.B;
                    sheet.Data[4 * index + 3] = pixel.A;
                }
            }

            SpriteSheets[imageCount - 1] = sheet;

            return imageCount - 1;
        }

        public ref SpriteSheet GetSpriteSheet(int id)
        {
            return ref SpriteSheets[id];
        }
    }
}
