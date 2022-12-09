using NodeSystem;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;
using BehaviorTree;
using Planet;
using KMath;
using AI;

namespace Action
{
    public class WaterAction
    {
        // Action used by either player and AI.
        static public NodeState Action(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
            PlanetState planet = GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);

            MechEntity plant = null;
            Vec2f planterPosition = Vec2f.Zero;
            if (agentEntity.isAgentPlayer)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float x = worldPosition.x;
                float y = worldPosition.y;

                for (int i = 0; i < planet.MechList.Length; i++)
                {
                    MechEntity mech = (planet.MechList.Get(i));

                    if (mech.GetProperties().Group == Enums.MechGroup.Plant)
                        plant = mech;
                    else if (mech.mechType.mechType == Enums.MechType.Planter)
                    {
                        if (mech.mechPlanter.GotPlant)
                            plant = planet.EntitasContext.mech.GetEntityWithMechID(mech.mechPlanter.PlantMechID);
                        else
                            continue;
                    }
                    else
                        continue;

                    // Is mouse over mech?
                    planterPosition = planet.MechList.Get(i).mechPosition2D.Value;
                    Vec2f size = planet.MechList.Get(i).mechSprite2D.Size;
                    if (x < planterPosition.X || y < planterPosition.Y)
                    {
                        plant = null;
                        continue;
                    }
                    if (x > planterPosition.X + size.X || y > planterPosition.Y + size.Y)
                    {
                        plant = null;
                        continue;
                    }
                    break;
                }
            }
            else
            {
                if (agentEntity.agentController.BlackboardID != -1)
                {
                    ref Blackboard blackboard = ref GameState.BlackboardManager.Get(agentEntity.agentController.BlackboardID);
                    plant = planet.EntitasContext.mech.GetEntityWithMechID(blackboard.MechID);
                }
            }

            if (plant != null)
            {
                plant.mechPlant.WaterLevel = Mathf.Min(plant.mechPlant.WaterLevel + 10f, 100);
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
    }
}
