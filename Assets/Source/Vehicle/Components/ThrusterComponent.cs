using Entitas;
using Enums;

namespace Vehicle
{
    [Vehicle]
    public class ThrusterComponent : IComponent
    {
        public bool Jet;
        public float Angle;
        public JetSize JetSize;
        public bool isLaunched;
    }
}
