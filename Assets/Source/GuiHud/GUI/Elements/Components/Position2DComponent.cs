using Entitas;
using UnityEngine;
using KMath;
using Entitas.CodeGeneration.Attributes;

namespace KGUI.Elements
{
    [UIElement]
    public class Position2DComponent : IComponent
    {
        public Vec2f Value;
        public Vec2f PreviousValue;
    }
}
