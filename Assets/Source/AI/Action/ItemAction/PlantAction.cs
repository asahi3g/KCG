using UnityEngine;
using Enums;
using KMath;
using NodeSystem.BehaviorTree;
using Planet;
using Unity.Collections.LowLevel.Unsafe;
using AI;
namespace Action
{
    public class PlantAction
    {
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.CurrentPlanet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem(ref planet);

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
                if (data.HasBlackboard())
                {
                    ref Blackboard blackboard = ref GameState.BlackboardManager.Get(data.BlackboardID);
                    planter = planet.EntitasContext.mech.GetEntityWithMechID(blackboard.MechID);
                }
            }

            if (planter == null)
            {
                return NodeState.Fail;
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
            return NodeState.Success;
        }
    }
}
