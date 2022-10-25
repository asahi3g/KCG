using static GameState;

namespace Action
{
    static public class ActionBasic
    {
        public static void RegisterBasicActions()
        {
            ActionManager.RegisterAction("Wait", WaitAction.Action);
            ActionManager.RegisterAction("SelectClosestTarget", SelectClosestTarget.Action);
            ActionManager.RegisterActionSequence("ReloadWeapon", 
                onEnter: ReloadAction.OnEnter, 
                onUpdate: ReloadAction.OnUpdate,
                onSuccess: ReloadAction.OnSuccess,
                onFailure:ReloadAction.OnFailure);
            ActionManager.RegisterActionSequence("FireWeapon",
                onEnter: ShootFireWeaponAction.OnEnter,
                onUpdate: ShootFireWeaponAction.OnUpdate);
        }
    }
}
