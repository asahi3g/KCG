using UnityEngine;
using Enums;
using KMath;

namespace Node
{
    public class PlantAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.PlantAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            MechEntity plant = null; // Todo: Figure it out a way to get plant

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;


            Vec2f planterPosition = Vec2f.Zero;
            bool addPlant = false;
            var entities = planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechPosition2D));
            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                        {
                            planterPosition = entity.mechPosition2D.Value;

                            if (entity.hasMechPlanter)
                            {
                                if (entity.mechPlanter.PlantGrowth < 100.0f)
                                {
                                    if (!entity.mechPlanter.GotSeed)
                                    {
                                        addPlant = true;
                                        if(plant != null)
                                        entity.mechPlanter.PlantMechID = plant.mechID.ID;

                                        // Mech Property
                                        entity.mechPlanter.GotSeed = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (addPlant)
            {
                planterPosition.Y += 0.85f;
                if (itemEntity.itemType.Type == Enums.ItemType.MajestyPalm)
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Mech.MechType.MajestyPalm);
                else if(itemEntity.itemType.Type == Enums.ItemType.SagoPalm)
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Mech.MechType.SagoPalm);
                else if (itemEntity.itemType.Type == Enums.ItemType.DracaenaTrifasciata)
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Mech.MechType.DracaenaTrifasciata);
            }

            foreach (var entity in entities)
            {
                // Mesure to Understand Cursor Inside the Mech
                if (Vec2f.Distance(new Vec2f(x, y), new Vec2f(entity.mechPosition2D.Value.X, entity.mechPosition2D.Value.Y)) < 1.5f)
                {
                    if (entity.hasMechType)
                    {
                        if (entity.mechType.mechType == Mech.MechType.Planter)
                            entity.mechPlanter.PlantMechID = plant.mechID.ID;
                    }
                }
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }

        public override void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);

            if (itemEntity != null)
            {
                GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
                itemEntity.Destroy();
            }
            base.OnExit(ref planet, nodeEntity);
        }
    }
}
