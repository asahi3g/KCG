using Entitas;

namespace Agent
{
    [Agent]
    public class ControllerComponent : IComponent
    {
        public int BehaviorTreeId;
        public int BlackboardID;
    }
}
