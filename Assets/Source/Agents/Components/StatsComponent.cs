using System;
using Entitas;

namespace Agent
{
    [Agent]
    public class StatsComponent : IComponent
    {

        public int Health = 0;
        public int Food = 0;
        public int Water = 0;
        public int Oxygen = 0;
        public int Fuel = 0;

        public bool IsLimping => Health <= 50.0f;
        
        
        public ContainerFloat GetValue(StatsKindInt kind)
        {
            switch (kind)
            {
                case StatsKindInt.Health: return new ContainerFloat(Health, 0, 100);
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }

        public ContainerFloat GetValue(StatsKindFloat kind)
        {
            switch (kind)
            {
                case StatsKindFloat.Food: return new ContainerFloat(Food, 0, 100);
                case StatsKindFloat.Water: return new ContainerFloat(Water, 0, 100);
                case StatsKindFloat.Oxygen: return new ContainerFloat(Oxygen, 0, 100);
                case StatsKindFloat.Fuel: return new ContainerFloat(Fuel, 0, 100);
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }

        public int GetValueInt32(StatsKindInt kind)
        {
            switch (kind)
            {
                case StatsKindInt.Health: return Health;
            }
            throw new NotImplementedException($"{nameof(StatsComponent)} by {kind} not implemented");
        }

        public int GetValueInt32(StatsKindFloat kind)
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