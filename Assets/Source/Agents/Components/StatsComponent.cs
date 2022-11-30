using System;
using Entitas;

namespace Agent
{
    [Agent]
    public class StatsComponent : IComponent
    {
        public ContainerInt Health = new ContainerInt(100, 0, 100);
        public ContainerInt Food = new ContainerInt(100, 0, 100);
        public ContainerInt Water = new ContainerInt(100, 0, 100);
        public ContainerInt Oxygen = new ContainerInt(100, 0, 100);
        public ContainerInt Fuel = new ContainerInt(100, 0, 100);

        public bool IsLimping => Health.GetValue() <= 50.0f;
        
        
        public ContainerInt GetValue(StatsKindInt kind)
        {
            switch (kind)
            {
                case StatsKindInt.Health: return Health;
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }

        public ContainerInt GetValue(StatsKindFloat kind)
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