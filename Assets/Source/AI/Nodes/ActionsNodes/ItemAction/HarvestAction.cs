using UnityEngine;
using KMath;
using Enums;
using Entitas;

namespace Node.Action
{
    public class HarvestAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.HarvestAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            MechEntity plant = null;
            MechEntity planter = null;
            if (agentEntity.isAgentPlayer)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float x = worldPosition.x;
                float y = worldPosition.y;

                for (int i = 0; i < planet.MechList.Length; i++)
                {
                    MechEntity mech = (planet.MechList.Get(i));

                    if (mech.mechType.mechType == Enums.MechType.Planter)
                        planter = mech;

                    if (!planter.mechPlanter.GotPlant)
                    {
                        planter = null;
                        continue;
                    }
                    plant = planet.EntitasContext.mech.GetEntityWithMechID(mech.mechPlanter.PlantMechID);
                    // Is mouse over mech?
                    Vec2f pos = planter.mechPosition2D.Value;
                    Vec2f size = planter.mechSprite2D.Size;
                    bool isOver = true;
                    if (x < pos.X || y < pos.Y)
                        isOver = false;
                    if (x > pos.X + size.X || y > pos.Y + size.Y)
                        isOver = false;
                    if (!isOver)
                    {
                        pos = plant.mechPosition2D.Value;
                        size = plant.mechSprite2D.Size;
                        if (x < pos.X || y < pos.Y)
                        {
                            plant = null;
                            planter = null;
                            continue;
                        }
                        if (x > pos.X + size.X || y > pos.Y + size.Y)
                        {
                            plant = null;
                            planter = null;
                            continue;
                        }
                    }
                    break;
                }
            }
            else
            {
                Debug.LogError("AI can't use this action. Add blackboard entry with mech target");
                nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
                return;
            }

            if (planter != null)
            {
                if (plant.mechPlant.PlantGrowth >= 100)
                {
                    Vec2f spawnPos = new Vec2f(plant.mechPosition2D.Value.X, plant.mechPosition2D.Value.Y + plant.mechSprite2D.Size.Y / 2.0f);
                    if (plant.mechType.mechType == Enums.MechType.MajestyPalm)
                        GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.MajestyPalm, spawnPos);
                    else if (plant.mechType.mechType == Enums.MechType.SagoPalm)
                        GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.SagoPalm, spawnPos);
                    else if (plant.mechType.mechType == Enums.MechType.DracaenaTrifasciata)
                        GameState.ItemSpawnSystem.SpawnItemParticle(planet.EntitasContext, Enums.ItemType.DracaenaTrifasciata, spawnPos);
                }
                planet.RemoveMech(plant.mechID.Index);
                planter.mechPlanter.GotPlant = false;
                planter.mechPlanter.PlantMechID = -1;
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                return;
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }
    }
}
