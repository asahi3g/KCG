using System;
using System.Collections;
using Entitas;
using UnityEngine;


namespace Inventory
{
    public class InventoryAttacher
    {
        private static InventoryAttacher instance;
        public static InventoryAttacher Instance => instance ??= new InventoryAttacher();

        private static int InventoryID = 0;

        public void AttachInventoryToAgent(Contexts entitasContext, int width, int height, AgentEntity agentEntity)
        {
            AgentEntity entity = agentEntity;
            entity.AddAgentInventory(InventoryID);
            CreateInventory(entitasContext, width, height, GameState.InventoryCreationApi.GetDefaultInventory());
        }

        // This is for chest and related items.
        //public void AttachInventorytoItem(Contexts entitasContext, int width, int height, int ItemID)
        //{
        //    ItemEntity entity = entitasContext.item.GetEntityWithItemID(ItemID);
        //    entity.AddItemInventory(InventoryID);
        //    MakeInventoryEntity(entitasContext, width, height);
        //}

        private InventoryEntity CreateInventory(Contexts entitasContext, int width, int height, int inventoryProprietiesID)
        {
            InventoryEntity entity = entitasContext.inventory.CreateEntity();
            entity.AddInventoryID(InventoryID++, inventoryProprietiesID);
            int length = width * height;
            entity.AddInventoryEntity(
                width,
                height,
                new Slot[length],
                new Utility.BitSet((UInt32)(length)),
                0 /*Selected slot*/ );

            // Initialize slots
            for (int i = 0; i < length; i++)
            {
                entity.inventoryEntity.Slots[i] = new Slot
                {
                    ID = i,
                    Restriction = Enums.ItemGroups.None,
                    SlotBorderBackgroundIcon = -1,
                    ItemID = -1,
                };
            }

            return entity;
        }
    }
}
