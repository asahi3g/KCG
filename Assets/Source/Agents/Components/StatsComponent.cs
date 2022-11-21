using Entitas;
using UnityEngine;

namespace Agent
{
    [Agent]
    public class StatsComponent : IComponent
    {
        [Range(0, 100)]
        public int Health;

        [Range(0, 100)]
        public int Food;

        [Range(0, 100)]
        public int Water;

        [Range(0, 100)]
        public int Oxygen;

        [Range(-100, 100)]
        public int Fuel;

        public bool IsLimping; // damaged and his movements are slowed
    }
}