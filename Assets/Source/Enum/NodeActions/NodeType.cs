namespace Enums
{
    // Todo: Procedural generate enums and Intialize functions.
    public enum NodeType
    {
        None,

        // Behaviour tree Nodes
        DecoratorNode,
        SequenceNode,

        /// General Actions
        DropAction,
        PickUpAction,
        ReloadAction,
        ChargeAction,
        ShieldAction,
        MoveAction,
        DrinkPotion,

        /// Mech Actions
        OpenChestAction,

        // Items
        ShootFireWeaponAction,
        ShootPulseWeaponAction,
        ThrowGasBombAction,
        ThrowFragGrenadeAction,

        /// PlaceTileTool
        /// One for each type of tile
        PlaceTilOre1Action,
        PlaceTilOre2Action,
        PlaceTilOre3Action,
        PlaceTilGlassAction,
        PlaceTilMoonAction,
        PlaceTilPipeAction,
        PlaceTilBackgroundAction,

        /// Others tools actions
        ToolActionPlaceParticle,
        ToolActionSpawnExplosion,
        ToolActionPlaceChest,
        ToolActionPlaceSmahableBox,
        ToolActionEnemySpawn,
        ToolActionEnemyGunnerSpawn,
        ToolActionEnemySwordmanSpawn,
        ToolActionMiningLaser,
        ToolActionRemoveTile,
        ToolActionThrowGrenade,
        ToolActionMeleeAttack,
        ToolActionShield,
        ToolActionPlanter,
        ToolActionWater,
        ToolActionHarvest,
        ToolActionConstruction,
        ToolActionScanner,
        ToolActionRemoveMech,
        ToolActionMechPlacement,
        ToolActionMaterialPlacement,
        ToolActionPotion,
        ToolActionGeometryPlacement,
    }
}
