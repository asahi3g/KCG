//imports UnityEngine

using Enums;

namespace Node
{
    public class DrinkPotionAction : NodeBase
    {
        public override NodeType Type => NodeType.DrinkPotionAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity ItemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);

            if (ItemEntity.itemPotion.potionType == PotionType.Error)
                ItemEntity.itemPotion.potionType = PotionType.HealthPotion;

            var player = GameState.Planet.Player;
            if (ItemEntity.hasItemPotion)
            {
                var entities = GameState.Planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            var Slots = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                            for (int i = 0; i < Slots.Length; i++)
                            {
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(GameState.Planet.EntitasContext, entity.inventoryID.ID, i);

                                if (item != null)
                                {
                                    if (item.hasItemStack)
                                    {
                                        if (ItemEntity.itemPotion.potionType == PotionType.Error)
                                        {
                                            nodeEntity.nodeExecution.State = NodeState.Success;
                                            return;
                                        }

                                        switch (ItemEntity.itemPotion.potionType)
                                        {
                                            case PotionType.HealthPotion:
                                                if (item.itemType.Type == ItemType.HealthPositon)
                                                {
                                                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    player.UsePotion(2.0f);
                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
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
