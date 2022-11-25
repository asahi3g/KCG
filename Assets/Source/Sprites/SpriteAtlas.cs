using Enums;
using UnityEngine;

namespace Sprites
{
    [System.Serializable]
    public class SpriteAtlas
    {
        private AtlasType _atlasType;
        public int AtlasID;
        public int GLTextureID;

        public int Width;
        public int Height;

        public byte[] Data;
        public Texture2D Texture;
        public bool TextureNeedsUpdate;
        public RectpackSharp.PackingRectangle[] Rectangles;


        public SpriteAtlas()
        {
            
        }

        public SpriteAtlas(AtlasType atlasType, int width, int height, int byteLength)
        {
            _atlasType = atlasType;
            this.Width = width;
            this.Height = height;
            this.Data = new byte[byteLength];
            
            for(int i = 0; i < byteLength; i++)
            {
                Data[i] = 255;
            }
        }

        public AtlasType GetAtlasType() => _atlasType;
    }
}
