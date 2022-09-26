using UnityEngine;
using Enums;

namespace Node
{
    public class ToolActionPotion : NodeBase
    {
        public override NodeType Type { get { return NodeType.ToolActionPotion; } }

        public override void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (ItemEntity.itemPotion.potionType == Enums.PotionType.Error)
                ItemEntity.itemPotion.potionType = Enums.PotionType.HealthPotion;

            var player = planet.Player;
            if (ItemEntity.hasItemPotion)
            {
                var entities = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            var Slots = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                            for (int i = 0; i < Slots.Length; i++)
                            {
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, entity.inventoryID.ID, i);

                                if (item != null)
                                {
                                    if (item.hasItemStack)
                                    {
                                        if (ItemEntity.itemPotion.potionType == Enums.PotionType.Error)
                                        {
                                            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                            return;
                                        }

                                        switch (ItemEntity.itemPotion.potionType)
                                        {
                                            case Enums.PotionType.HealthPotion:
                                                if (item.itemType.Type == Enums.ItemType.HealthPositon)
                                                {
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    player.UsePotion(2.0f);
                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                nodeEntity.nodeExecution.State = Enums.NodeState.Success;
            }

            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
