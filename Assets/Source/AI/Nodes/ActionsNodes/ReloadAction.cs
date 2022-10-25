using Inventory;
using Item;
using Planet;
using UnityEngine;
using Enums;

namespace Node.Action
{
    public class ReloadAction : NodeBase
    {
        public override NodeType Type { get { return NodeType.ReloadAction; } }
        public override NodeGroup NodeGroup { get { return NodeGroup.ActionNode; } }

        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem(ref planet);

            if (item != null)
            {
                if (item.hasItemFireWeaponClip)
                {
                    nodeEntity.nodeExecution.State = Enums.NodeState.Running;
                    return;
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Fail;
        }

        public override void OnUpdate(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem(ref planet);
            FireWeaponPropreties WeaponPropreties = GameState.ItemCreationApi.GetWeapon(item.itemType.Type);

            float runningTime = Time.realtimeSinceStartup - nodeEntity.nodeTime.StartTime;
            if (runningTime >= WeaponPropreties.ReloadTime)
            {
                if(item.hasItemFireWeaponClip)
                    item.itemFireWeaponClip.NumOfBullets = WeaponPropreties.ClipSize;

                nodeEntity.nodeExecution.State =  Enums.NodeState.Success;
            }
        }

        public override void OnExit(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity item = nodeEntity.GetItem(ref planet);

            if (item.hasItemFireWeaponClip)
                Debug.Log("Weapon Reloaded." + item.itemFireWeaponClip.NumOfBullets.ToString() + " Ammo in the clip.");
        }

        public override void OnFail(ref PlanetState plane, NodeEntity nodeEntity)
        {
            Debug.Log("Reload Failed.");
        }
    }
}
