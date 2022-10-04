using UnityEngine;
using System.Collections.Generic;
using KMath;
using Planet;
using Enums;

namespace Node
{
    public class WaterAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.WaterAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }


        // Todo urgent: Action in infinite loop. Fix it.
        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            var entities = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            List<MechEntity> lights = new List<MechEntity>();

            foreach (var entity in entities)
            {
                if (entity.mechType.mechType == Mech.MechType.Light)
                {
                    lights.Add(entity);
                }

                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            if (entity.hasMechPlanter)
                            {
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

        }

        // Todo Urgent: move this out of to a system outside actions.
        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            List<MechEntity> lights = new List<MechEntity>();
            var entities = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                if (entity.mechType.mechType == Mech.MechType.Light)
                    lights.Add(entity);
            }

            foreach (var entity in entities)
            {
                if (entity.hasMechType)
                {
                    if (entity.mechType.mechType == Mech.MechType.Planter)
                    {
                        if (entity.mechPlanter.PlantGrowth < 100)
                        {
                            // Set Light Level Set Zero
                            entity.mechPlanter.LightLevel = 0;

                            // Check Plant Null or Not, Update Plant Position Relavtive to The Pot
                            //if(entity.mechPlanter.Plant != null)
                            //  entity.mechPlanter.Plant.mechPosition2D.Value = new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y + 0.85f);

                            for (int i = 0; i < lights.Count; i++)
                            {
                                if (Vec2f.Distance(new Vec2f(lights[i].mechPosition2D.Value.X, lights[i].mechPosition2D.Value.Y),
                                    new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 10.0f)
                                {
                                    entity.mechPlanter.LightLevel++;
                                }
                            }

                            if (entity.hasMechPlanter)
                            {
                                if (entity.mechPlanter.WaterLevel > 0 && entity.mechPlanter.LightLevel > 0)
                                {
                                    if (entity.mechPlanter.WaterLevel < entity.mechPlanter.MaxWaterLevel)
                                        entity.mechPlanter.WaterLevel -= Time.deltaTime * 0.4f;

                                    if (entity.mechPlanter.PlantGrowth < entity.mechPlanter.GrowthTarget)
                                        entity.mechPlanter.PlantGrowth += Time.deltaTime * 0.3f;

                                    MechEntity plant = planet.EntitasContext.mech.GetEntityWithMechID(entity.mechPlanter.PlantMechID);

                                    if (entity.mechPlanter.PlantGrowth >= 50 && entity.mechPlanter.PlantGrowth < 100)
                                    {
                                        if (plant.mechType.mechType == Mech.MechType.MajestyPalm)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.MajestyPalm;
                                        else if (plant.mechType.mechType == Mech.MechType.SagoPalm)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.SagoPalm;
                                        else if (plant.mechType.mechType == Mech.MechType.DracaenaTrifasciata)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.DracaenaTrifasciataS1;
                                    }

                                    else if (entity.mechPlanter.PlantGrowth >= 100)
                                    {
                                        if (plant.mechType.mechType == Mech.MechType.MajestyPalm)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.MajestyPalmS2;
                                        else if (plant.mechType.mechType == Mech.MechType.SagoPalm)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.SagoPalmS2;
                                        else if (plant.mechType.mechType == Mech.MechType.DracaenaTrifasciata)
                                            plant.mechSprite2D.SpriteId = GameState.MechCreationApi.DracaenaTrifasciataS2;
                                    }

                                    if (entity.mechPlanter.WaterLevel <= 0)
                                    {
                                        // Return True
                                        //nodeEntity.nodeExecution.State = Enums.ActionState.Success;
                                    }

                                    if (entity.mechPlanter.PlantGrowth >= 100)
                                    {
                                        //nodeEntity.nodeExecution.State = Enums.ActionState.Success;
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
