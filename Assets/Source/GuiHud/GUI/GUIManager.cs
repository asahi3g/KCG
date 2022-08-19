using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;
using KMath;
using Enums.Tile;

namespace KGUI
{
    public class GUIManager
    {
        // Food Bar
        public KGUI.PlayerStatus.FoodBarUI foodBarUI;

        // Water Bar
        public KGUI.PlayerStatus.WaterBarUI waterBarUI;

        // Oxygen Bar
        public KGUI.PlayerStatus.OxygenBarUI oxygenBarUI;

        // Fuel Bar
        public KGUI.PlayerStatus.FuelBarUI fuelBarUI;

        // Health Bar
        public KGUI.PlayerStatus.HealthBarUI healthBarUI;

        // Bedrock
        public KGUI.Elements.Image bedrockUI;
        public KGUI.Elements.Image bedrockUIBackground;

        // Dirt
        public KGUI.Elements.Image dirtUI;
        public KGUI.Elements.Image dirtUIBackground;

        // Wire
        public KGUI.Elements.Image wireUI;
        public KGUI.Elements.Image wireUIBackground;

        // Pipe
        public KGUI.Elements.Image pipeUI;
        public KGUI.Elements.Image pipeUIBackground;

        // GUI Elements List
        public List<GUIManager> UIList = new List<GUIManager>();

        // Cursor Screen Position from Unity Input Class
        public Vec2f CursorPosition;

        // Object Screen Position
        public Vec2f ObjectPosition;

        // Run Various Functions One Time
        private bool CanRun = true;

        // UI Scaling
        private float HUDScale = 1.0f;

        // Canvas
        Canvas _Canvas;

        // Planet
        Planet.PlanetState _planet;

        // Scanner Tool Text
        Text scannerText = new Text();

        int inventoryID;
        InventoryEntity Inventory;
        int selectedSlot;
        ItemInventoryEntity item;

        static bool Init = false;

        // Initialize
        public virtual void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            _planet = planet;

            // Set Canvas
            _Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            // Set HUD Scale
            _Canvas.scaleFactor = HUDScale;

            foodBarUI = new PlayerStatus.FoodBarUI();
            waterBarUI = new PlayerStatus.WaterBarUI();
            oxygenBarUI = new PlayerStatus.OxygenBarUI();
            fuelBarUI = new PlayerStatus.FuelBarUI();
            healthBarUI = new PlayerStatus.HealthBarUI();

            bedrockUIBackground = planet.AddUIImage("BedrockBackground", _Canvas.transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                new Vec2f(-600.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            bedrockUIBackground.SetImageMidBottom();

            bedrockUI = planet.AddUIImage("Bedrock", bedrockUIBackground.GetTransform(), "Assets\\StreamingAssets\\Tiles\\Blocks\\Bedrock\\bedrock.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            dirtUIBackground = planet.AddUIImage("DirtBackground", _Canvas.transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                new Vec2f(-550.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            dirtUIBackground.SetImageMidBottom();

            dirtUI = planet.AddUIImage("DirtTile", dirtUIBackground.GetTransform(), "Assets\\StreamingAssets\\Tiles\\Blocks\\Dirt\\dirt.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            pipeUIBackground = planet.AddUIImage("PipeBackground", _Canvas.transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                new Vec2f(-500.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            pipeUIBackground.SetImageMidBottom();

            pipeUI = planet.AddUIImage("PipeTile", pipeUIBackground.GetTransform(), "Assets\\StreamingAssets\\Items\\AdminIcon\\Pipesim\\admin_icon_pipesim.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            wireUIBackground = planet.AddUIImage("WireBackground", _Canvas.transform, UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                new Vec2f(-450.0f, -80.2f), new Vec3f(0.4f, 0.4f, 0.4f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            wireUIBackground.SetImageMidBottom();

            wireUI = planet.AddUIImage("WireTile", wireUIBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Pipesim\\Wires\\wires.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, 0.8f, 0.8f), 128, 128).kGUIElementsImage.Image;

            // Add Food Bar To Draw Array
            UIList.Add(foodBarUI);

            // Add Water Bar To Draw Array
            UIList.Add(waterBarUI);

            // Add Oxygen Bar To Draw Array
            UIList.Add(oxygenBarUI);

            // Add Fuel Bar To Draw Array
            UIList.Add(fuelBarUI);

            // Add Health Bar To Draw Array
            UIList.Add(healthBarUI);

            // Init Elements
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Initialize(planet, agentEntity);
            }

            Init = true;
        }

        public virtual void Update(AgentEntity agentEntity)
        {
            if(Init) 
            {
                // Update HUD Scale
                _Canvas.scaleFactor = HUDScale;

                // Update Elements
                for (int i = 0; i < UIList.Count; i++)
                {
                    UIList[i].Update(agentEntity);
                }

                // Assign New Cursor Position
                CursorPosition = new Vec2f(Input.mousePosition.x, Input.mousePosition.y);

                scannerText.Update();

                // Set Inventory Elements
                Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                // Create Item
                item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);
                if(item != null)
                {
                    if (item.itemType.Type == Enums.ItemType.PlacementTool)
                    {
                        if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GetGameObject().active ||
                            dirtUIBackground.IsMouseOver(CursorPosition)    && dirtUIBackground.GetGameObject().active || 
                            pipeUIBackground.IsMouseOver(CursorPosition)    && pipeUIBackground.GetGameObject().active ||
                            wireUIBackground.IsMouseOver(CursorPosition)    && wireUIBackground.GetGameObject().active)
                        {
                            if (item.itemType.Type == Enums.ItemType.PlacementTool)
                            {
                                item.itemCastData.InputsActive = false;
                            }
                            else if(item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                            {
                                item.itemCastData.InputsActive = false;
                            }
                        }
                        else
                        {
                            item.itemCastData.InputsActive = true;
                        }
                    }

                    if (item.itemType.Type == Enums.ItemType.PlacementTool)
                    {
                        // Set All tiles Active
                        dirtUIBackground.GetGameObject().SetActive(true);
                        bedrockUIBackground.GetGameObject().SetActive(true);
                        wireUIBackground.GetGameObject().SetActive(true);
                        pipeUIBackground.GetGameObject().SetActive(true);

                        // If Selected     = Red
                        // If Not Selected = Yellow
                        bedrockUIBackground.SetImageColor(item.itemCastData.data.TileID == TileID.Bedrock ? Color.red : Color.yellow);
                        dirtUIBackground.SetImageColor(item.itemCastData.data.TileID == TileID.Moon       ? Color.red : Color.yellow);
                        pipeUIBackground.SetImageColor(item.itemCastData.data.TileID == TileID.Pipe       ? Color.red : Color.yellow);
                        wireUIBackground.SetImageColor(item.itemCastData.data.TileID == TileID.Wire       ? Color.red : Color.yellow);
                    }
                    else if (item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                    {
                        // Set All Tiles Active To False
                        dirtUIBackground.GetGameObject().SetActive(false);
                        bedrockUIBackground.GetGameObject().SetActive(false);
                        wireUIBackground.GetGameObject().SetActive(false);
                        pipeUIBackground.GetGameObject().SetActive(false);

                        // Get Inventories
                        var entities = _planet.EntitasContext.inventory.GetGroup(InventoryMatcher.AllOf(InventoryMatcher.InventoryID));

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
                                    var Slots = _planet.EntitasContext.inventory.GetEntityWithInventoryID(entity.inventoryID.ID).inventoryEntity.Slots;

                                    // Iterate All Slots
                                    for(int i = 0; i < Slots.Length; i++)
                                    {
                                        // Get Item
                                        ItemInventoryEntity MaterialBag = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, entity.inventoryID.ID, i);

                                        // Check Item Is Available
                                        if (MaterialBag != null)
                                        {
                                            // Item Equals To Dirt?
                                            if (MaterialBag.itemType.Type == Enums.ItemType.Dirt)
                                            {
                                                // Entity Has Item Stack?
                                                if(MaterialBag.hasItemStack)
                                                {
                                                    // Check Count Of The Item
                                                    if(MaterialBag.itemStack.Count >= 1)
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
                                                Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                                                selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                                                // Create Item
                                                item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);

                                                // Check If Item Is Available
                                                if (item != null)
                                                {
                                                    if (item.itemCastData.data.TileID == TileID.Bedrock)
                                                    {
                                                        // Set Red After Selected
                                                        bedrockUIBackground.SetImageColor(Color.red);
                                                    }
                                                    else
                                                    {
                                                        // Set Yellow After Unselected
                                                        bedrockUIBackground.SetImageColor(Color.yellow);
                                                    }

                                                    if (item.itemCastData.data.TileID == TileID.Moon)
                                                    {
                                                        // Set Red After Selected
                                                        dirtUIBackground.SetImageColor(Color.red);
                                                    }
                                                    else
                                                    {
                                                        // Set Yellow After Unselected
                                                        dirtUIBackground.SetImageColor(Color.yellow);
                                                    }

                                                    if (item.itemCastData.data.TileID == TileID.Pipe)
                                                    {
                                                        // Set Red After Selected
                                                        pipeUIBackground.SetImageColor(Color.red);
                                                    }
                                                    else
                                                    {
                                                        // Set Yellow After Unselected
                                                        pipeUIBackground.SetImageColor(Color.yellow);
                                                    }

                                                    if (item.itemCastData.data.TileID == TileID.Wire)
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
                    else
                    {
                        // If Item Is not equal to any placement tool,
                        // hide all of the widget tiles
                        dirtUIBackground.GetGameObject().SetActive(false);
                        bedrockUIBackground.GetGameObject().SetActive(false);
                        wireUIBackground.GetGameObject().SetActive(false);
                        pipeUIBackground.GetGameObject().SetActive(false);
                    }
                }

                // Handle Inputs
                HandleInputs(agentEntity);
            }
        }

        public virtual void Draw()
        {
            if(Init)
            {
                // Update Elements
                for (int i = 0; i < UIList.Count; i++)
                {
                    UIList[i].Draw();
                }
            }
        }

        public void HandleInputs(AgentEntity agentEntity)
        {
            // On Mouse Click Event
            OnMouseClick(agentEntity);

            // On Mouse Stay Event
            OnMouseStay();
        }

        public void HandleIndicators()
        {
            if(foodBarUI.foodBar._fillValue < 50)
            {
                // TODO: Show Indicator Icon
            }
            else
            {
                // TODO: Don't show indicator Icon
            }

            if (waterBarUI.waterBar._fillValue < 50)
            {
                // TODO: Show Indicator Icon
            }
            else
            {
                // TODO: Don't show indicator Icon
            }
        }

        public virtual void OnMouseClick(AgentEntity agentEntity)
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // Check The Distance Betweeen Cursor And Object
                if (Vec2f.Distance(new Vec2f(CursorPosition.X, CursorPosition.Y), new Vec2f(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                {
                    // If Mosue 0 Button Down
                    if (Input.GetMouseButton(0))
                    {
                        // Call All Click Events
                        UIList[i].OnMouseClick(agentEntity);
                    }
                }
            }

            // If Mosue 0 Button Down
            if (Input.GetMouseButton(0))
            {
                if (bedrockUIBackground.IsMouseOver(CursorPosition) && bedrockUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);
                    if(item != null)
                    {
                        if (item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Bedrock;
                        }
                        else if (item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Bedrock;
                        }
                    }
                }
                if (dirtUIBackground.IsMouseOver(CursorPosition) && dirtUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);
                    if (item != null)
                    {
                        if (item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Moon;
                        }
                        else if (item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Moon;
                        }
                    }
                }
                if (pipeUIBackground.IsMouseOver(CursorPosition) && pipeUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);
                    if (item != null)
                    {
                        if (item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Pipe;
                        }
                        else if (item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Pipe;
                        }
                    }
                }
                if (wireUIBackground.IsMouseOver(CursorPosition) && wireUIBackground.GetGameObject().active)
                {
                    // Set Inventory Elements
                    inventoryID = agentEntity.agentInventory.InventoryID;
                    Inventory = _planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                    selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                    // Create Item
                    item = GameState.InventoryManager.GetItemInSlot(_planet.EntitasContext, inventoryID, selectedSlot);
                    if (item != null)
                    {
                        if (item.itemType.Type == Enums.ItemType.PlacementTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Wire;
                        }
                        else if (item.itemType.Type == Enums.ItemType.PlacementMaterialTool)
                        {
                            // Set Data Tile ID to Pipe
                            item.itemCastData.data.TileID = TileID.Wire;
                        }
                    }
                }
            }
        }

        public virtual void OnMouseEnter()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // If Condition Is True
                if(UIList[i].CanRun)
                {
                    // Set Condition Bool
                    UIList[i].CanRun = false;

                    // If Cursor Is In UI Element
                    if (Vec2f.Distance(new Vec2f(CursorPosition.X, CursorPosition.Y), new Vec2f(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                    {
                        // Run Mouse Enter Event
                        UIList[i].OnMouseEnter();
                    }
                }
            }
        }

        public virtual void OnMouseStay()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // Check If Cursor Is On UI Element
                if (Vec2f.Distance(new Vec2f(CursorPosition.X, CursorPosition.Y), new Vec2f(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                {
                    // Run Mouse Enter Event
                    OnMouseEnter();

                    // Call Mouse Stay Event For Each
                    UIList[i].OnMouseStay();
                }
                else
                {
                    // Set Condition to True
                    UIList[i].CanRun = true;

                    OnMouseExit();
                }
            }
        }

        public virtual void OnMouseExit()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // On Mouse Exit
                UIList[i].OnMouseExit();
            }
        }

        public void AddScannerText(string _text, Vec2f canvasPosition, Vec2f hudSize, float lifeTime)
        {
            scannerText.Create("TempText", _text, _Canvas.transform, lifeTime);
            scannerText.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            scannerText.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));
            scannerText.startLifeTime = true;
        }

        public Text AddText(string _text, Vec2f canvasPosition, Vec2f hudSize)
        {
            Text text = new Text();
            text.Create("TempText", _text, _Canvas.transform, 1);
            text.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            text.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));

            return text;
        }

        public void Teardown()
        {
            
        }
    }
}
