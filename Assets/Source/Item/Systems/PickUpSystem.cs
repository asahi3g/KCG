﻿using KMath;
using Enums;

namespace Item
{
    public class PickUpSystem
    {
        // Todo:
        //  Hash entities by their position.
        //  Only call this after an item or an agent has changed position. 
        public void Update(Contexts contexts)
        {
            var agents = contexts.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState, AgentMatcher.AgentInventory));
            var pickableItems = contexts.itemParticle.GetGroup(
                ItemParticleMatcher.AllOf(ItemParticleMatcher.ItemID, ItemParticleMatcher.ItemPhysicsState).NoneOf(ItemParticleMatcher.ItemUnpickable));

            foreach (var item in pickableItems)
            {
                // Get item ceter position.
                var itemPropreties = GameState.ItemCreationApi.Get(item.itemType.Type);
                Vec2f centerPos = item.itemPhysicsState.Position + itemPropreties.SpriteSize / 2.0f;
                foreach (var agent in agents)
                {
                    if (!agent.agentInventory.AutoPick)
                        continue;
                    // Todo: Use action center Position.
                    if ((agent.agentPhysicsState.Position - centerPos).Magnitude <= 1.25f)
                        GameState.ActionCreationSystem.CreateAction(contexts, Enums.NodeType.PickUpAction, agent.agentID.ID, item.itemID.ID);
                }    
            }
        }
    }
}
