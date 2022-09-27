using Entitas;
using KMath;

namespace Agent
{
    [Agent]
    public class ControllerComponent : IComponent
    {
        public AI.AgentController Controller;
    }
}
