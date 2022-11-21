using Entitas;
using UnityEngine;

namespace Item.PulseWeapon
{
    // Pulse Weapon Mode Component
    // Pulse is a weapon, can change mode between bullet and grenade

    [ItemParticle, ItemInventory]
    public class PulseComponent : IComponent
    {
        public bool GrenadeMode;
        [Range(0, 12)]
        public int NumberOfGrenades;
    }
}

