namespace Action
{
    public class ChestAction : ActionBase
    {
        public ChestAction(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        // Add here animation etc to be played with this action.
    }

    public class ChestActionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ChestAction(entitasContext, actionID);
        }
    }
}
