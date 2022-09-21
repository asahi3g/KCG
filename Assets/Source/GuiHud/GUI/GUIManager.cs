using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;
using KMath;
using Enums.Tile;
using Utility;

namespace KGUI
{
    public class GUIManager
    {
        public bool ShowGUI = true;
        
        public AgentEntity AgentEntity;

        // Bedrock
        public Image bedrockUI;
        public Image bedrockUIBackground;

        // Dirt
        public Image dirtUI;
        public Image dirtUIBackground;

        // Wire
        public Image wireUI;
        public Image wireUIBackground;

        // Pipe
        public Image pipeUI;
        public Image pipeUIBackground;

        // Bedrock
        public Image healthPotionUI;
        public Image healthPotionUIBackground;

        // Default Cursors
        public Image DefaultCursor;

        // Aim Cursor
        public Image AimCursor;

        // Aim Cursor
        public Image BuildCursor;

        // GUI Elements List
        public Dictionary<UIPanelID, UIPanel> UIPanelList = new();
        
        public Vec2f CursorPosition;
        public UIElement CursorElement;

        // Canvas
        private Canvas canvas;

        // Planet
        private Planet.PlanetState planet;

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
        public void InitStage1(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Set Planet
            this.planet = planet;

            AgentEntity = agentEntity;

            // Hide Cursor
            Cursor.visible = true;

            // Set Canvas
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            GeometryGUI = new GeometryGUI();

            GeometryGUI.Initialize(ref planet);

            // Initialize Bedrock Widget
            bedrockUIBackground = planet.AddUIImage("BedrockBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-600.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.Image;
            bedrockUIBackground.SetImageMidBottom();

            bedrockUI = planet.AddUIImage("Bedrock", bedrockUIBackground.GetTransform(), "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            // Initialize Dirt Widget
            dirtUIBackground = planet.AddUIImage("DirtBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-550.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.Image;
            dirtUIBackground.SetImageMidBottom();

            dirtUI = planet.AddUIImage("DirtTile", dirtUIBackground.GetTransform(), "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            // Initialize Pipe Widget
            pipeUIBackground = planet.AddUIImage("PipeBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-500.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.Image;
            pipeUIBackground.SetImageMidBottom();

            pipeUI = planet.AddUIImage("PipeTile", pipeUIBackground.GetTransform(), "Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            // Initialize Wire Widget
            wireUIBackground = planet.AddUIImage("WireBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-450.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.Image;
            wireUIBackground.SetImageMidBottom();

            wireUI = planet.AddUIImage("WireTile", wireUIBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 128, 128).kGUIElementsImage.Image;

            // Initialize Health Potion Widget
            healthPotionUIBackground = planet.AddUIImage("HealthPotionBackground", canvas.transform, "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png",
                new Vec2f(-450.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), 225, 225).kGUIElementsImage.Image;
            healthPotionUIBackground.SetImageMidBottom();

            healthPotionUI = planet.AddUIImage("HealthPotion", healthPotionUIBackground.GetTransform(), "Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 19, 19).kGUIElementsImage.Image;
        }

        public void InitStage2()
        {
            
        }

        public void Update(AgentEntity agentEntity)
        {
            inventoryID = agentEntity.agentInventory.InventoryID;

            canvas.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution =
                new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

            /*
            // Update Elements
            foreach (var uiObject in UIList)
            {
                uiObject.Update(agentEntity);
                DrawDebug.DrawBox(uiObject.ObjectPosition, uiObject.ObjectSize);
            }
            */

            // Assign New Cursor Position
            CursorPosition = new Vec2f(Input.mousePosition.x, Input.mousePosition.y);

            // Update Scanner Text
            scannerText.Update();

            // Set Inventory Elements
            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);

            // Set Selected Slot
            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

            // Create Item
            Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);

            if (Item != null)
            {

                if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                {
                    // If Mouse Over On Widgets
                    if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GetGameObject().active ||
                        dirtUIBackground.IsMouseOver(CursorPosition) && dirtUIBackground.GetGameObject().active ||
                        pipeUIBackground.IsMouseOver(CursorPosition) && pipeUIBackground.GetGameObject().active ||
                        wireUIBackground.IsMouseOver(CursorPosition) && wireUIBackground.GetGameObject().active)
                    {
                        // If Item Is Placement Tool
                        if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // DeActivate Inputs
                            Item.itemCastData.InputsActive = false;
                        }
                        else if (Item.itemType.Type == Enums.ItemType.PlacementMaterialTool) // If Item Is Material Tool
                        {
                            // Activate Inputs
                            Item.itemCastData.InputsActive = false;
                        }
                    }
                    else
                    {
                        // If not, DeActivate Inputs
                        Item.itemCastData.InputsActive = true;
                    }
                }

                if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                {
                    // Set All tiles Active
                    dirtUIBackground.GetGameObject().SetActive(true);
                    bedrockUIBackground.GetGameObject().SetActive(true);
                    wireUIBackground.GetGameObject().SetActive(true);
                    pipeUIBackground.GetGameObject().SetActive(true);
                    healthPotionUIBackground.GetGameObject().SetActive(false);

                    // If Selected     = Red
                    // If Not Selected = Yellow
                    bedrockUIBackground.SetImageColor(Item.itemCastData.data.TileID == TileID.Bedrock
                        ? Color.red
                        : Color.yellow);
                    dirtUIBackground.SetImageColor(Item.itemCastData.data.TileID == TileID.Moon
                        ? Color.red
                        : Color.yellow);
                    pipeUIBackground.SetImageColor(Item.itemCastData.data.TileID == TileID.Pipe
                        ? Color.red
                        : Color.yellow);
                    wireUIBackground.SetImageColor(Item.itemCastData.data.TileID == TileID.Wire
                        ? Color.red
                        : Color.yellow);
                }
                else if (Item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                {
                    // Set All Tiles Active To False
                    dirtUIBackground.GetGameObject().SetActive(false);
                    bedrockUIBackground.GetGameObject().SetActive(false);
                    wireUIBackground.GetGameObject().SetActive(false);
                    pipeUIBackground.GetGameObject().SetActive(false);
                    healthPotionUIBackground.GetGameObject().SetActive(false);

                    // Get Inventories
                    var entities =
                        planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

                    // Iterate All Inventories
                    foreach (var entity in entities)
                    {
                        // Check Component Availble
                        if (entity.hasInventoryName)
                        {
                            // Check Entity Name Is Equals To Material Bag
                            if (entity.inventoryName.Name == "MaterialBag")
                            {
                                // Get All Slots
                                var Slots = planet.EntitasContext.inventory
                                    .GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                                // Iterate All Slots
                                for (int i = 0; i < Slots.Length; i++)
                                {
                                    // Get Item
                                    ItemInventoryEntity MaterialBag =
                                        GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                                            entity.inventoryID.ID, i);

                                    // Check Item Is Available
                                    if (MaterialBag != null)
                                    {
                                        // Item Equals To Dirt?
                                        if (MaterialBag.itemType.Type == Enums.ItemType.Dirt)
                                        {
                                            // Entity Has Item Stack?
                                            if (MaterialBag.hasItemStack)
                                            {
                                                // Check Count Of The Item
                                                if (MaterialBag.itemStack.Count >= 1)
                                                {
                                                    // Set Active True
                                                    dirtUIBackground.GetGameObject().SetActive(true);
                                                }
                                                else
                                                {
                                                    // Set Active False
                                                    dirtUIBackground.GetGameObject().SetActive(false);
                                                }
                                            }
                                        }
                                        else if (MaterialBag.itemType.Type == Enums.ItemType.Bedrock)
                                        {
                                            // If Item Equals To Bedrock, Set Bedrock Active True
                                            bedrockUIBackground.GetGameObject().SetActive(true);
                                        }
                                        else if (MaterialBag.itemType.Type == Enums.ItemType.Pipe)
                                        {
                                            // If Item Equals To Pipe, Set Pipe Active True
                                            pipeUIBackground.GetGameObject().SetActive(true);
                                        }
                                        else if (MaterialBag.itemType.Type == Enums.ItemType.Wire)
                                        {
                                            // If Item Equals To Wire, Set Wire Active True
                                            wireUIBackground.GetGameObject().SetActive(true);
                                        }

                                        // Check Item Has Stack
                                        if (MaterialBag.hasItemStack)
                                        {
                                            // Set Inventory Elements
                                            inventoryID = agentEntity.agentInventory.InventoryID;
                                            Inventory =
                                                planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                                            // Create Item
                                            Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                                                inventoryID, selectedSlot);

                                            // Check If Item Is Available
                                            if (Item != null)
                                            {
                                                if (Item.itemCastData.data.TileID == TileID.Bedrock)
                                                {
                                                    // Set Red After Selected
                                                    bedrockUIBackground.SetImageColor(Color.red);
                                                }
                                                else
                                                {
                                                    // Set Yellow After Unselected
                                                    bedrockUIBackground.SetImageColor(Color.yellow);
                                                }

                                                if (Item.itemCastData.data.TileID == TileID.Moon)
                                                {
                                                    // Set Red After Selected
                                                    dirtUIBackground.SetImageColor(Color.red);
                                                }
                                                else
                                                {
                                                    // Set Yellow After Unselected
                                                    dirtUIBackground.SetImageColor(Color.yellow);
                                                }

                                                if (Item.itemCastData.data.TileID == TileID.Pipe)
                                                {
                                                    // Set Red After Selected
                                                    pipeUIBackground.SetImageColor(Color.red);
                                                }
                                                else
                                                {
                                                    // Set Yellow After Unselected
                                                    pipeUIBackground.SetImageColor(Color.yellow);
                                                }

                                                if (Item.itemCastData.data.TileID == TileID.Wire)
                                                {
                                                    // Set Red After Selected
                                                    wireUIBackground.SetImageColor(Color.red);
                                                }
                                                else
                                                {
                                                    // Set Yellow After Unselected
                                                    wireUIBackground.SetImageColor(Color.yellow);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Item.itemType.Type == Enums.ItemType.PotionTool)
                {
                    // Set All Tiles Active To False
                    healthPotionUIBackground.GetGameObject().SetActive(true);
                    dirtUIBackground.GetGameObject().SetActive(false);
                    bedrockUIBackground.GetGameObject().SetActive(false);
                    wireUIBackground.GetGameObject().SetActive(false);
                    pipeUIBackground.GetGameObject().SetActive(false);

                    // Get Inventories
                    var entities =
                        planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

                    // Iterate All Inventories
                    foreach (var entity in entities)
                    {
                        // Check Component Availble
                        if (entity.hasInventoryName)
                        {
                            // Check Entity Name Is Equals To Material Bag
                            if (entity.inventoryName.Name == "MaterialBag")
                            {
                                // Get All Slots
                                var Slots = planet.EntitasContext.inventory
                                    .GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                                // Iterate All Slots
                                for (int i = 0; i < Slots.Length; i++)
                                {
                                    // Get Item
                                    ItemInventoryEntity MaterialBag =
                                        GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                                            entity.inventoryID.ID, i);

                                    // Check Item Is Available
                                    if (MaterialBag != null)
                                    {
                                        // Item Equals To Dirt?
                                        if (MaterialBag.itemType.Type == Enums.ItemType.HealthPositon)
                                        {
                                            // Entity Has Item Stack?
                                            if (MaterialBag.hasItemStack)
                                            {
                                                // Set Active True
                                                healthPotionUIBackground.GetGameObject().SetActive(true);
                                            }
                                        }
                                        else
                                        {
                                            healthPotionUIBackground.GetGameObject().SetActive(false);
                                        }

                                        // Check Item Has Stack
                                        if (MaterialBag.hasItemStack)
                                        {
                                            // Set Inventory Elements
                                            inventoryID = agentEntity.agentInventory.InventoryID;
                                            Inventory =
                                                planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                                            // Create Item
                                            Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext,
                                                inventoryID, selectedSlot);

                                            // Check If Item Is Available
                                            if (Item != null)
                                            {
                                                if (Item.itemPotionCastData.potionType == Enums.PotionType.HealthPotion)
                                                {
                                                    // Set Red After Selected
                                                    healthPotionUIBackground.SetImageColor(Color.red);
                                                }
                                                else
                                                {
                                                    // Set Yellow After Unselected
                                                    healthPotionUIBackground.SetImageColor(Color.yellow);
                                                }
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
                    // If Item Is not equal to any placement tool,
                    // hide all of the widget tiles
                    dirtUIBackground.GetGameObject().SetActive(false);
                    bedrockUIBackground.GetGameObject().SetActive(false);
                    wireUIBackground.GetGameObject().SetActive(false);
                    pipeUIBackground.GetGameObject().SetActive(false);
                    healthPotionUIBackground.GetGameObject().SetActive(false);
                }
            }
            
            HandleMouseEvents();
        }

        public void Draw()
        {
            // Update Elements
            for (int i = 0; i < UIPanelList.Count; i++)
            {
                //UIList[i].Draw();
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
                if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GetGameObject().active)
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
                            case Enums.ItemType.PlacementTool:
                            case Enums.ItemType.PlacementMaterialTool:
                                Item.itemCastData.data.TileID = TileID.Bedrock;
                                break;
                        }
                    }
                }
                if (dirtUIBackground.IsMouseOver(CursorPosition) && dirtUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Moon;
                        }
                        else if (Item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Moon;
                        }
                    }
                }
                if (pipeUIBackground.IsMouseOver(CursorPosition) && pipeUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Pipe;
                        }
                        else if (Item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Pipe;
                        }
                    }
                }
                if (wireUIBackground.IsMouseOver(CursorPosition) && wireUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    Item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                    if (Item != null)
                    {
                        if (Item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Wire;
                        }
                        else if (Item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            Item.itemCastData.data.TileID = TileID.Wire;
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
            scannerText.startLifeTime = true;
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
