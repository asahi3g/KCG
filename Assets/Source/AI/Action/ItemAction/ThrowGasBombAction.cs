using KMath;
using UnityEngine;
using NodeSystem;
using Planet;
using Unity.Collections.LowLevel.Unsafe;

namespace Action
{
    public class ThrowGasBombAction
    {
        // Action used by either player and AI.
        // Todo: Make this usable by AI.
        static public NodeState Action(object objData, int id)
        {
            ref NodesExecutionState data = ref UnsafeUtility.As<object, NodesExecutionState>(ref objData);
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

            ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.GasGrenade, agentEntity.agentID.ID, false);
            // Todo: Urgent(Create new cool down system)
            //GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
            return NodeState.Success;
        }
    }
}

