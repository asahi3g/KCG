//imports UnityEngine

using Enums;

namespace Node
{
    public class DrinkPotionAction : NodeBase
    {
        public override ItemUsageActionType Type => ItemUsageActionType.DrinkPotionAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ref var planet = ref GameState.Planet;
            var itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            var player = planet.Player;
            if (itemEntity.hasItemPotion)
            {
                
            if (itemEntity.itemPotion.potionType == PotionType.Error)
                itemEntity.itemPotion.potionType = PotionType.HealthPotion;
                
                var entities = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            var Slots = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryInventoryEntity.Slots;

                            for (int i = 0; i < Slots.Length; i++)
                            {
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(entity.inventoryID.ID, i);

                                if (item != null)
                                {
                                    if (item.hasItemStack)
                                    {
                                        if (itemEntity.itemPotion.potionType == PotionType.Error)
                                        {
                                            nodeEntity.nodeExecution.State = NodeState.Success;
                                            return;
                                        }

                                        switch (itemEntity.itemPotion.potionType)
                                        {
                                            case PotionType.HealthPotion:
                                                if (item.itemType.Type == ItemType.HealthPotion)
                                                {
                                                    var worldPosition = ECSInput.InputProcessSystem.GetCursorWorldPosition();

                                                    player.UsePotion(2.0f);
                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        GameState.InventoryManager.RemoveItem(entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
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
                nodeEntity.nodeExecution.State = NodeState.Success;
            }

            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
