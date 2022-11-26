using System;
using Enums;
using UnityEngine;

namespace Sprites
{
    [System.Serializable]
    public class SpriteAtlas
    {
        [SerializeField] private AtlasType _atlasType;
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        
        public int AtlasID;
        public int GLTextureID;

        [NonSerialized]
        public byte[] Data;
        public Texture2D Texture;
        public bool TextureNeedsUpdate;
        public RectpackSharp.PackingRectangle[] Rectangles;
        
        public int Width => _width;
        public int Height => _height;

        public SpriteAtlas(AtlasType atlasType, int width, int height, int byteLength)
        {
            _atlasType = atlasType;
            _width = width;
            _height = height;
            this.Data = new byte[byteLength];
            
            for(int i = 0; i < byteLength; i++)
            {
                Data[i] = 255;
            }
        }

        public AtlasType GetAtlasType() => _atlasType;

        public void SetWidth(int value)
        {
            _width = value;
        }
        
        public void SetHeight(int value)
        {
            _height = value;
        }
    }
}
