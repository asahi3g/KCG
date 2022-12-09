//import UntiyEngine

using System;

public class InventoryTest : UnityEngine.MonoBehaviour
{
    Inventory.InventoryManager inventoryManager;
    Item.SpawnerSystem      itemSpawnSystem;
    Inventory.InventoryList inventoryList;
    int terrariaLikeInventoryID = 2;
    int terrariaLikeInventoryModelID;
    int customRestrictionInventoryID = 3;
    int customRestrictionInventoryModelID;
    InventoryEntity materialBag;
    int defaultInventoryID = 0;
    int defaultEquipmentID = 1;

    bool Init = false;

    Func<int, InventoryEntity> GetInventory = Contexts.sharedInstance.inventory.GetEntityWithInventoryID;

    [UnityEngine.SerializeField] UnityEngine.Material material;

    public void Start()
    {
        Initialize();
        inventoryManager = new Inventory.InventoryManager();
        itemSpawnSystem = new Item.SpawnerSystem();
        inventoryList = new Inventory.InventoryList();
        GameState.Renderer.Initialize(material);

        int inventoryID = 0;
        int equipmentInventoryID = 1;
        inventoryList.Add(GameState.InventoryManager.CreateDefaultInventory());
        inventoryList.Add(GameState.InventoryManager.CreateInventory(GameState.InventoryCreationApi.GetDefaultRestrictionInventoryModelID()));
        materialBag = inventoryList.Add(GameState.InventoryManager.CreateInventory(GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID()));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(terrariaLikeInventoryModelID));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(customRestrictionInventoryModelID));


        inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Helmet), inventoryID);
        inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Suit), inventoryID);


        // Test not stackable items.
        for (int i = 0; i < 10; i++)
        {
            inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Pistol + i), inventoryID);
            inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Pistol + 4 + i), terrariaLikeInventoryID);
        }

        // Testing stackable items.
        for (uint i = 0; i < 256; i++)
        {
            inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Ore), inventoryID);
            inventoryManager.AddItem(itemSpawnSystem.SpawnInventoryItem(Enums.ItemType.Ore), materialBag.inventoryID.ID);
        }

        Init = true;
    }

    public void Update()
    {
        // check if the sprite atlas textures needs to be updated
        GameState.SpriteAtlasManager.UpdateAtlasTextures();

        // check if the tile sprite atlas textures needs to be updated
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
    }

    private void Initialize()
    {
        GameState.InventoryCreationApi.InitStage1();
        GameState.InventoryCreationApi.InitStage2();

        terrariaLikeInventoryModelID = GameState.InventoryCreationApi.Create();
        GameState.InventoryCreationApi.SetAllSlotsAsActive();
        GameState.InventoryCreationApi.SetToolBar();
        GameState.InventoryCreationApi.SetSize(10, 5);
        GameState.InventoryCreationApi.SetInventoryPos(50, 680);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new UnityEngine.Color(0.29f, 0.31f, 0.59f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new UnityEngine.Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();

        customRestrictionInventoryModelID = GameState.InventoryCreationApi.Create();
        GameState.InventoryCreationApi.SetActiveSlot(1);
        GameState.InventoryCreationApi.SetActiveSlot(3);
        GameState.InventoryCreationApi.SetActiveSlot(4);
        GameState.InventoryCreationApi.SetActiveSlot(5);
        GameState.InventoryCreationApi.SetActiveSlot(7);
        GameState.InventoryCreationApi.SetDefaultRestrictionTexture();
        GameState.InventoryCreationApi.SetRestriction(1, Enums.ItemGroupType.Helmet);
        GameState.InventoryCreationApi.SetRestriction(3, Enums.ItemGroupType.Ring);
        GameState.InventoryCreationApi.SetRestriction(4, Enums.ItemGroupType.Armour);
        GameState.InventoryCreationApi.SetRestriction(5, Enums.ItemGroupType.Gloves);
        GameState.InventoryCreationApi.SetRestriction(7, Enums.ItemGroupType.Belt);
        GameState.InventoryCreationApi.SetSize(3, 3);
        GameState.InventoryCreationApi.SetInventoryPos(1_650f, 430f);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new UnityEngine.Color(0.0f, 0.70f, 0.55f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new UnityEngine.Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();
    }
}
