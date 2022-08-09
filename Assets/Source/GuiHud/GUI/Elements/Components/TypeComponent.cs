using Entitas;
using UnityEngine;
using KMath;
using Entitas.CodeGeneration.Attributes;
using Enums;

namespace KGUI.Elements
{
    [UIElement]
    public class TypeComponent : IComponent
    {
        public ElementType elementType;
    }
}
