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
        DrinkPotionAction,

        /// Mech Actions
        OpenChestAction,

        // Items
        ShootFireWeaponAction,
        ShootPulseWeaponAction,
        ThrowGasBombAction,
        ThrowFragGrenadeAction,

        /// Ttools actions
        ToolActionPlaceParticle,
        ToolActionPlaceTile,
        ToolActionSpawnExplosion,
        ToolActionPlaceChest,
        ToolActionPlaceSmahableBox,
        ToolActionEnemySpawn,
        ToolActionEnemyGunnerSpawn,
        ToolActionEnemySwordmanSpawn,
        ToolActionMiningLaser,
        ToolActionRemoveTile,
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
