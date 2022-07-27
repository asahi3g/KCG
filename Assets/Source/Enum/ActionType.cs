﻿namespace Enums
{
    // Todo: Procedural generate enums and Intialize functions.
    public enum ActionType
    {
        /// <summary>
        /// General Actions
        /// </summary>
        DropAction,
        PickUpAction,
        ReloadAction,
        ChargeAction,
        ShieldAction,
        MoveAction,

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
        ToolActionEnemySpawn,
        ToolActionMiningLaser,
        ToolActionRemoveTile,
        ToolActionThrowGrenade,
        ToolActionMeleeAttack,
        ToolActionPulseWeapon,
        ToolActionShield,
        ToolActionPlanter,
    }
}
