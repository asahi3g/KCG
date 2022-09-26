using UnityEngine;
using KMath;
using KGUI.Elements;

namespace Mech
{

    public class MechGUIDrawSystem
    {
        private Image Chest;
        private Image ChestBackground;

        private Image Planter;
        private Image PlanterBackground;

        private Image Light;
        private Image LightBackground;

        private Image MajestyPalm;
        private Image MajestyPalmBackground;

        private Image SagoPalm;
        private Image SagoPalmBackground;

        private Image DracaenaTrifasciata;
        private Image DracaenaTrifasciataBackground;

        private Image SmashableBox;
        private Image SmashableBoxBackground;
        
        private Image SmashableEgg;
        private Image SmashableEggBackground;

        int inventoryID;
        InventoryEntity Inventory;
        int selectedSlot;
        ItemInventoryEntity item;

        public void Initialize(ref Planet.PlanetState planet)
        {
            var canvas = GameObject.Find("Canvas").transform;
            var backgroundSprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            var positionX = -130f;
            var differenceX = 40;
            var positionY = -220f;


            ChestBackground = planet.AddUIImage("ChestBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            Chest = planet.AddUIImage("Chest", ChestBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.Image;

            PlanterBackground = planet.AddUIImage("PlanterBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            Planter = planet.AddUIImage("Planter", PlanterBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f),  32, 16).kGUIElementsImage.Image;

            LightBackground = planet.AddUIImage("LightBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            Light = planet.AddUIImage("Light", LightBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 48, 16).kGUIElementsImage.Image;

            MajestyPalmBackground = planet.AddUIImage("MajestyPalmBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            MajestyPalm = planet.AddUIImage("MajestyPalm", MajestyPalmBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.Image;

            SagoPalmBackground = planet.AddUIImage("SagoPalmBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            SagoPalm = planet.AddUIImage("SagoPalm", SagoPalmBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.Image;

            DracaenaTrifasciataBackground = planet.AddUIImage("DracaenaTrifasciataBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            DracaenaTrifasciata = planet.AddUIImage("DracaenaTrifasciata", DracaenaTrifasciataBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.Image;

            SmashableBoxBackground = planet.AddUIImage("SmashableBoxBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;

            SmashableBox = planet.AddUIImage("SmashableBox", SmashableBoxBackground.GetTransform(),
                "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.Image;
            
            SmashableEggBackground = planet.AddUIImage("SmashableEggBackground", canvas, backgroundSprite,
                new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;
            positionX += differenceX;
            
            SmashableEgg = planet.AddUIImage("SmashableEgg", SmashableEggBackground.GetTransform(),
                "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.Image;
        }

        public void Draw(ref Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Set Inventory Elements
            inventoryID = agentEntity.agentInventory.InventoryID;
            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

            // Create Item
            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
            if (item != null)
            {
                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                {
                    ChestBackground.GetGameObject().SetActive(true);
                    PlanterBackground.GetGameObject().SetActive(true);
                    LightBackground.GetGameObject().SetActive(true);
                    MajestyPalmBackground.GetGameObject().SetActive(true);
                    SagoPalmBackground.GetGameObject().SetActive(true);
                    DracaenaTrifasciataBackground.GetGameObject().SetActive(true);
                    SmashableBoxBackground.GetGameObject().SetActive(true);
                    SmashableEggBackground.GetGameObject().SetActive(true);

                    ChestBackground.SetImageColor(item.itemMech.MechID 
                                                                == MechType.Storage ? Color.red : Color.yellow);
                    PlanterBackground.SetImageColor(item.itemMech.MechID 
                                                                == MechType.Planter ? Color.red : Color.yellow);
                    LightBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.Light ? Color.red : Color.yellow);
                    MajestyPalmBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.MajestyPalm ? Color.red : Color.yellow);
                    SagoPalmBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.SagoPalm ? Color.red : Color.yellow);
                    DracaenaTrifasciataBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.DracaenaTrifasciata ? Color.red : Color.yellow);
                    SmashableBoxBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.SmashableBox ? Color.red : Color.yellow);
                    SmashableEggBackground.SetImageColor(item.itemMech.MechID
                                                                == MechType.SmashableEgg ? Color.red : Color.yellow);

                    if (ChestBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (PlanterBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (LightBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (MajestyPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (SagoPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (DracaenaTrifasciataBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (SmashableBoxBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else if (SmashableEggBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                    {
                        item.itemMech.InputsActive = false;
                    }
                    else
                    {
                        item.itemMech.InputsActive = true;

                    }

                    if (Input.GetMouseButton(0))
                    {
                        if(ChestBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.Storage;
                                }
                        }
                        
                        if (PlanterBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.Planter;
                                }
                        }

                        if (LightBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.Light;
                                }

                        }

                        if (MajestyPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.MajestyPalm;
                                }
                        }

                        if (SagoPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.SagoPalm;
                                }
                        }

                        if (DracaenaTrifasciataBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.DracaenaTrifasciata;
                                }
                        }

                        if (SmashableBoxBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                            {
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.SmashableBox;
                                }
                            }
                        }
                        
                        if (SmashableEggBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)))
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                            {
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMech.MechID = MechType.SmashableEgg;
                                }
                            }
                        }
                    }

                }
                else
                {
                    ChestBackground.GetGameObject().SetActive(false);
                    PlanterBackground.GetGameObject().SetActive(false);
                    LightBackground.GetGameObject().SetActive(false);
                    MajestyPalmBackground.GetGameObject().SetActive(false);
                    SagoPalmBackground.GetGameObject().SetActive(false);
                    DracaenaTrifasciataBackground.GetGameObject().SetActive(false);
                    SmashableBoxBackground.GetGameObject().SetActive(false);
                    SmashableEggBackground.GetGameObject().SetActive(false);
                }
            }
        }
    }
}