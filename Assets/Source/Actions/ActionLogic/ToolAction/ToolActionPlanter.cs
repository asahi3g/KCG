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
        // Mech Property
        private Mech.MechProperties MechProperty;

        // Item Entity
        private ItemInventoryEntity ItemEntity;

        // Light Entities List
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
                                    {
                                        entity.mechPlanterPlanter.Plant = planet.AddMech(new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y + 0.85f), Mech.MechType.Plant);


                                        // Mech Property
                                        entity.mechPlanterPlanter.GotSeed = true;
                                    }

                                    if (entity.mechPlanterPlanter.WaterLevel < entity.mechPlanterPlanter.MaxWaterLevel)
                                        entity.mechPlanterPlanter.WaterLevel += 10.0f;
                                    else
                                        entity.mechPlanterPlanter.WaterLevel = 99;
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
                        // Set Light Level Set Zero
                        entity.mechPlanterPlanter.LightLevel = 0;

                        // Check Plant Null or Not, Update Plant Position Relavtive to The Pot
                        if(entity.mechPlanterPlanter.Plant != null)
                            entity.mechPlanterPlanter.Plant.mechPosition2D.Value = new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y + 0.85f);

                        // Iterate All Light Mechs 
                        for (int i = 0; i < lights.Count; i++)
                        {
                            // Get All Lights near the Pot
                            if (Vector2.Distance(new Vector2(lights[i].mechPosition2D.Value.X, lights[i].mechPosition2D.Value.Y),
                                new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 10.0f)
                            {
                                // Increase Ligth Level
                                entity.mechPlanterPlanter.LightLevel++;
                            }
                        }

                        // Has Planter Component?
                        if (entity.hasMechPlanterPlanter)
                        {
                            // Check Water Level and Light Level
                            if (entity.mechPlanterPlanter.WaterLevel > 0 && entity.mechPlanterPlanter.LightLevel > 0)
                            {
                                // Increase Water Level
                                if (entity.mechPlanterPlanter.WaterLevel < entity.mechPlanterPlanter.MaxWaterLevel)
                                    entity.mechPlanterPlanter.WaterLevel -= Time.deltaTime * 0.4f;

                                // Increase Plant Growth Related to The Water Level
                                if (entity.mechPlanterPlanter.PlantGrowth < entity.mechPlanterPlanter.GrowthTarget)
                                    entity.mechPlanterPlanter.PlantGrowth += Time.deltaTime * 0.3f;

                                // Check Plant
                                if (entity.mechPlanterPlanter.Plant != null)
                                {
                                    // Increase Y Scale
                                    entity.mechPlanterPlanter.Plant.mechSprite2D.Size.Y += 0.000001f;
                                }

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
