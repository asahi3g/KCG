using Entitas;
using KMath;
using System.Collections.Generic;

namespace Vehicle
{
    [Vehicle]
    public class HeightMapComponent : IComponent
    {
        public bool OpenSky;
        public Vec2f SpawnPosition;
    }
}
