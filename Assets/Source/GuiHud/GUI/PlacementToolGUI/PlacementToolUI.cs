using Enums;
using Enums.Tile;
using UnityEngine;

namespace KGUI
{
    public class PlacementToolUI : UIPanel
    {
        [SerializeField] private BedrockElementUI bedrockElementUI;
        [SerializeField] private DirtElementUI dirtElementUI;
        [SerializeField] private PipeElementUI pipeElementUI;
        [SerializeField] private WireElementUI wireElementUI;
        [SerializeField] private HealthPotionElementUI healthPotionElementUI;

        public override void Update()
        {
            var item = GameState.GUIManager.InventorySlotItem;
            if(item == null) return;

            var planet = GameState.GUIManager.Planet;
            
            switch (item.itemType.Type)
            {
                case ItemType.PlacementTool:
                    bedrockElementUI.gameObject.SetActive(true);
                    dirtElementUI.gameObject.SetActive(true);
                    wireElementUI.gameObject.SetActive(true);
                    pipeElementUI.gameObject.SetActive(true);
                    healthPotionElementUI.gameObject.SetActive(false);
                    
                    // If Selected     = Red
                    // If Not Selected = Yellow
                    bedrockElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Bedrock
                        ? Color.red
                        : Color.yellow);
                    dirtElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Moon
                        ? Color.red
                        : Color.yellow);
                    pipeElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Pipe
                        ? Color.red
                        : Color.yellow);
                    wireElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Wire
                        ? Color.red
                        : Color.yellow);
                    break;
                case ItemType.PlacementMaterialTool:
                    bedrockElementUI.gameObject.SetActive(false);
                    dirtElementUI.gameObject.SetActive(false);
                    wireElementUI.gameObject.SetActive(false);
                    pipeElementUI.gameObject.SetActive(false);
                    healthPotionElementUI.gameObject.SetActive(false);
                    
                    var inventories1 = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID, InventoryMatcher.InventoryName));

                    foreach (var inventoryEntity in inventories1)
                    {
                        if (inventoryEntity.inventoryName.Name != "MaterialBag") continue;

                        var inventorySlots = planet.EntitasContext.inventory
                            .GetEntityWithInventoryID(inventoryEntity.inventoryID.ID).inventoryEntity.Slots;

                        for (int i = 0; i < inventorySlots.Length; i++)
                        {
                            var materialBag = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryEntity.inventoryID.ID, i);
                            if (materialBag == null) continue;

                            switch (materialBag.itemType.Type)
                            {
                                case ItemType.Dirt:
                                {
                                    if (materialBag.hasItemStack)
                                    {
                                        dirtElementUI.gameObject.SetActive(materialBag.itemStack.Count >= 1);
                                    }

                                    break;
                                }
                                case ItemType.Bedrock:
                                    bedrockElementUI.gameObject.SetActive(true);
                                    break;
                                case ItemType.Pipe:
                                    pipeElementUI.gameObject.SetActive(true);
                                    break;
                                case ItemType.Wire:
                                    wireElementUI.gameObject.SetActive(true);
                                    break;
                            }

                            if (materialBag.hasItemStack)
                            {
                                bedrockElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Bedrock
                                    ? Color.red
                                    : Color.yellow);
                                dirtElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Moon
                                    ? Color.red
                                    : Color.yellow);

                                pipeElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Pipe
                                    ? Color.red
                                    : Color.yellow);

                                wireElementUI.Border.SetImageColor(item.itemTile.TileID == TileID.Wire
                                    ? Color.red
                                    : Color.yellow);
                            }
                        }
                    }

                    break;
                case ItemType.PotionTool:
                    healthPotionElementUI.gameObject.SetActive(true);
                    bedrockElementUI.gameObject.SetActive(false);
                    dirtElementUI.gameObject.SetActive(false);
                    wireElementUI.gameObject.SetActive(false);
                    pipeElementUI.gameObject.SetActive(false);
                    
                    var inventories2 = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID, InventoryMatcher.InventoryName));

                    foreach (var inventoryEntity in inventories2)
                    {
                        if (inventoryEntity.inventoryName.Name != "MaterialBag") continue;

                        var inventorySlots = planet.EntitasContext.inventory
                            .GetEntityWithInventoryID(inventoryEntity.inventoryID.ID).inventoryEntity.Slots;

                        for (int i = 0; i < inventorySlots.Length; i++)
                        {
                            var materialBag = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryEntity.inventoryID.ID, i);
                            if (materialBag == null) continue;

                            if (materialBag.itemType.Type == ItemType.HealthPositon)
                            {
                                if (materialBag.hasItemStack)
                                {
                                    healthPotionElementUI.gameObject.SetActive(true);
                                }
                            }
                            else
                            {
                                healthPotionElementUI.gameObject.SetActive(false);
                            }

                            if (materialBag.hasItemStack)
                            {
                                healthPotionElementUI.Border.SetImageColor(item.itemPotion.potionType == PotionType.HealthPotion ? Color.red : Color.yellow);
                            }
                        }
                    }

                    break;
                default:
                    bedrockElementUI.gameObject.SetActive(false);
                    dirtElementUI.gameObject.SetActive(false);
                    wireElementUI.gameObject.SetActive(false);
                    pipeElementUI.gameObject.SetActive(false);
                    healthPotionElementUI.gameObject.SetActive(false);
                    break;
            }
        }
        
        public override void Init()
        {
            ID = UIPanelID.PlacementTool;
            
            UIElementList.Add(bedrockElementUI.ID, bedrockElementUI);
            UIElementList.Add(dirtElementUI.ID, dirtElementUI);
            UIElementList.Add(pipeElementUI.ID, pipeElementUI);
            UIElementList.Add(wireElementUI.ID, wireElementUI);
            UIElementList.Add(healthPotionElementUI.ID, healthPotionElementUI);
            
            base.Init();
        }

        public override void OnDeactivate()
        {
            var inventoryID = GameState.GUIManager.AgentEntity.agentInventory.InventoryID;
            var inventory = GameState.GUIManager.Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            var selectedSlot = inventory.inventoryEntity.SelectedSlotID;
            
            var item = GameState.InventoryManager.GetItemInSlot(GameState.GUIManager.Planet.EntitasContext, inventoryID, selectedSlot);
            if(item != null)
            {
                switch (item.itemType.Type)
                {
                    case ItemType.PlacementTool:
                    case ItemType.PlacementMaterialTool:
                        item.itemTile.TileID = TileID.Error;
                        break;
                }
            }
            
            bedrockElementUI.Border.SetImageColor(Color.yellow);
        }
    }
}
