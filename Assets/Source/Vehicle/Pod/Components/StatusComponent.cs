using Entitas;

namespace Vehicle.Pod
{
    [Pod]
    public class StatusComponent : IComponent
    {
        public int PodValue;
        public int Score;
        public bool Freeze;
    }
}