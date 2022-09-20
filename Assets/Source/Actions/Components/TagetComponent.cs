using Entitas;
using KMath;

namespace Action
{
    public class TagetComponent : IComponent
    {
        public int AgentTargetID;   // -1 If target is not agent.
        public int MechTargetID;    // -1 If target is not mech.
        public Vec2f TargetPos;
    }
}
