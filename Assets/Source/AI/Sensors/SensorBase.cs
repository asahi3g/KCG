using Collisions;
using KMath;
using Planet;
using System.Collections.Generic;

namespace AI.Sensor
{
    public class SensorBase
    {
        public virtual void RegisterStates() { }
        public virtual void Update(BlackBoard blackboard, ref PlanetState planet) { }
    }
}