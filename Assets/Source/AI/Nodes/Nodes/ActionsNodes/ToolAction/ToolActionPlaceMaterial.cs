using UnityEngine;
using Enums.Tile;
using Planet;

namespace Node.Action
{
    public class ToolActionPlaceMaterial : NodeBase
    {
        public override void OnEnter(ref PlanetState planet, NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            itemEntity.itemCastData.data.Layer = MapLayerType.Front;

            bool CanPlace = true;

            if (itemEntity.hasItemCastData)
            {
                var entities = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            var Slots = planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;
                            for(int i = 0; i < Slots.Length; i++)
                            {
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, entity.inventoryID.ID, i);

                                if (item != null)
                                {
                                    if (item.hasItemStack)
                                    {
                                        if (itemEntity.itemCastData.data.TileID == TileID.Error ||
                                                        itemEntity.itemCastData.data.TileID == TileID.Air ||
                                                        itemEntity.itemCastData.data.Layer != MapLayerType.Front)
                                        {
                                            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                            return;
                                        }

                                        switch (itemEntity.itemCastData.data.TileID)
                                        {
                                            case TileID.Moon:
                                                if (item.itemType.Type == Enums.ItemType.Dirt)
                                                {
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    if (planet.TileMap.GetFrontTileID(x, y) == TileID.Moon)
                                                    {
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }
                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemCastData.data.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            // If Case Is Bedrock
                                            case TileID.Bedrock:
                                                if (item.itemType.Type == Enums.ItemType.Bedrock)
                                                {
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    // If Selected Tile Already Have Same Tile
                                                    if (planet.TileMap.GetFrontTileID(x, y) == TileID.Bedrock)
                                                    {
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemCastData.data.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            case TileID.Pipe:
                                                if (item.itemType.Type == Enums.ItemType.Pipe)
                                                {
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    if (planet.TileMap.GetFrontTileID(x, y) == TileID.Pipe)
                                                    {
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemCastData.data.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            case TileID.Wire:
                                                if (item.itemType.Type == Enums.ItemType.Wire)
                                                {
                                                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    // If Selected Tile Already Have Same Tile
                                                    if (planet.TileMap.GetFrontTileID(x, y) == TileID.Wire)
                                                    {
                                                        nodeEntity.nodeExecution.State = Enums.NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemCastData.data.TileID = TileID.Error;
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

                if (itemEntity.itemCastData.InputsActive && CanPlace)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    // Check Map Size
                    if (x >= 0 && x < planet.TileMap.MapSize.X && y >= 0 && y < planet.TileMap.MapSize.Y)
                        planet.TileMap.SetFrontTile(x, y, itemEntity.itemCastData.data.TileID);
                }
            }
            else
            {
                NodeProperties nodeProperties = GameState.ActionCreationApi.Get(nodeEntity.nodeID.TypeID);
                Data data = (Data)nodeProperties.ObjectData;

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                // Check Map Size
                if (x >= 0 && x < planet.TileMap.MapSize.X &&
                        y >= 0 && y < planet.TileMap.MapSize.Y)
                {
                    switch (data.Layer)
                    {
                        case MapLayerType.Back:
                            planet.TileMap.SetBackTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Mid:
                            planet.TileMap.SetMidTile(x, y, data.TileID);
                            break;
                        case MapLayerType.Front:
                            planet.TileMap.SetFrontTile(x, y, data.TileID);
                            break;
                    }
                }
            }
            nodeEntity.nodeExecution.State = Enums.NodeState.Success;
        }
    }
}
