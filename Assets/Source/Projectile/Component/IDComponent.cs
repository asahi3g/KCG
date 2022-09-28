using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Projectile
{
    [Projectile]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of ProjectileList. It should never reuse values.
        public int ID;
        public int Index;
        public int AgentOwnerID;
    }
}