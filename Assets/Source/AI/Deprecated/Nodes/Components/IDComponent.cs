using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Node
{
    [Node]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int                  ID;
        [EntityIndex]
        public Enums.ActionType      TypeID;
    }
}
