using KMath;
using UnityEngine;
using Item;
using Enums;
using BehaviorTree;
using Planet;
using Unity.Collections.LowLevel.Unsafe;

namespace Action
{
    public class ThrowFlareAction
    {
        // Todo: Make this usable by AI.
        static public NodeState Action(object objData, int id)
        {
            ref BehaviorTreeState data = ref UnsafeUtility.As<object, BehaviorTreeState>(ref objData);
            ref PlanetState planet = ref GameState.Planet;
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(data.AgentID);
            ItemInventoryEntity itemEntity = agentEntity.GetItem();
            Item.FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.5f;
            startPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(agentEntity.agentInventory.InventoryID, itemEntity.itemInventory.SlotID);
            itemEntity.Destroy();

            planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.Flare, agentEntity.agentID.ID, false);
            // Todo: Urgent(Create new cool down system)
            //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);

            return NodeState.Success;
        }
    }
}

