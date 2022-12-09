using UnityEngine;
using System.Collections.Generic;
using KMath;
using Enums;
using System;

namespace Node
{
    public class WaterAction : NodeBase
    {
        public override ActionType  Type => ActionType .WaterAction;

        public override List<Tuple<string, Type>> RegisterEntries()
        {
            List<Tuple<string, Type>> blackboardEntries = new List<Tuple<string, Type>>()
            {
                CreateEntry("PlantID", typeof(int)),
            };
            return blackboardEntries;
        }

        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
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

                    if (mech.GetProperties().Group == MechGroup.Plant)
                        plant = mech;
                    else if (mech.mechType.mechType == MechType.Planter)
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
                if (nodeEntity.hasNodeBlackboardData)
                    plant = planet.EntitasContext.mech.GetEntityWithMechID(nodeEntity.nodeBlackboardData.entriesIDs[0]);
            }

            if (plant != null)
            {
                plant.mechPlant.WaterLevel = Mathf.Min(plant.mechPlant.WaterLevel + 10f, 100);
                nodeEntity.nodeExecution.State = NodeState.Success;
                return;
            }
            nodeEntity.nodeExecution.State = NodeState.Fail;
        }
    }
}
