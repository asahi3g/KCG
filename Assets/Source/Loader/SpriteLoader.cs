using BigGustave;
using System;
using System.Collections.Generic;
using Sprites;
using UnityEngine;

namespace Loader
{
    public class SpriteLoader
    {
        /**
         * Stores full filePath for debugging purposes.
         * Example: "Assets\StreamingAssets\Tiles\GeometryMetal\metal_tiles_geometry.png"
         */
        private string _filePath; 
        
        public SpriteSheet[] SpriteSheets;
        public Dictionary<string, int> SpriteSheetID;

        public SpriteLoader()
        {
            SpriteSheets = Array.Empty<SpriteSheet>();
            SpriteSheetID = new Dictionary<string, int>();
        }

        public string GetFilePath() => _filePath;
        
        public void InitStage1()
        {
            
        }

        public void InitStage2()
        {
        
        }

        public int GetSpriteSheetID(string filePath, int spriteWidth, int spriteHeight)
        {
            if (SpriteSheetID.ContainsKey(filePath)) return SpriteSheetID[filePath];
            return LoadImageFile(filePath, spriteWidth, spriteHeight);
        }

        private int LoadImageFile(string filePath, int spriteWidth, int spriteHeight)
        {
            _filePath = filePath;
            int imageCount = SpriteSheets.Length + 1;

            Array.Resize(ref SpriteSheets, imageCount);

            SpriteSheetID.Add(filePath, imageCount - 1);

            Png data = Png.Open(filePath);

            int w = data.Header.Width;
            int h = data.Header.Height;

            //spriteWidth = w;
            //spriteHeight = h;

            bool wMatch = w % spriteWidth == 0;
            bool hMatch = h % spriteHeight == 0;

            if (!wMatch || !hMatch)
            {
                Debug.LogWarning($"{nameof(SpriteLoader)} LoadImageFile() filename[{_filePath.Color(Color.cyan)}] spriteWidth[{spriteWidth.ToString().Color(wMatch ? Color.green : Color.red)}] or spriteHeight[{spriteHeight.ToString().Color(hMatch ? Color.green : Color.red)}] does not match PNG w[{w.ToString().Color(wMatch ? Color.green : Color.red)}] h[{h.ToString().Color(hMatch ? Color.green : Color.red)}]");
            }
            
            int sheetIndex = imageCount - 1;
            
            SpriteSheet sheet = new SpriteSheet
            {
                filePath = _filePath,
                Index = sheetIndex,
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

            SpriteSheets[sheetIndex] = sheet;

            return sheetIndex;
        }

        public ref SpriteSheet GetSpriteSheet(int id)
        {
            return ref SpriteSheets[id];
        }
    }
}
