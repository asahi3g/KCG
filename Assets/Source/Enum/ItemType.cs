namespace Enums
{
    public enum ItemType
    {
        Error = -1,     // Not inialized type.
        Rock,
        RockDust,
        Ore,
        Slime,
        Food,
        Bone,

        /// <summary>
        /// Weapons
        /// </summary>
        SniperRifle,
        LongRifle,
        PulseWeapon,
        AutoCannon,
        SMG,
        Shotgun,
        PumpShotgun,
        Pistol,
        GrenadeLauncher,
        RPG,
        Bow,
        Sword,
        StunBaton,
        RiotShield,

        /// <summary>
        /// Mech's As Item
        /// </summary>
        MajestyPalm,
        SagoPalm,
        WaterBottle,
        DracaenaTrifasciata,

        /// <summary>
        /// Tools
        /// </summary>
        PlacementTool,
        PlacementToolBack,
        RemoveTileTool,
        MiningLaserTool,
        SpawnEnemySlimeTool,
        PipePlacementTool,
        ParticleEmitterPlacementTool,
        ChestPlacementTool,
        HarvestTool,
        ConstructionTool,
        ScannerTool,
        RemoveMech,
    }
}
