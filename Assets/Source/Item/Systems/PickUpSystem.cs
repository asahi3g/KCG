using KMath;
using System.Diagnostics.Tracing;
using System.Drawing;
using UnityEngine;
using static UnityEditor.Progress;

namespace Item
{
    public class PickUpSystem
    {
        // Todo:
        //  Hash entities by their position.
        //  Only call this after an item or an agent has changed position. 
        // Todo: Maybe the system should not schedule action. 
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.ItemParticleList.Length; i++)
            {
                ItemParticleEntity itemParticle = planet.ItemParticleList.Get(i);

                // Update unpickable items.
                if (itemParticle.hasItemItemParticleAttributeUnpickable)
                {
                    itemParticle.itemItemParticleAttributeUnpickable.Duration += Time.deltaTime;
                    const float Unpickable_State_Duration = 2.0f; // How long item stays unpickable after dropped.
                    if (itemParticle.itemItemParticleAttributeUnpickable.Duration >= Unpickable_State_Duration)
                        itemParticle.RemoveItemItemParticleAttributeUnpickable();
                    continue;
                }

                // Get item ceter position.
                var itemProprieties = GameState.ItemCreationApi.Get(itemParticle.itemType.Type);
                Vec2f centerPos = itemParticle.itemPhysicsState.Position + itemProprieties.SpriteSize / 2.0f;
                const float PickingRadius = 2.0f; // Minimum distance to pick item.
                int[] agentIds = Collisions.Collisions.BroadphaseAgentCircleTest(centerPos, PickingRadius);

                foreach (int id in agentIds)
                {
                    AgentEntity agent = planet.EntitasContext.agent.GetEntityWithAgentID(id);
                    if (!agent.agentInventory.AutoPick)
                        continue;

                    GameState.ActionCreationSystem.CreateAction(Enums.NodeType.PickUpAction, agent.agentID.ID, itemParticle.itemID.ID);
                }
            }
        }
    }
}
