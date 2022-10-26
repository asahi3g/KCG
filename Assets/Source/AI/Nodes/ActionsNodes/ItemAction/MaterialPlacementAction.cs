//imports UnityEngine

using Enums.Tile;
using Enums;

namespace Node.Action
{
    public class MaterialPlacementAction : NodeBase
    {
        public override NodeType Type => NodeType.MaterialPlacementAction;
        public override NodeGroup NodeGroup => NodeGroup.ActionNode;


        public override void OnEnter(NodeEntity nodeEntity)
        {
            ItemInventoryEntity itemEntity = GameState.Planet.EntitasContext.itemInventory.GetEntityWithItemID(nodeEntity.nodeTool.ItemID);
            itemEntity.itemTile.Layer = MapLayerType.Front;

            bool CanPlace = true;

            if (itemEntity.hasItemTile)
            {
                var entities = GameState.Planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));
                foreach (var entity in entities)
                {
                    if (entity.hasInventoryName)
                    {
                        if (entity.inventoryName.Name == "MaterialBag")
                        {
                            var Slots = GameState.Planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;
                            for(int i = 0; i < Slots.Length; i++)
                            {
                                ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(GameState.Planet.EntitasContext, entity.inventoryID.ID, i);

                                if (item != null)
                                {
                                    if (item.hasItemStack)
                                    {
                                        if (itemEntity.itemTile.TileID == TileID.Error ||
                                                        itemEntity.itemTile.TileID == TileID.Air ||
                                                        itemEntity.itemTile.Layer != MapLayerType.Front)
                                        {
                                            nodeEntity.nodeExecution.State = NodeState.Success;
                                            return;
                                        }

                                        switch (itemEntity.itemTile.TileID)
                                        {
                                            case TileID.Moon:
                                                if (item.itemType.Type == ItemType.Dirt)
                                                {
                                                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) == TileID.Moon)
                                                    {
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }
                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemTile.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            // If Case Is Bedrock
                                            case TileID.Bedrock:
                                                if (item.itemType.Type == ItemType.Bedrock)
                                                {
                                                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    // If Selected Tile Already Have Same Tile
                                                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) == TileID.Bedrock)
                                                    {
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemTile.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            case TileID.Pipe:
                                                if (item.itemType.Type == ItemType.Pipe)
                                                {
                                                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) == TileID.Pipe)
                                                    {
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemTile.TileID = TileID.Error;
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }
                                                }
                                                break;

                                            case TileID.Wire:
                                                if (item.itemType.Type == ItemType.Wire)
                                                {
                                                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                                                    int x = (int)worldPosition.x;
                                                    int y = (int)worldPosition.y;

                                                    // If Selected Tile Already Have Same Tile
                                                    if (GameState.Planet.TileMap.GetFrontTileID(x, y) == TileID.Wire)
                                                    {
                                                        nodeEntity.nodeExecution.State = NodeState.Success;
                                                        return;
                                                    }

                                                    item.itemStack.Count--;
                                                    if (item.itemStack.Count < 1)
                                                    {
                                                        CanPlace = false;
                                                        GameState.InventoryManager.RemoveItem(GameState.Planet.EntitasContext, entity.inventoryID.ID, item.itemInventory.SlotID);
                                                        item.Destroy();
                                                        itemEntity.itemTile.TileID = TileID.Error;
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

                if (itemEntity.itemTile.InputsActive && CanPlace)
                {
                    UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                    int x = (int)worldPosition.x;
                    int y = (int)worldPosition.y;

                    // Check Map Size
                    if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X && y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                        GameState.Planet.TileMap.SetFrontTile(x, y, itemEntity.itemTile.TileID);
                }
            }
            else
            {
                UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                int x = (int)worldPosition.x;
                int y = (int)worldPosition.y;

                // Check Map Size
                if (x >= 0 && x < GameState.Planet.TileMap.MapSize.X &&
                        y >= 0 && y < GameState.Planet.TileMap.MapSize.Y)
                {
                    switch (itemEntity.itemTile.Layer)
                    {
                        case MapLayerType.Back:
                            GameState.Planet.TileMap.SetBackTile(x, y, itemEntity.itemTile.TileID);
                            break;
                        case MapLayerType.Mid:
                            GameState.Planet.TileMap.SetMidTile(x, y, itemEntity.itemTile.TileID);
                            break;
                        case MapLayerType.Front:
                            GameState.Planet.TileMap.SetFrontTile(x, y, itemEntity.itemTile.TileID);
                            break;
                    }
                }
            }
            nodeEntity.nodeExecution.State = NodeState.Success;
        }
    }
}
