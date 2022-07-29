using Entitas;
using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums.Tile;
using Action;

namespace Action
{
    public class ToolActionWater : ActionBase
    {
        // Item Entity
        private ItemInventoryEntity ItemEntity;

        // Light Entities List
        private List<MechEntity> lights = new List<MechEntity>();

        private bool DrawIndicator = true;

        // Constructor
        public ToolActionWater(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
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
                if (entity.mechType.mechType == Mech.MechType.Light)
                {
                    lights.Add(entity);
                }

                // Mesure to Understand Cursor Inside the Mech
                if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    // Has Mech Type Component?
                    if (entity.hasMechType)
                    {
                        // Is Mech Planter?
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            // Has Planter Component?
                            if (entity.hasMechPlanter)
                            {
                                // Plant Growth Reached Maximum?
                                if (entity.mechPlanter.PlantGrowth < 100.0f)
                                {
                                    if (entity.mechPlanter.WaterLevel < entity.mechPlanter.MaxWaterLevel)
                                        entity.mechPlanter.WaterLevel += 10.0f;
                                    else
                                        entity.mechPlanter.WaterLevel = 99;
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

                        if (entity.mechPlanter.PlantGrowth >= 100)
                        {
                            ActionEntity.actionExecution.State = Enums.ActionState.Success;
                            break;
                        }

                        if (DrawIndicator)
                        {
                            // Get Cursor Position
                            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            float x = worldPosition.x;
                            float y = worldPosition.y;
                            if (Vector2.Distance(new Vector2(x, y), new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                            {
                                if (!entity.mechPlanter.GotSeed)
                                    planet.AddFloatingText("Need Seed", 3.0f, Vec2f.Zero, new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y));

                                if (entity.mechPlanter.WaterLevel <= 0.0f)
                                    planet.AddFloatingText("Need Water", 3.0f, Vec2f.Zero, new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y - 0.4f));

                                if (entity.mechPlanter.LightLevel <= 0.0f)
                                    planet.AddFloatingText("Need Light", 3.0f, Vec2f.Zero, new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y - 0.8f));

                                if (entity.mechPlanter.PlantGrowth >= 100)
                                    planet.AddFloatingText("Ready For Harvest", 3.0f, Vec2f.Zero, new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y - 1.2f));

                                DrawIndicator = false;
                            }
                            else
                            {
                                DrawIndicator = true;
                            }
                        }

                        // Set Light Level Set Zero
                        entity.mechPlanter.LightLevel = 0;

                        // Check Plant Null or Not, Update Plant Position Relavtive to The Pot
                        //if(entity.mechPlanter.Plant != null)
                        //  entity.mechPlanter.Plant.mechPosition2D.Value = new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y + 0.85f);

                        // Iterate All Light Mechs 
                        for (int i = 0; i < lights.Count; i++)
                        {
                            // Get All Lights near the Pot
                            if (Vector2.Distance(new Vector2(lights[i].mechPosition2D.Value.X, lights[i].mechPosition2D.Value.Y),
                                new Vector2(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 10.0f)
                            {
                                // Increase Ligth Level
                                entity.mechPlanter.LightLevel++;
                            }
                        }

                        // Has Planter Component?
                        if (entity.hasMechPlanter)
                        {
                            // Check Water Level and Light Level
                            if (entity.mechPlanter.WaterLevel > 0 && entity.mechPlanter.LightLevel > 0)
                            {
                                // Increase Water Level
                                if (entity.mechPlanter.WaterLevel < entity.mechPlanter.MaxWaterLevel)
                                    entity.mechPlanter.WaterLevel -= Time.deltaTime * 0.4f;

                                // Increase Plant Growth Related to The Water Level
                                if (entity.mechPlanter.PlantGrowth < entity.mechPlanter.GrowthTarget)
                                    entity.mechPlanter.PlantGrowth += Time.deltaTime * 0.3f;

                                // Check Plant Growth
                                if (entity.mechPlanter.PlantGrowth >= 50 && entity.mechPlanter.PlantGrowth < 100)
                                {
                                    // Increase Stage
                                    entity.mechPlanter.Plant.mechSprite2D.SpriteId = GameResources.MajestyPalmS1;
                                }

                                else if (entity.mechPlanter.PlantGrowth >= 100)
                                {
                                    // Increase Stage
                                    entity.mechPlanter.Plant.mechSprite2D.SpriteId = GameResources.MajestyPalmS2;
                                }

                                if (entity.mechPlanter.WaterLevel <= 0)
                                {
                                    // Return True
                                    ActionEntity.actionExecution.State = Enums.ActionState.Success;
                                }

                                if (entity.mechPlanter.PlantGrowth >= 100)
                                {
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
    public class ToolActionWaterCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionWater(entitasContext, actionID);
        }
    }
}
