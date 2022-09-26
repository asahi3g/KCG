using KMath;
using UnityEngine;
using Item;
using Enums;

namespace Node
{
    public class ThrowGasBombAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ThrowFragGrenadeAction; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(ItemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.5f;
            startPos.Y += 0.5f;

            GameState.InventoryManager.RemoveItem(planet.EntitasContext, agentEntity.agentInventory.InventoryID, ItemEntity.itemInventory.SlotID);
            ItemEntity.Destroy();

            ProjectileEntity projectileEntity = planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, Enums.ProjectileType.GasGrenade, false);
            nodeEntity.nodeExecution.State = Enums.NodeState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }

        // Todo: Move this out to a projectile system.
       //public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
       //
       //   float damage = 0.1f;
       //
       //   // Check if projectile has hit something and was destroyed.
       //   if (!ProjectileEntity.isEnabled)
       //   {
       //       nodeEntity.nodeExecution.State = Enums.NodeState.Success;
       //       return;
       //   }
       //
       //   elapsed += Time.deltaTime;
       //
       //   if(elapsed > 3.0f)
       //   {
       //       CircleSmoke.Spawn(1, ProjectileEntity.projectilePhysicsState.Position, new Vec2f(0.1f, 0.2f), new Vec2f(0.1f, 0.2f));
       //   }
       //
       //   if(elapsed > 8.0f)
       //   {
       //       nodeEntity.nodeExecution.State = Enums.NodeState.Success;
       //       return;
       //   }
       //
       //   // Check if projectile has hit a enemy.
       //   var entities = EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentID));
       //
       //   // Todo: Create a agent colision system?
       //   foreach (var entity in entities)
       //   {
       //       float dist = Vec2f.Distance(new Vec2f(agentEntity.agentPhysicsState.Position.X, agentEntity.agentPhysicsState.Position.Y), new Vec2f(ProjectileEntity.projectilePhysicsState.Position.X, ProjectileEntity.projectilePhysicsState.Position.Y));
       //
       //       if (dist < 2.0f)
       //       {
       //          Vec2f entityPos = entity.agentPhysicsState.Position;
       //          Vec2f bulletPos = ProjectileEntity.projectilePhysicsState.Position;
       //          Vec2f diff = bulletPos - entityPos;
       //          diff.Y = 0;
       //          diff.Normalize();
       //
       //          Vector2 oppositeDirection = new Vector2(-diff.X, -diff.Y);
       //
       //           if (agentEntity.hasAgentStats)
       //           {
       //               var stats = entity.agentStats;
       //               stats.Health -= (int)damage;
       //
       //               planet.AddFloatingText("SMOKE!", 0.001f, Vec2f.Zero, agentEntity.agentPhysicsState.Position);
       //
       //               // spawns a debug floating text for damage 
       //               planet.AddFloatingText("" + damage, 0.001f, new Vec2f(oppositeDirection.x * 0.05f, oppositeDirection.y * 0.05f), new Vec2f(entityPos.X, entityPos.Y + 0.85f));
       //           }
       //       }
       //   }
       //
    }
}

