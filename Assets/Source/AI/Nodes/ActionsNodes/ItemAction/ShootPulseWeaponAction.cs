using KMath;
using Enums;
using UnityEngine;
using Item;

namespace Node
{
    public class ShootPulseWeaponAction : NodeBase
    {
        public override NodeType Type => NodeType.ShootPulseWeaponAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeEntity.nodeOwner.AgentID);
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            FireWeaponPropreties WeaponProperty = GameState.ItemCreationApi.GetWeapon(itemEntity.itemType.Type);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = worldPosition.x;
            float y = worldPosition.y;

            int bulletsPerShot = WeaponProperty.BulletsPerShot;

            if(itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (itemEntity.hasItemPulseWeaponPulse)
                {
                    int numGrenade = itemEntity.itemPulseWeaponPulse.NumberOfGrenades;

                    if (numGrenade == 0)
                    {
                        Debug.Log("Grenade Clip is empty. Press R to reload.");
                        nodeEntity.nodeExecution.State = NodeState.Fail;
                        return;
                    }
                }
            }
            else
            {
                if (itemEntity.hasItemFireWeaponClip)
                {
                    int numBullet = itemEntity.itemFireWeaponClip.NumOfBullets;

                    if (numBullet == 0)
                    {
                        Debug.Log("Clip is empty. Press R to reload.");
                        nodeEntity.nodeExecution.State = NodeState.Fail;
                        return;
                    }
                }
            }

            if (itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                if (itemEntity.hasItemPulseWeaponPulse)
                {
                    itemEntity.itemPulseWeaponPulse.NumberOfGrenades--;
                }
            }
            else
            {
                if (itemEntity.hasItemFireWeaponClip)
                {
                    itemEntity.itemFireWeaponClip.NumOfBullets -= bulletsPerShot;
                }
            }

            Vec2f startPos = agentEntity.agentPhysicsState.Position;
            startPos.X += 0.3f;
            startPos.Y += 0.5f;

            if (!itemEntity.itemPulseWeaponPulse.GrenadeMode)
            {
                var spread = WeaponProperty.SpreadAngle;

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    var random = Random.Range(-spread, spread);
                    GameState.Planet.AddProjectile(startPos, new Vec2f((x - startPos.X) - random, y - startPos.Y).Normalized, ProjectileType.Bullet, agentEntity.agentID.ID);
                }
            }
            else
                GameState.Planet.AddProjectile(startPos, new Vec2f(x - startPos.X, y - startPos.Y).Normalized, ProjectileType.Grenade, agentEntity.agentID.ID);

            nodeEntity.nodeExecution.State = NodeState.Running;
            GameState.ActionCoolDownSystem.SetCoolDown(GameState.Planet.EntitasContext, nodeEntity.nodeID.TypeID, agentEntity.agentID.ID, WeaponProperty.CoolDown);
        }
    }
}
