﻿namespace Enums
{
    // Todo: Procedural generate enums and Intialize functions.
    public enum NodeType
    {
        None = -1,

        // Behaviour tree Nodes
        DecoratorNode,
        RepeatedNode,
        SequenceNode,
        SelectorNode,

        // General Actions
        SelectClosestTarget,
        WaitAction,
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
        ShootGrenadeAction,
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