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
        public float Food;

        [Range(0, 100)]
        public float Water;

        [Range(0, 100)]
        public float Oxygen;

        [Range(-100, 100)]
        public float Fuel;

        public float AttackCooldown;
        public bool IsLimping; // damaged and his movements are slowed
    }
}