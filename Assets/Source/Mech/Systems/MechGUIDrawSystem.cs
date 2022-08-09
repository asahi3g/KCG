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