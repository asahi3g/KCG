using UnityEngine;
using KMath;
using Enums;
using Utility;

namespace Mech
{

    public class MechGUIDrawSystem
    {
        private ImageWrapper Chest;
        private ImageWrapper ChestBackground;

        private ImageWrapper Planter;
        private ImageWrapper PlanterBackground;

        private ImageWrapper Light;
        private ImageWrapper LightBackground;

        private ImageWrapper MajestyPalm;
        private ImageWrapper MajestyPalmBackground;

        private ImageWrapper SagoPalm;
        private ImageWrapper SagoPalmBackground;

        private ImageWrapper DracaenaTrifasciata;
        private ImageWrapper DracaenaTrifasciataBackground;

        private ImageWrapper SmashableBox;
        private ImageWrapper SmashableBoxBackground;
        
        private ImageWrapper SmashableEgg;
        private ImageWrapper SmashableEggBackground;

        int inventoryID;
        InventoryEntity Inventory;
        int selectedSlot;
        ItemInventoryEntity item;

        public void Initialize(ref Planet.PlanetState planet)
        {
            //var backgroundSprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");


            /*ChestBackground = planet.AddUIImage("ChestBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            Chest = planet.AddUIImage("Chest", ChestBackground.Transform, "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.ImageWrapper;

            PlanterBackground = planet.AddUIImage("PlanterBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            Planter = planet.AddUIImage("Planter", PlanterBackground.Transform, "Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f),  32, 16).kGUIElementsImage.ImageWrapper;

            LightBackground = planet.AddUIImage("LightBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            Light = planet.AddUIImage("Light", LightBackground.Transform, "Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 48, 16).kGUIElementsImage.ImageWrapper;

            MajestyPalmBackground = planet.AddUIImage("MajestyPalmBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            MajestyPalm = planet.AddUIImage("MajestyPalm", MajestyPalmBackground.Transform,
                "Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.ImageWrapper;

            SagoPalmBackground = planet.AddUIImage("SagoPalmBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            SagoPalm = planet.AddUIImage("SagoPalm", SagoPalmBackground.Transform,
                "Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.ImageWrapper;

            DracaenaTrifasciataBackground = planet.AddUIImage("DracaenaTrifasciataBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            DracaenaTrifasciata = planet.AddUIImage("DracaenaTrifasciata", DracaenaTrifasciataBackground.Transform,
                "Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 16, 16).kGUIElementsImage.ImageWrapper;

            SmashableBoxBackground = planet.AddUIImage("SmashableBoxBackground", canvas, backgroundSprite,
                    new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;

            SmashableBox = planet.AddUIImage("SmashableBox", SmashableBoxBackground.Transform,
                "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.ImageWrapper;
            
            SmashableEggBackground = planet.AddUIImage("SmashableEggBackground", canvas, backgroundSprite,
                new Vec2f(positionX, positionY), new Vec3f(0.7f, 0.7f, 0.7f), new Vec2f(50f, 50f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.ImageWrapper;
            positionX += differenceX;
            
            SmashableEgg = planet.AddUIImage("SmashableEgg", SmashableEggBackground.Transform,
                "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), new Vec2f(50f, 50f), 32, 32).kGUIElementsImage.ImageWrapper;*/
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
                    ChestBackground.GameObject.SetActive(true);
                    PlanterBackground.GameObject.SetActive(true);
                    LightBackground.GameObject.SetActive(true);
                    MajestyPalmBackground.GameObject.SetActive(true);
                    SagoPalmBackground.GameObject.SetActive(true);
                    DracaenaTrifasciataBackground.GameObject.SetActive(true);
                    SmashableBoxBackground.GameObject.SetActive(true);
                    SmashableEggBackground.GameObject.SetActive(true);

                    ChestBackground.SetImageColor(item.itemMech.MechID == MechType.Storage ? Color.red : Color.yellow);
                    PlanterBackground.SetImageColor(item.itemMech.MechID == MechType.Planter ? Color.red : Color.yellow);
                    LightBackground.SetImageColor(item.itemMech.MechID == MechType.Light ? Color.red : Color.yellow);
                    MajestyPalmBackground.SetImageColor(item.itemMech.MechID == MechType.MajestyPalm ? Color.red : Color.yellow);
                    SagoPalmBackground.SetImageColor(item.itemMech.MechID == MechType.SagoPalm ? Color.red : Color.yellow);
                    DracaenaTrifasciataBackground.SetImageColor(item.itemMech.MechID == MechType.DracaenaTrifasciata ? Color.red : Color.yellow);
                    SmashableBoxBackground.SetImageColor(item.itemMech.MechID == MechType.SmashableBox ? Color.red : Color.yellow);
                    SmashableEggBackground.SetImageColor(item.itemMech.MechID == MechType.SmashableEgg ? Color.red : Color.yellow);

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
                                    item.itemMech.MechID = Enums.MechType.DracaenaTrifasciata;
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
                    ChestBackground.GameObject.SetActive(false);
                    PlanterBackground.GameObject.SetActive(false);
                    LightBackground.GameObject.SetActive(false);
                    MajestyPalmBackground.GameObject.SetActive(false);
                    SagoPalmBackground.GameObject.SetActive(false);
                    DracaenaTrifasciataBackground.GameObject.SetActive(false);
                    SmashableBoxBackground.GameObject.SetActive(false);
                    SmashableEggBackground.GameObject.SetActive(false);
                }
            }
        }
    }
}