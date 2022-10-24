// Imports UnityEngine

using Entitas;
using KMath;

namespace KGUI.Elements
{
    [UIElement]
    public class ImageComponent : IComponent
    {
        public string Name;
        public UnityEngine.Sprite Sprite;
        public ImageWrapper ImageWrapper;
        public Vec3f Scale;
        public int Width;
        public int Height;
        public string Path;
        public int tileSpriteID;
    }
}
