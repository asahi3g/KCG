using Entitas;
using UnityEngine;
using KMath;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace KGUI.Elements
{
    [UIElement]
    public class ImageComponent : IComponent
    {
        public string Name;
        public Sprite Sprite;
        public ImageWrapper ImageWrapper;
        public Vec3f Scale;
        public int Width;
        public int Height;
        public string Path;
        public int tileSpriteID;
    }
}
