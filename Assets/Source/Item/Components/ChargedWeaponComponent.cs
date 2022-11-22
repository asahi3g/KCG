using Entitas;

namespace Item.FireWeapon
{
    [ItemParticle, ItemInventory]
    public class ChargedWeaponComponent : IComponent
    {
        public bool CanCharge;
        public float ChargeRate;
        public float ChargeRatio;
        public float ChargePerShot;
        public float ChargeMin;
        public float ChargeMax;
    }
}

