using Collisions;
using KMath;
using Planet;
using System.Collections.Generic;

namespace AI.Sensor
{
    public class SensorBase
    {
        public int VariableID;
        public virtual void RegisterInBlackBoard(BlackBoard blackboard) { }
        public virtual void Update(BlackBoard blackboard, ref PlanetState planet) { }
    }
}