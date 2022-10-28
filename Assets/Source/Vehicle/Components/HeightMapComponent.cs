using Entitas;
using KMath;

namespace Vehicle
{
    [Vehicle]
    public class HeightMapComponent : IComponent
    {
        public bool OpenSky;
        public Vec2f SpawnPosition;
    }
}
