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

        // Constructor
        public ToolActionPlanter(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            // Item Entity
            ItemEntity = EntitasContext.itemInventory.GetEntityWithItemID(ActionEntity.actionTool.ItemID);

            // Get Cursor Position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;


            // Get Mech Entities
            var entities = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach(var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if(Vector2.Distance(new Vector2(x,y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.0f)
                {
                    // Has Mech Type Component?
                    if(entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if(entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            // Has Planter Component?
                            if(entity.hasMechPlanterPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanterPlanter.PlantGrowth < 100.0f)
                                {
                                    // Is seed placed?
                                    if(!entity.mechPlanterPlanter.GotSeed)
                                        entity.mechPlanterPlanter.GotSeed = true;

                                    // Increase Growth
                                    if (entity.mechPlanterPlanter.PlantGrowth < entity.mechPlanterPlanter.GrowthTarget)
                                        entity.mechPlanterPlanter.PlantGrowth += 25.0f;

                                    // Increase Water Level
                                    if (entity.mechPlanterPlanter.WaterLevel < entity.mechPlanterPlanter.MaxWaterLevel)
                                        entity.mechPlanterPlanter.WaterLevel += 25.0f;
                                
                                    // Break the loop
                                    break;
                                }

                            }
                        }
                    }
                }
            }

            // Return True
            ActionEntity.ReplaceActionExecution(this, Enums.ActionState.Success);
        }
    }

    // Factory Method
    public class ToolActionPlanterCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionPlanter(entitasContext, actionID);
        }
    }
}
