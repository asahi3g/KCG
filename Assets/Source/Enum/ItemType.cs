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
        /// Equipment
        /// </summary>
        Helmet,
        Suit,

        /// <summary>
        /// Mech's As Item
        /// </summary>
        MajestyPalm,
        SagoPalm,
        WaterBottle,
        DracaenaTrifasciata,
        Chest,
        Planter,
        Light,
        SmashableBox,

        /// <summary>
        /// Materials
        /// </summary>
        Dirt,

        /// <summary>
        /// Tools
        /// </summary>
        PlacementTool,
        PlacementToolBack,
        RemoveTileTool,
        MiningLaserTool,
        SpawnEnemySlimeTool,
        SpawnEnemyGunnerTool,
        SpawnEnemySwordmanTool,
        PipePlacementTool,
        ParticleEmitterPlacementTool,
        ChestPlacementTool,
        HarvestTool,
        ConstructionTool,
        ScannerTool,
        PlanterTool,
        RemoveMech
    }
}
