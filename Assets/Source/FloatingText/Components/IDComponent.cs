using Entitas;
using Entitas.CodeGeneration.Attributes;
using KMath;

namespace FloatingText
{

    [FloatingText]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of FloatingTextList. It should never reuse values. It should never be changed.
        public int ID;
        public int Index;
    }
}
