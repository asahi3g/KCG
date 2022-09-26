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
        MoveToAction,
        DrinkPotionAction,

        /// Mech Actions
        OpenChestAction,

        // Items
        ShootFireWeaponAction,
        ShootPulseWeaponAction,
        ThrowGasBombAction,
        ThrowFragGrenadeAction,
        WaterAction,
        UseShieldAction,
        MeleeAttackAction,
        PlantAction,
        HarvestAction,
        MechPlacementAction,
        MaterialPlacementAction,


        /// Ttools actions
        ToolActionPlaceParticleEmitter,
        ToolActionPlaceTile,
        ToolActionSpawnExplosion,
        ToolActionPlaceChest,
        ToolActionPlaceSmahableBox,
        ToolActionEnemySpawn,
        ToolActionEnemyGunnerSpawn,
        ToolActionEnemySwordmanSpawn,
        ToolActionMiningLaser,
        ToolActionRemoveTile,
        ToolActionConstruction,
        ToolActionScanner,
        ToolActionRemoveMech,
        ToolActionPotion,
        ToolActionGeometryPlacement,
    }
}
