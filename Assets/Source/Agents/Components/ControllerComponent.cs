using Entitas;
using System.Collections.Generic;

namespace Agent
{
    [Agent]
    public class ControllerComponent : IComponent
    {
        public int BehaviorTreeId;
        public int BlackboardID;
        public List<int> SensorsID;
        public int SquadID;
    }
}
