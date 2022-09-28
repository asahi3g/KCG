using Enums;
using KMath;
using System;
using System.Collections.Generic;
using UnityEngine;
using PlanetTileMap;

namespace Mech
{
    //https://github.com/kk-digital/kcg/issues/397

    public class MechCreationApi
    {
        public MechProperties[] PropertiesArray;

        public int CurrentIndex;

        private Dictionary<string, int> NameToID;

        public MechCreationApi()
        {
            NameToID = new Dictionary<string, int>();

            PropertiesArray = new MechProperties[1024];

            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new MechProperties();
            }

            CurrentIndex = -1;
        }

        public MechProperties Get(int Id)
        {
            if (Id >= 0 && Id < PropertiesArray.Length)
            {
                return PropertiesArray[Id];
            }

            return new MechProperties();
        }

        public ref MechProperties GetRef(int Id)
        {
            return ref PropertiesArray[Id];
        }

        public MechProperties Get(string name)
        {
            int value;
            bool exists = NameToID.TryGetValue(name, out value);
            if (exists)
            {
                return Get(value);
            }

            return new MechProperties();
        }

        public void Create(int Id)
        {
            while (Id >= PropertiesArray.Length)
            {
                Array.Resize(ref PropertiesArray, PropertiesArray.Length * 2);
            }

            CurrentIndex = Id;
            if (CurrentIndex != -1)
            {
                PropertiesArray[CurrentIndex].MechID = CurrentIndex;
            }
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            if (!NameToID.ContainsKey(name))
            {
                NameToID.Add(name, CurrentIndex);
            }

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetDropTableID(Enums.LootTableType dropTableID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].DropTableID = dropTableID;
            }
        }

        public void SetTexture(int spriteId)
        {
            PropertiesArray[CurrentIndex].SpriteID = spriteId;
        }

        public void SetSpriteSize(Vec2f size)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].SpriteSize = size;
            }
        }

        public void SetAction(Enums.NodeType NodeType)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Action = NodeType;
            }
        }

        public void SetInventory(int inventoryModelID)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].InventoryModelID = inventoryModelID;
                PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.HasInventory;
            }
        }

        public void SetDurability(int durability)
        {
            if (CurrentIndex >= 0 && CurrentIndex < PropertiesArray.Length)
            {
                PropertiesArray[CurrentIndex].Durability = durability;
                PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.IsBreakable;
            }
        }

        public void End()
        {
            CurrentIndex = -1;
        }


        public int ChestSpriteSheet;
        public int ChestIcon;
        public int PotIcon;
        public int Light2Icon;

        public int MajestyPalm;
        public int MajestyPalmS1;
        public int MajestyPalmS2;

        public int SagoPalm;
        public int SagoPalmS1;
        public int SagoPalmS2;

        public int DracaenaTrifasciata;
        public int DracaenaTrifasciataS1;
        public int DracaenaTrifasciataS2;

        public int surveillanceCamera;
        public int roofScreen;

        public int craftingTable;

        public void InitializeResources()
        {
            ChestSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Containers\\Chest\\chest.png", 32, 32);
            PotIcon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Pots\\pot_1.png", 32, 16);
            Light2Icon = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Lights\\Light2\\On\\light_2_on.png", 48, 16);
            MajestyPalm = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3.png", 16, 16);
            SagoPalm = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7.png", 16, 16);
            DracaenaTrifasciata = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6.png", 16, 16);
            MajestyPalmS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3_v1.png", 16, 16);
            MajestyPalmS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\MajestyPalm\\plant_3_v2.png", 16, 32);
            SagoPalmS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7_v1.png", 16, 16);
            SagoPalmS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7_v2.png", 16, 32);
            DracaenaTrifasciataS1 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6_v1.png", 16, 16);
            DracaenaTrifasciataS2 = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\DracaenaTrifasciata\\plant_6_v2.png", 16, 32);
            surveillanceCamera = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Cameras\\Surveillance\\surveillanceCamera.png", 16, 16);
            roofScreen = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Decorations\\RoofScreen\\roofScreen.png", 32, 16);
            craftingTable = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Furnitures\\Tables\\Table\\table10.png", 48, 32);


            ChestIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestSpriteSheet, 0, 0, Enums.AtlasType.Mech);
            PotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIcon, 0, 0, Enums.AtlasType.Mech);
            Light2Icon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2Icon, 0, 0, Enums.AtlasType.Mech);
            MajestyPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalm, 0, 0, Enums.AtlasType.Mech);
            MajestyPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS1, 0, 0, Enums.AtlasType.Mech);
            MajestyPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS2, 0, 0, Enums.AtlasType.Mech);
            SagoPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalm, 0, 0, Enums.AtlasType.Mech);
            SagoPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS1, 0, 0, Enums.AtlasType.Mech);
            SagoPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS2, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciata = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciata, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciataS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS1, 0, 0, Enums.AtlasType.Mech);
            DracaenaTrifasciataS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS2, 0, 0, Enums.AtlasType.Mech);
            surveillanceCamera = GameState.SpriteAtlasManager.CopySpriteToAtlas(surveillanceCamera, 0, 0, Enums.AtlasType.Mech);
            roofScreen = GameState.SpriteAtlasManager.CopySpriteToAtlas(roofScreen, 0, 0, Enums.AtlasType.Mech);
            craftingTable = GameState.SpriteAtlasManager.CopySpriteToAtlas(craftingTable, 0, 0, Enums.AtlasType.Mech);


            GameState.MechCreationApi.Create((int)Mech.MechType.Storage);
            GameState.MechCreationApi.SetName("chest");
            GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.ChestDrop);
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1f, 1.0f));
            GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.Planter);
            GameState.MechCreationApi.SetName("planter");
            GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.PlanterDrop);
            GameState.MechCreationApi.SetTexture(PotIcon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.Light);
            GameState.MechCreationApi.SetName("light");
            GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.LightDrop);
            GameState.MechCreationApi.SetTexture(Light2Icon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.MajestyPalm);
            GameState.MechCreationApi.SetName("majesty");
            GameState.MechCreationApi.SetTexture(MajestyPalm);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.SagoPalm);
            GameState.MechCreationApi.SetName("sago");
            GameState.MechCreationApi.SetTexture(SagoPalm);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.DracaenaTrifasciata);
            GameState.MechCreationApi.SetName("dracaenatrifasciata");
            GameState.MechCreationApi.SetTexture(DracaenaTrifasciata);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.SmashableBox);
            GameState.MechCreationApi.SetName("smashableBox");
            GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.SmashableBoxDrop);
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetAction(Enums.NodeType.OpenChestAction);
            GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
            GameState.MechCreationApi.SetDurability(100);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.SmashableEgg);
            GameState.MechCreationApi.SetName("smashableEgg");
            GameState.MechCreationApi.SetDropTableID(Enums.LootTableType.SmashableEggDrop);
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetDurability(100);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.SurveillanceCamera);
            GameState.MechCreationApi.SetName("SurveillanceCamera");
            GameState.MechCreationApi.SetTexture(surveillanceCamera);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.RoofScreen);
            GameState.MechCreationApi.SetName("RoofScreen");
            GameState.MechCreationApi.SetTexture(roofScreen);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create((int)Mech.MechType.CraftingTable);
            GameState.MechCreationApi.SetName("CraftingTable");
            GameState.MechCreationApi.SetTexture(craftingTable);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();
        }

    }
}