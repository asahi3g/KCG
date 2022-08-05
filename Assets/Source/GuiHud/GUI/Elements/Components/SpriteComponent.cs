using Entitas;
using UnityEngine;
using KMath;
using Entitas.CodeGeneration.Attributes;

namespace KGUI.Elements
{
    [UIElement]
    public class SpriteComponent : IComponent
    {
        public GameObject GameObject; // used for unity rendering
    }
}
