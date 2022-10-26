//imports UnityEngine

using Enums;
using KMath;
using System.Collections.Generic;
using System;

namespace Node
{
    public class PlantAction : NodeBase
    {
        public override NodeType Type => NodeType.PlantAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;

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
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            MechEntity planter = null;
            Vec2f planterPosition = Vec2f.Zero;
            if (agentEntity.isAgentPlayer)
            {
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                float x = worldPosition.x;
                float y = worldPosition.y;

                for (int i = 0; i < GameState.Planet.MechList.Length; i++)
                {
                    if (GameState.Planet.MechList.Get(i).mechType.mechType != MechType.Planter)
                        continue;

                    if (GameState.Planet.MechList.Get(i).mechPlanter.GotPlant)
                        continue;

                    // Is mouse over it?
                    planterPosition = GameState.Planet.MechList.Get(i).mechPosition2D.Value;
                    Vec2f size = GameState.Planet.MechList.Get(i).mechSprite2D.Size;
                    if (x < planterPosition.X || y < planterPosition.Y)
                        continue;

                    if (x > planterPosition.X + size.X || y > planterPosition.Y + size.Y)
                        continue;

                    planter = GameState.Planet.MechList.Get(i);
                    break;
                }
            }
            else
            {
                if (nodeEntity.hasNodeBlackboardData)
                    planter = GameState.Planet.EntitasContext.mech.GetEntityWithMechID(nodeEntity.nodeBlackboardData.entriesIDs[0]);
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
                    plant = GameState.Planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.MajestyPalm);
                    break;
                case ItemType.SagoPalm:
                    plant = GameState.Planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.SagoPalm);
                    break;
                case ItemType.DracaenaTrifasciata:
                    plant = GameState.Planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.DracaenaTrifasciata);
                    break;
                default:
                    plant = GameState.Planet.AddMech(new Vec2f(planterPosition.X, planterPosition.Y), MechType.DracaenaTrifasciata);
                    break;
            }

            planter.mechPlanter.GotPlant = true;
            planter.mechPlanter.PlantMechID = plant.mechID.ID;
            GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
