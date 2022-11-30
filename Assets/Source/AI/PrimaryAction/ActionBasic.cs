﻿using static GameState;

namespace Action
{
    // Register Actions that should be used by several behavior trees.
    static public class ActionBasic
    {
        public static void RegisterBasicActions()
        {
            ActionManager.RegisterAction("Wait", WaitAction.Action);
            ActionManager.RegisterAction("SelectClosestTarget", SelectClosestTarget.Action);
            ActionManager.RegisterAction("AimAt", AimAt.Action);
            ActionManager.RegisterActionSequence("MeleeAtack",
                onEnter:  BasicMeleeAtackAction.OnEnter,
                onUpdate: BasicMeleeAtackAction.OnUpdate);
            ActionManager.RegisterAction("MoveDirectlyToward", MoveDirectlyToward.Action);
            ActionManager.RegisterActionSequence("ReloadWeapon", 
                onEnter: ReloadAction.OnEnter, 
                onUpdate: ReloadAction.OnUpdate,
                onSuccess: ReloadAction.OnSuccess,
                onFailure:ReloadAction.OnFailure);
            ActionManager.RegisterAction("FireWeapon", ShootFireWeaponAction.Action);
        }
    }
}
