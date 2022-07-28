using Entitas;
using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionHarvest : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        // Constructor
        public ToolActionHarvest(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
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
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            if (entity.mechPlanterPlanter.PlantGrowth >= 100)
                            {
                                entity.mechPlanterPlanter.Plant.Destroy();
                                GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.Plant, entity.mechPosition2D.Value);
                                entity.mechPlanterPlanter.GotSeed = false;
                                entity.mechPlanterPlanter.PlantGrowth = 0;
                                entity.mechPlanterPlanter.WaterLevel = 0;
                                entity.mechPlanterPlanter.LightLevel = 0;
                                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                                break;
                            }
                        }
                    }
                }
            }

            // Return True
            ActionEntity.actionExecution.State = Enums.ActionState.Running;
        }
    }

    // Factory Method
    public class ToolActionHarvestCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionHarvest(entitasContext, actionID);
        }
    }
}
