using Entitas;
using Entitas.CodeGeneration.Attributes;
using Enums.Tile;

namespace Mech
{
    [Mech]
    public class CastDataComponent : IComponent
    {
        public Data data;
        public bool InputsActive;
    }
}

