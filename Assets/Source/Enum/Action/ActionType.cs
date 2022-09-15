namespace Enums
{
    // Todo: Procedural generate enums and Intialize functions.
    public enum ActionType
    {
        None,
        /// <summary>
        /// General Actions
        /// </summary>
        DropAction,
        PickUpAction,
        ReloadAction,
        ChargeAction,
        ShieldAction,
        MoveAction,
        DrinkPotion,

        /// <summary>
        /// Mech Actions
        /// </summary>
        OpenChestAction,


        // grenade
        FragGrenade,

        /// <summary>
        /// PlaceTileTool
        /// One for each type of tile
        /// </summary>
        PlaceTilOre1Action,
        PlaceTilOre2Action,
        PlaceTilOre3Action,
        PlaceTilGlassAction,
        PlaceTilMoonAction,
        PlaceTilPipeAction,
        PlaceTilBackgroundAction,

        /// <summary>
        /// Others tools actions
        /// </summary>
        ToolActionFireWeapon,
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
        ToolActionPulseWeapon,
        ToolActionShield,
        ToolActionPlanter,
        ToolActionWater,
        ToolActionHarvest,
        ToolActionConstruction,
        ToolActionScanner,
        ToolActionRemoveMech,
        ToolActionMechPlacement,
        ToolActionMaterialPlacement,
        ToolActionGasBomb,
        ToolActionPotion,
    }
}
