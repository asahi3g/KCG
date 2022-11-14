using Enums;
using KMath;
using System;

namespace Mech
{
    //https://github.com/kk-digital/kcg/issues/397

    public class MechCreationApi
    {
        public MechProperties[] PropertiesArray;
        public int CurrentIndex;

        public MechCreationApi()
        {
            PropertiesArray = new MechProperties[Enum.GetNames(typeof(MechType)).Length - 1];

            for (int i = 0; i < PropertiesArray.Length; i++)
            {
                PropertiesArray[i] = new MechProperties();
            }

            CurrentIndex = -1;
        }

        public MechProperties Get(MechType type)
        {
            return PropertiesArray[(int)type];
        }

        public ref MechProperties GetRef(MechType type)
        {
            return ref PropertiesArray[(int)type];
        }

        public void Create(MechType type)
        {
            CurrentIndex = (int)type;
            if (CurrentIndex != -1)
            {
                PropertiesArray[CurrentIndex].Type = type;
                PropertiesArray[CurrentIndex].Action = NodeType.None;
                PropertiesArray[CurrentIndex].Group = MechGroup.None;
            }
        }

        public void SetGroup(MechGroup group)
        {
            PropertiesArray[CurrentIndex].Group = group;
        }

        public void SetName(string name)
        {
            if (CurrentIndex == -1) return;

            PropertiesArray[CurrentIndex].Name = name;
        }

        public void SetDropTableID(int dropTableID)
        {
            PropertiesArray[CurrentIndex].DropTableID = dropTableID;
        }

        public void SetTexture(int spriteId)
        {
            PropertiesArray[CurrentIndex].SpriteID = spriteId;
        }

        public void SetPlantTextures(int stage1SpriteID, int stage2SpriteID = -1, int stage3SpriteID = -1)
        {
            PropertiesArray[CurrentIndex].SpriteID = stage1SpriteID;
            PropertiesArray[CurrentIndex].Stage2Sprite = stage2SpriteID;
            PropertiesArray[CurrentIndex].Stage3Sprite = stage3SpriteID;
        }

        public void SetPlantSizes(Vec2f stage1Size, Vec2f stage2Size, Vec2f stage3Size)
        {
            PropertiesArray[CurrentIndex].SpriteSize = stage1Size;
            PropertiesArray[CurrentIndex].Stage2SpriteSize = stage2Size;
            PropertiesArray[CurrentIndex].Stage3SpriteSize = stage3Size;
        }

        public void SetSpriteSize(Vec2f size)
        {
            PropertiesArray[CurrentIndex].SpriteSize = size;
        }

        public void SetAction(NodeType NodeType)
        {
            PropertiesArray[CurrentIndex].Action = NodeType;
        }

        public void SetInventory(int inventoryModelID)
        {
            PropertiesArray[CurrentIndex].InventoryModelID = inventoryModelID;
            PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.HasInventory;
        }

        public void SetDurability(int durability)
        {
            PropertiesArray[CurrentIndex].Durability = durability;
            PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.IsBreakable;
        }

        public void SetHealth(int health)
        {            
            PropertiesArray[CurrentIndex].TreeHealth = health;
            PropertiesArray[CurrentIndex].MechFlags |= MechProperties.Flags.IsBreakable;
        }

        public void SetTreeSize(int treeSize)
        {
            PropertiesArray[CurrentIndex].TreeSize = treeSize;
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

        public int Tree;

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
            Tree = GameState.SpriteLoader.GetSpriteSheetID("Assets\\Source\\Mech\\Plants\\StagePlants\\SagoPalm\\plant_7_v2.png", 16, 32);


            ChestIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(ChestSpriteSheet, 0, 0, AtlasType.Mech);
            PotIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(PotIcon, 0, 0, AtlasType.Mech);
            Light2Icon = GameState.SpriteAtlasManager.CopySpriteToAtlas(Light2Icon, 0, 0, AtlasType.Mech);
            MajestyPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalm, 0, 0, AtlasType.Mech);
            MajestyPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS1, 0, 0, AtlasType.Mech);
            MajestyPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(MajestyPalmS2, 0, 0, AtlasType.Mech);
            SagoPalm = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalm, 0, 0, AtlasType.Mech);
            SagoPalmS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS1, 0, 0, AtlasType.Mech);
            SagoPalmS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(SagoPalmS2, 0, 0, AtlasType.Mech);
            DracaenaTrifasciata = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciata, 0, 0, AtlasType.Mech);
            DracaenaTrifasciataS1 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS1, 0, 0, AtlasType.Mech);
            DracaenaTrifasciataS2 = GameState.SpriteAtlasManager.CopySpriteToAtlas(DracaenaTrifasciataS2, 0, 0, AtlasType.Mech);
            surveillanceCamera = GameState.SpriteAtlasManager.CopySpriteToAtlas(surveillanceCamera, 0, 0, AtlasType.Mech);
            roofScreen = GameState.SpriteAtlasManager.CopySpriteToAtlas(roofScreen, 0, 0, AtlasType.Mech);
            craftingTable = GameState.SpriteAtlasManager.CopySpriteToAtlas(craftingTable, 0, 0, AtlasType.Mech);
            Tree = GameState.SpriteAtlasManager.CopySpriteToAtlas(Tree, 0, 0, AtlasType.Mech);


            GameState.MechCreationApi.Create(MechType.Storage);
            GameState.MechCreationApi.SetName("chest");
            int lootDropID = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Chest, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.MechCreationApi.SetDropTableID(lootDropID);
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1f, 1.0f));
            GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.Planter);
            GameState.MechCreationApi.SetName("planter");
            lootDropID = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Planter, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End();
            GameState.MechCreationApi.SetDropTableID(lootDropID);
            GameState.MechCreationApi.SetTexture(PotIcon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.Light);
            GameState.MechCreationApi.SetName("light");
            lootDropID = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.Light, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End(); GameState.MechCreationApi.SetTexture(Light2Icon);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.MajestyPalm);
            GameState.MechCreationApi.SetGroup(MechGroup.Plant);
            GameState.MechCreationApi.SetName("majesty");
            GameState.MechCreationApi.SetPlantTextures(MajestyPalm, MajestyPalmS1, MajestyPalmS2);
            GameState.MechCreationApi.SetPlantSizes(new Vec2f(1.5f, 1.5f), new Vec2f(1.5f, 3.0f), new Vec2f(1.5f, 4.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.SagoPalm);
            GameState.MechCreationApi.SetGroup(MechGroup.Plant);
            GameState.MechCreationApi.SetName("sago");
            GameState.MechCreationApi.SetPlantTextures(SagoPalm, SagoPalmS1, SagoPalmS2);
            GameState.MechCreationApi.SetPlantSizes(new Vec2f(1.5f, 1.5f), new Vec2f(1.5f, 3.0f), new Vec2f(1.5f, 4.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.DracaenaTrifasciata);
            GameState.MechCreationApi.SetGroup(MechGroup.Plant);
            GameState.MechCreationApi.SetName("dracaenatrifasciata");
            GameState.MechCreationApi.SetTexture(DracaenaTrifasciata);
            GameState.MechCreationApi.SetPlantTextures(DracaenaTrifasciata, DracaenaTrifasciataS1, DracaenaTrifasciataS2);
            GameState.MechCreationApi.SetPlantSizes(new Vec2f(1.5f, 1.5f), new Vec2f(1.5f, 3.0f), new Vec2f(1.5f, 4.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.SmashableBox);
            GameState.MechCreationApi.SetName("smashableBox");
            lootDropID = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.SmashableBox, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End(); GameState.MechCreationApi.SetTexture(Light2Icon); 
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetAction(NodeType.OpenChestAction);
            GameState.MechCreationApi.SetInventory(GameState.InventoryCreationApi.GetDefaultChestInventoryModelID());
            GameState.MechCreationApi.SetDurability(100);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.SmashableEgg);
            GameState.MechCreationApi.SetName("smashableEgg");
            lootDropID = GameState.LootTableCreationAPI.Create();
            GameState.LootTableCreationAPI.AddItem(ItemType.SmashableEgg, 1);
            GameState.LootTableCreationAPI.SetEntry(1, 100);
            GameState.LootTableCreationAPI.End(); 
            GameState.MechCreationApi.SetTexture(ChestIcon);
            GameState.MechCreationApi.SetDurability(100);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.5f, 1.5f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.SurveillanceCamera);
            GameState.MechCreationApi.SetName("SurveillanceCamera");
            GameState.MechCreationApi.SetTexture(surveillanceCamera);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.RoofScreen);
            GameState.MechCreationApi.SetName("RoofScreen");
            GameState.MechCreationApi.SetTexture(roofScreen);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.CraftingTable);
            GameState.MechCreationApi.SetName("CraftingTable");
            GameState.MechCreationApi.SetTexture(craftingTable);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 1.0f));
            GameState.MechCreationApi.End();

            GameState.MechCreationApi.Create(MechType.Tree);
            GameState.MechCreationApi.SetName("Tree");
            GameState.MechCreationApi.SetTexture(Tree);
            GameState.MechCreationApi.SetHealth(100);
            GameState.MechCreationApi.SetTreeSize(5);
            GameState.MechCreationApi.SetSpriteSize(new Vec2f(1.0f, 3.0f));
            GameState.MechCreationApi.End();
        }
         
    }
}