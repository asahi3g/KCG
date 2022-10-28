using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Mech
{
    [Mech]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of MechList. It should never reuse values or change after created.
        // ID should be used when linking diferent entities.
        public int ID;
        public int Index;
    }
}
