using Entitas;
using UnityEngine;
using KMath;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace KGUI.Elements
{
    [UIElement]
    public class TextComponent : IComponent
    {
        public string Text;
        public float TimeToLive;
        public Text GameObject;
        public Vec2f SizeDelta;
    }
}
