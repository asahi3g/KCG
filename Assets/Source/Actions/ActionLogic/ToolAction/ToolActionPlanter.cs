using Entitas;
using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionPlanter : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        private List<MechEntity> lights = new List<MechEntity>();

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
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Light)
                        {
                            lights.Add(entity);
                        }

                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            // Has Planter Component?
                            if (entity.hasMechPlanterPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanterPlanter.PlantGrowth < 100.0f)
                                {
                                    // Is seed placed?
                                    if (!entity.mechPlanterPlanter.GotSeed)
                                        entity.mechPlanterPlanter.GotSeed = true;

                                    entity.mechPlanterPlanter.WaterLevel += 10.0f;
                                }

                            }
                        }
                    }
                }
            }

            // Return True
            ActionEntity.actionExecution.State = Enums.ActionState.Running;
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {

            // Get Mech Entities
            var entities = EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                // Has Mech Type Component?
                if (entity.hasMechType)
                {
                    // Is Mech Planter?
                    if (entity.mechType.mechType == Mech.MechType.Planter)
                    {
                        entity.mechPlanterPlanter.LightLevel = 0;
                        for (int i = 0; i < lights.Count; i++)
                        {
                            if (Vector2.Distance(new Vector2(lights[i].mechPosition2D.Value.X, lights[i].mechPosition2D.Value.Y),
                                new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 10.0f)
                            {
                                entity.mechPlanterPlanter.LightLevel++;
                            }
                        }

                        // Has Planter Component?
                        if (entity.hasMechPlanterPlanter)
                        {
                            if (entity.mechPlanterPlanter.WaterLevel > 0 && entity.mechPlanterPlanter.LightLevel > 0)
                            {
                                // Increase Water Level
                                if (entity.mechPlanterPlanter.WaterLevel < entity.mechPlanterPlanter.MaxWaterLevel)
                                    entity.mechPlanterPlanter.WaterLevel -= Time.deltaTime * 0.4f;

                                if (entity.mechPlanterPlanter.PlantGrowth < entity.mechPlanterPlanter.GrowthTarget)
                                    entity.mechPlanterPlanter.PlantGrowth += Time.deltaTime * 0.3f;

                                if(entity.mechPlanterPlanter.WaterLevel <= 0)
                                {
                                    // Return True
                                    ActionEntity.actionExecution.State = Enums.ActionState.Success;
                                }
                            }
                            else
                            {
                                // Return True
                                ActionEntity.actionExecution.State = Enums.ActionState.Success;
                            }

                        }
                    }
                }
            }
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
