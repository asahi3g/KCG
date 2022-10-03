using System.Collections.Generic;
using Enums;
using Enums.Tile;
using KGUI.Elements;
using KMath;
using Planet;
using UnityEngine;
using UnityEngine.UI;
using Text = KGUI.Elements.Text;

namespace KGUI
{
    public class GUIManager
    {
        public bool ShowGUI = true;
        
        public AgentEntity AgentEntity;

        // Bedrock
        public ImageWrapper bedrockUI;
        public ImageWrapper bedrockUIBackground;

        // Dirt
        public ImageWrapper dirtUI;
        public ImageWrapper dirtUIBackground;

        // Wire
        public ImageWrapper wireUI;
        public ImageWrapper wireUIBackground;

        // Pipe
        public ImageWrapper pipeUI;
        public ImageWrapper pipeUIBackground;

        // Bedrock
        public ImageWrapper healthPotionUI;
        public ImageWrapper healthPotionUIBackground;

        // Default Cursors
        public ImageWrapper DefaultCursor;

        // Aim Cursor
        public ImageWrapper AimCursor;

        // Aim Cursor
        public ImageWrapper BuildCursor;
        
        public Dictionary<UIPanelID, UIPanel> UIPanelPrefabList = new();
        public Dictionary<UIPanelID, UIPanel> UIPanelList = new();

        public Vec2f CursorPosition;
        public UIElement CursorElement;

        // Canvas
        private Canvas canvas;

        // Planet
        private PlanetState planet;

        // Scanner Tool Text
        Text scannerText = new();

        // Inventory ID
        int inventoryID;

        // Inventory Entity
        InventoryEntity Inventory;

        // Selected Slot
        int selectedSlot;

        // Item Entity
        public ItemInventoryEntity Item;

        GeometryGUI GeometryGUI;

        // Initialize
        public void InitStage1(PlanetState planet, AgentEntity agentEntity)
        {
            // Set Planet
            this.planet = planet;

            AgentEntity = agentEntity;

            // Hide Cursor
            Cursor.visible = true;

            // Set Canvas
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            UIPanelPrefabList.Add(UIPanelID.PlayerStatus, Resources.Load<UIPanel>("GUIPrefabs/PlayerStatusUI"));
            UIPanelPrefabList.Add(UIPanelID.PlacementTool, null);

            GeometryGUI = new GeometryGUI();

            GeometryGUI.Initialize(ref planet);

            // Initialize Bedrock Widget
            bedrockUIBackground = planet.AddUIImage("BedrockBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-600.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.ImageWrapper;
            bedrockUIBackground.SetImageMidBottom();

            bedrockUI = planet.AddUIImage("Bedrock", bedrockUIBackground.Transform, "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.ImageWrapper;

            // Initialize Dirt Widget
            dirtUIBackground = planet.AddUIImage("DirtBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-550.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.ImageWrapper;
            dirtUIBackground.SetImageMidBottom();

            dirtUI = planet.AddUIImage("DirtTile", dirtUIBackground.Transform, "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.ImageWrapper;

            // Initialize Pipe Widget
            pipeUIBackground = planet.AddUIImage("PipeBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-500.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.ImageWrapper;
            pipeUIBackground.SetImageMidBottom();

            pipeUI = planet.AddUIImage("PipeTile", pipeUIBackground.Transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.ImageWrapper;

            // Initialize Wire Widget
            wireUIBackground = planet.AddUIImage("WireBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-450.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.ImageWrapper;
            wireUIBackground.SetImageMidBottom();

            wireUI = planet.AddUIImage("WireTile", wireUIBackground.Transform, "Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 128, 128).kGUIElementsImage.ImageWrapper;

            // Initialize Health Potion Widget
            healthPotionUIBackground = planet.AddUIImage("HealthPotionBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-450.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.ImageWrapper;
            healthPotionUIBackground.SetImageMidBottom();

            healthPotionUI = planet.AddUIImage("HealthPotion", healthPotionUIBackground.Transform, "Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 19, 19).kGUIElementsImage.ImageWrapper;
        }

        public void InitStage2()
        {
            SetPanelActive(UIPanelID.PlayerStatus);
        }

        public void SetPanelActive(UIPanelID panelID, bool active = true)
        {
            if (UIPanelList.TryGetValue(panelID, out var panel))
            {
                panel.gameObject.SetActive(active);
            }
            else if(UIPanelPrefabList.TryGetValue(panelID, out var panelPrefab))
            {
                Object.Instantiate(panelPrefab, canvas.transform);
            }
            else
            {
                Debug.LogError("No such panel in prefab list");
            }
        }

        public void Update(AgentEntity agentEntity)
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution =
                new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            
            CursorPosition = new Vec2f(Input.mousePosition.x, Input.mousePosition.y);
            
            scannerText.Update();
            GeometryGUI.Update(ref planet, agentEntity);
            
            inventoryID = agentEntity.agentInventory.InventoryID;
            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;
            Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);

            if (Item != null)
            {
                if (Item.itemType.Type is ItemType.PlacementTool or ItemType.PlacementMaterialTool)
                {
                    // If Mouse Over On Widgets
                    if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GameObject.active ||
                        dirtUIBackground.IsMouseOver(CursorPosition) && dirtUIBackground.GameObject.active ||
                        pipeUIBackground.IsMouseOver(CursorPosition) && pipeUIBackground.GameObject.active ||
                        wireUIBackground.IsMouseOver(CursorPosition) && wireUIBackground.GameObject.active)
                    {
                        Item.itemTile.InputsActive = false;
                    }
                    else
                    {
                        Item.itemTile.InputsActive = true;
                    }
                }

                switch (Item.itemType.Type)
                {
                    case ItemType.PlacementTool:
                        dirtUIBackground.GameObject.SetActive(true);
                        bedrockUIBackground.GameObject.SetActive(true);
                        wireUIBackground.GameObject.SetActive(true);
                        pipeUIBackground.GameObject.SetActive(true);
                        healthPotionUIBackground.GameObject.SetActive(false);

                        // If Selected     = Red
                        // If Not Selected = Yellow
                        bedrockUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Bedrock
                            ? Color.red
                            : Color.yellow);
                        dirtUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Moon
                            ? Color.red
                            : Color.yellow);
                        pipeUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Pipe
                            ? Color.red
                            : Color.yellow);
                        wireUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Wire
                            ? Color.red
                            : Color.yellow);
                        break;
                    case ItemType.PlacementMaterialTool:
                    {
                        dirtUIBackground.GameObject.SetActive(false);
                        bedrockUIBackground.GameObject.SetActive(false);
                        wireUIBackground.GameObject.SetActive(false);
                        pipeUIBackground.GameObject.SetActive(false);
                        healthPotionUIBackground.GameObject.SetActive(false);
                    
                        var inventories = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID, InventoryMatcher.InventoryName));
                    
                        foreach (var inventory in inventories)
                        {
                            if (inventory.inventoryName.Name != "MaterialBag") continue;
                            
                            var inventorySlots = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryEntity.Slots;
                            
                            for (int i = 0; i < inventorySlots.Length; i++)
                            {
                                var materialBag = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventory.inventoryID.ID, i);
                                if (materialBag == null) continue;
                                
                                switch (materialBag.itemType.Type)
                                {
                                    case ItemType.Dirt:
                                    {
                                        if (materialBag.hasItemStack)
                                        {
                                            dirtUIBackground.GameObject.SetActive(materialBag.itemStack.Count >= 1);
                                        }

                                        break;
                                    }
                                    case ItemType.Bedrock:
                                        bedrockUIBackground.GameObject.SetActive(true);
                                        break;
                                    case ItemType.Pipe:
                                        pipeUIBackground.GameObject.SetActive(true);
                                        break;
                                    case ItemType.Wire:
                                        wireUIBackground.GameObject.SetActive(true);
                                        break;
                                }
                                
                                if (materialBag.hasItemStack)
                                {
                                    inventoryID = agentEntity.agentInventory.InventoryID;
                                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;
                                    
                                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                                        inventoryID, selectedSlot);
                                    
                                    if (Item != null)
                                    {
                                        bedrockUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Bedrock
                                            ? Color.red
                                            : Color.yellow);

                                        dirtUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Moon
                                            ? Color.red
                                            : Color.yellow);

                                        pipeUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Pipe
                                            ? Color.red
                                            : Color.yellow);

                                        wireUIBackground.SetImageColor(Item.itemTile.TileID == TileID.Wire
                                            ? Color.red
                                            : Color.yellow);
                                    }
                                }
                            }
                        }

                        break;
                    }
                    case ItemType.PotionTool:
                    {
                        // Set All Tiles Active To False
                        healthPotionUIBackground.GameObject.SetActive(true);
                        dirtUIBackground.GameObject.SetActive(false);
                        bedrockUIBackground.GameObject.SetActive(false);
                        wireUIBackground.GameObject.SetActive(false);
                        pipeUIBackground.GameObject.SetActive(false);
                        
                        var inventories = planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID, InventoryMatcher.InventoryName));
                        
                        foreach (var inventory in inventories)
                        {
                            if (inventory.inventoryName.Name != "MaterialBag") continue;
                            
                            var inventorySlots = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventory.inventoryID.ID).inventoryEntity.Slots;

                            for (int i = 0; i < inventorySlots.Length; i++)
                            {
                                var materialBag = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventory.inventoryID.ID, i);
                                if (materialBag == null) continue;
                                    
                                if (materialBag.itemType.Type == ItemType.HealthPositon)
                                {
                                    // Entity Has Item Stack?
                                    if (materialBag.hasItemStack)
                                    {
                                        // Set Active True
                                        healthPotionUIBackground.GameObject.SetActive(true);
                                    }
                                }
                                else
                                {
                                    healthPotionUIBackground.GameObject.SetActive(false);
                                }
                                    
                                if (materialBag.hasItemStack)
                                {
                                    // Set Inventory Elements
                                    inventoryID = agentEntity.agentInventory.InventoryID;
                                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;
                                        
                                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                                        
                                    if (Item != null)
                                    {
                                        healthPotionUIBackground.SetImageColor(Item.itemPotion.potionType == PotionType.HealthPotion
                                            ? Color.red
                                            : Color.yellow);
                                    }
                                }
                            }
                        }

                        break;
                    }
                    default:
                        // If Item Is not equal to any placement tool,
                        // hide all of the widget tiles
                        dirtUIBackground.GameObject.SetActive(false);
                        bedrockUIBackground.GameObject.SetActive(false);
                        wireUIBackground.GameObject.SetActive(false);
                        pipeUIBackground.GameObject.SetActive(false);
                        healthPotionUIBackground.GameObject.SetActive(false);
                        break;
                }
            }
            
            HandleMouseEvents();
        }

        public void Draw()
        {
            foreach (var panel in UIPanelList.Values)
            {
                foreach (var element in panel.UIElementList.Values)
                {
                    element.Draw();
                }
            }
        }

        public void HandleMouseEvents()
        {
            if (CursorElement != null 
                && Collisions.Collisions.PointOverlapRect
                (
                    CursorPosition.X, CursorPosition.Y,
                    CursorElement.HitBox.xmin, CursorElement.HitBox.xmax, CursorElement.HitBox.ymin, CursorElement.HitBox.ymax)
               )
            {
                CursorElement.OnMouseStay();
            }
            else
            {
                CursorElement?.OnMouseExited();
                CursorElement = null;
                foreach (var panel in UIPanelList.Values)
                {
                    foreach (var element in panel.UIElementList.Values)
                    {
                        if (Collisions.Collisions.PointOverlapRect(CursorPosition.X, CursorPosition.Y, element.HitBox.xmin, element.HitBox.xmax, element.HitBox.ymin, element.HitBox.ymax))
                        {
                            CursorElement = element;
                            CursorElement.OnMouseEntered();
                            return;
                        }
                    }
                }
            }
            
            OnMouseClick(AgentEntity);
        }

        public void OnMouseClick(AgentEntity agentEntity)
        {
            if (Input.GetMouseButton(0))
            {
                CursorElement?.OnMouseClick();
            }
            
            // If Mosue 0 Button Down
            if (Input.GetMouseButton(0))
            {
                if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GameObject.active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if(Item != null)
                    {
                        switch (Item.itemType.Type)
                        {
                            case ItemType.PlacementTool:
                            case ItemType.PlacementMaterialTool:
                                Item.itemTile.TileID = TileID.Bedrock;
                                break;
                        }
                    }
                }
                if (dirtUIBackground.IsMouseOver(CursorPosition) && dirtUIBackground.GameObject.active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemTile.TileID = TileID.Moon;
                        }
                        else if (Item.itemType.Type == ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemTile.TileID = TileID.Moon;
                        }
                    }
                }
                if (pipeUIBackground.IsMouseOver(CursorPosition) && pipeUIBackground.GameObject.active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemTile.TileID = TileID.Pipe;
                        }
                        else if (Item.itemType.Type == ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemTile.TileID = TileID.Pipe;
                        }
                    }
                }
                if (wireUIBackground.IsMouseOver(CursorPosition) && wireUIBackground.GameObject.active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == ItemType.PlacementTool || Item.itemType.Type == ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemTile.TileID = TileID.Wire;
                        }
                    }
                }
            }
        }

        public void AddScannerText(string _text, Vec2f canvasPosition, Vec2f hudSize, float lifeTime)
        {
            // Create Scanner Text
            scannerText.Create("TempText", _text, canvas.transform, lifeTime);
            scannerText.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            scannerText.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));
            scannerText.StartLifeTime = true;
        }

        public Text AddText(string _text, Vec2f canvasPosition, Vec2f hudSize)
        {
            // Add Temporary text
            Text text = new Text();
            text.Create("TempText", _text, canvas.transform, 1);
            text.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            text.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));

            return text;
        }

        // Kill
        public void Teardown()
        {
            
        }
    }
}
