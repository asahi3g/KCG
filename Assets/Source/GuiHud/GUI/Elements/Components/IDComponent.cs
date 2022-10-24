using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;

namespace KGUI.Elements
{
    [UIElement]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int Index;
    }
}
