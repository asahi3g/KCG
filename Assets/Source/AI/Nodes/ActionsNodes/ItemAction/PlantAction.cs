using UnityEngine;
using Enums;
using KMath;
using System.Web.WebPages;
using Entitas;
using System.Collections.Generic;
using System;

namespace Node
{
    public class PlantAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.PlantAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }
        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("PlanterID", typeof(int)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            MechEntity planter = null;
            Vec2f planterPosition = Vec2f.Zero;
            if (agentEntity.isAgentPlayer)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float x = worldPosition.x;
                float y = worldPosition.y;

                for (int i = 0; i < planet.MechList.Length; i++)
                {
                    if (planet.MechList.Get(i).mechType.mechType != Enums.MechType.Planter)
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
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            MechEntity plant;
            planterPosition.Y += 0.85f;
            switch (itemEntity.itemType.Type)
            {
                case Enums.ItemType.MajestyPalm:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Enums.MechType.MajestyPalm);
                    break;
                case Enums.ItemType.SagoPalm:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Enums.MechType.SagoPalm);
                    break;
                case Enums.ItemType.DracaenaTrifasciata:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Enums.MechType.DracaenaTrifasciata);
                    break;
                default:
                    plant = planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), Enums.MechType.DracaenaTrifasciata);
                    break;
            }

            planter.mechPlanter.GotPlant = true;
            planter.mechPlanter.PlantMechID = plant.mechID.ID;
            GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
