using System;
using UnityEngine;

namespace Sprites
{
    [System.Serializable]
    public struct SpriteSheet
    {
        // File path for the given texture, for debugging purposes.
        [TextArea(3,6)]
        public string filePath;

        [NonSerialized]
        public byte[] Data; // RGBA8 array

        public int Index; // Index of the sprite Sheet
        
        public int SpriteWidth; // a single sprite width in pixels
        public int SpriteHeight; // a single sprite height in pixels

        public int Width; // sprite sheet width in pixels
        public int Height; // sprite sheet height in pixels
    }
}
