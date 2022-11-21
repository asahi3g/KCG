using System;
using Entitas;

namespace Agent
{
    [Agent]
    public class StatsComponent : IComponent
    {

        public readonly ContainerInt Health = new ContainerInt(100, 0, 100);
        public readonly ContainerFloat Food = new ContainerFloat(100f, 0f, 100f);
        public readonly ContainerFloat Water = new ContainerFloat(100f, 0f, 100f);
        public readonly ContainerFloat Oxygen = new ContainerFloat(100f, 0f, 100f);
        public readonly ContainerFloat Fuel = new ContainerFloat(100f, 0f, 100f);

        public bool IsLimping => Health.GetValue() <= 50.0f;
        
        
        public ContainerInt GetValue(StatsKindInt kind)
        {
            switch (kind)
            {
                case StatsKindInt.Health: return Health;
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }

        public ContainerFloat GetValue(StatsKindFloat kind)
        {
            switch (kind)
            {
                case StatsKindFloat.Food: return Food;
                case StatsKindFloat.Water: return Water;
                case StatsKindFloat.Oxygen: return Oxygen;
                case StatsKindFloat.Fuel: return Fuel;
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }
    }
}