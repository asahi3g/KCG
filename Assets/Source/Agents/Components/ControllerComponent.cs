using Entitas;

namespace Agent
{
    [Agent]
    public class ControllerComponent : IComponent
    {
        public AI.AgentController Controller;
    }
}
