using UnityEngine;
using System.Collections.Generic;
using KMath;
using Entitas;
using Item;
using System.Linq;
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

        int inventoryID;
        InventoryEntity Inventory;
        int selectedSlot;
        ItemInventoryEntity item;

        public void Initialize(ref Planet.PlanetState planet)
        {
            ChestBackground = planet.AddUIImage("ChestBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(-280.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            Chest = planet.AddUIImage("Chest", ChestBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 32, 32).kGUIElementsImage.Image;

            PlanterBackground = planet.AddUIImage("PlanterBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(-200.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            Planter = planet.AddUIImage("Planter", PlanterBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 32, 16).kGUIElementsImage.Image;

            LightBackground = planet.AddUIImage("LightBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(-120.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            Light = planet.AddUIImage("Light", LightBackground.GetTransform(), "Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 48, 16).kGUIElementsImage.Image;

            MajestyPalmBackground = planet.AddUIImage("MajestyPalmBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(-38.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            MajestyPalm = planet.AddUIImage("MajestyPalm", MajestyPalmBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            SagoPalmBackground = planet.AddUIImage("SagoPalmBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(40.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            SagoPalm = planet.AddUIImage("SagoPalm", SagoPalmBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            DracaenaTrifasciataBackground = planet.AddUIImage("DracaenaTrifasciataBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(120.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            DracaenaTrifasciata = planet.AddUIImage("DracaenaTrifasciata", DracaenaTrifasciataBackground.GetTransform(),
                "Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 16, 16).kGUIElementsImage.Image;

            SmashableBoxBackground = planet.AddUIImage("SmashableBoxBackground", GameObject.Find("Canvas").transform,
                UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd"),
                    new Vec2f(200.0f, -80.2f), new Vec3f(0.7f, 0.7f, 0.7f), UnityEngine.UI.Image.Type.Tiled, Color.yellow).kGUIElementsImage.Image;

            SmashableBox = planet.AddUIImage("SmashableBox", SmashableBoxBackground.GetTransform(),
                "Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png",
                    new Vec2f(0.0f, 0.0f), new Vec3f(0.8f, -0.8f, 0.8f), 32, 32).kGUIElementsImage.Image;
        }

        public void Draw(ref Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Set Inventory Elements
            inventoryID = agentEntity.agentInventory.InventoryID;
            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
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

                    if(item.itemMechCastData.data.MechID == MechType.Storage)
                    {
                        ChestBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        ChestBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.Planter)
                    {
                        PlanterBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        PlanterBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.Light)
                    {
                        LightBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        LightBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.MajestyPalm)
                    {
                        MajestyPalmBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        MajestyPalmBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.SagoPalm)
                    {
                        SagoPalmBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        SagoPalmBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.DracaenaTrifasciata)
                    {
                        DracaenaTrifasciataBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        DracaenaTrifasciataBackground.SetImageColor(Color.yellow);
                    }

                    if (item.itemMechCastData.data.MechID == MechType.SmashableBox)
                    {
                        SmashableBoxBackground.SetImageColor(Color.red);
                    }
                    else
                    {
                        SmashableBoxBackground.SetImageColor(Color.yellow);
                    }

                    if (ChestBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && ChestBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (PlanterBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && PlanterBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (LightBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && LightBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (MajestyPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && MajestyPalmBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (SagoPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && SagoPalmBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (DracaenaTrifasciataBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && DracaenaTrifasciataBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else if (SmashableBoxBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && SmashableBoxBackground.GetGameObject().active)
                    {
                        item.itemMechCastData.InputsActive = false;
                    }
                    else
                    {
                        item.itemMechCastData.InputsActive = true;

                    }

                    if (Input.GetMouseButton(0))
                    {
                        if(ChestBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && ChestBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.Storage;
                                }
                        }
                        
                        if (PlanterBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && PlanterBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.Planter;
                                }
                        }

                        if (LightBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && LightBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.Light;
                                }

                        }

                        if (MajestyPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && MajestyPalmBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.MajestyPalm;
                                }
                        }

                        if (SagoPalmBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && SagoPalmBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.SagoPalm;
                                }
                        }

                        if (DracaenaTrifasciataBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && DracaenaTrifasciataBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.DracaenaTrifasciata;
                                }
                        }

                        if (SmashableBoxBackground.IsMouseOver(new Vec2f(Input.mousePosition.x, Input.mousePosition.y)) && SmashableBoxBackground.GetGameObject().active)
                        {
                            // Set Inventory Elements
                            inventoryID = agentEntity.agentInventory.InventoryID;
                            Inventory = planet.EntitasContext.inventory.GetEntityWithInventoryIDID(inventoryID);
                            selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

                            // Create Item
                            item = GameState.InventoryManager.GetItemInSlot(planet.EntitasContext, inventoryID, selectedSlot);
                            if (item != null)
                                if (item.itemType.Type == Enums.ItemType.ConstructionTool)
                                {
                                    // Set Data Tile ID to Pipe
                                    item.itemMechCastData.data.MechID = MechType.SmashableBox;
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
                }
            }
        }
    }
}