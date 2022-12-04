//imports UnityEngine

using Enums;
using KMath;
using System.Collections.Generic;
using System;

namespace Node
{
    public class PlantAction : NodeBase
    {
        public override ActionType  Type => ActionType .PlantAction;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("PlanterID", typeof(int)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            MechEntity planter = null;
            Vec2f planterPosition = Vec2f.Zero;
            if (agentEntity.isAgentPlayer)
            {
                var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();
                float x = worldPosition.X;
                float y = worldPosition.Y;

                for (int i = 0; i < planet.MechList.Length; i++)
                {
                    if (planet.MechList.Get(i).mechType.mechType != MechType.Planter)
                        continue;

                    if (planet.MechList.Get(i).mechPlanter.GotPlant)
                        continue;

                    // Is mouse over it?
                    planterPosition = planet.MechList.Get(i).mechPosition2D.Value;
                    Vec2f size = planet.MechList.Get(i).mechSprite2D.Size;
                    if (x < planterPosition.X || y < planterPosition.Y)
                        continue;

                    if (x > planterPosition.X + size.X || y > planterPosition.Y + size.Y)
                        continue;

                    planter = planet.MechList.Get(i);
                    break;
                }
            }
            else
            {
                if (nodeEntity.hasNodeBlackboardData)
                    planter = planet.EntitasContext.mech.GetEntityWithMechID(nodeEntity.nodeBlackboardData.entriesIDs[0]);
            }

            if (planter == null)
            {
                nodeEntity.nodeExecution.State = NodeState.Fail;
                return;
            }

            MechEntity plant;
            planterPosition.Y += 0.85f;
            switch (itemEntity.itemType.Type)
            {
                case ItemType.MajestyPalm:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.MajestyPalm);
                    break;
                case ItemType.SagoPalm:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.SagoPalm);
                    break;
                case ItemType.DracaenaTrifasciata:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.DracaenaTrifasciata);
                    break;
                default:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.DracaenaTrifasciata);
                    break;
            }

            planter.mechPlanter.GotPlant = true;
            planter.mechPlanter.PlantMechID = plant.mechID.ID;
            GameState.InventoryManager.RemoveItem(agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
