using Entitas;
using UnityEngine;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionPlanter : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        public ToolActionPlanter(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            var entities = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach(var entity in entities)
            {
                if(Vector2.Distance(new Vector2(x,y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 0.5f)
                {
                    if(entity.hasMechType)
                    {
                        if(entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            if(entity.hasMechPlanterPlanter)
                            {
                                entity.mechPlanterPlanter.GotSeed = true;
                                ItemEntity.Destroy();
                                break;
                            }
                        }
                    }
                }
            }

            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionPlanterCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionPlanter(entitasContext, actionID);
        }
    }
}
