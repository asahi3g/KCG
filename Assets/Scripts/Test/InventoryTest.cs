//import UntiyEngine

using System;

public class InventoryTest : UnityEngine.MonoBehaviour
{
    Contexts context;

    Inventory.InventoryManager inventoryManager;
    Inventory.DrawSystem    inventoryDrawSystem;
    Item.SpawnerSystem      itemSpawnSystem;
    Inventory.MouseSelectionSystem  mouseSelectionSystem;
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
        context = Contexts.sharedInstance;
        inventoryManager = new Inventory.InventoryManager();
        itemSpawnSystem = new Item.SpawnerSystem();
        inventoryDrawSystem = new Inventory.DrawSystem();
        inventoryList = new Inventory.InventoryList();
        GameState.Renderer.Initialize(material);

        int inventoryID = 0;
        int equipmentInventoryID = 1;
        inventoryList.Add(GameState.InventoryManager.CreateDefaultInventory(context));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, GameState.InventoryCreationApi.GetDefaultRestrictionInventoryModelID()));
        materialBag = inventoryList.Add(GameState.InventoryManager.CreateInventory(context, GameState.InventoryCreationApi.GetDefaultMaterialBagInventoryModelID()));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, terrariaLikeInventoryModelID));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, customRestrictionInventoryModelID));


        inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Helmet), inventoryID);
        inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Suit), inventoryID);


        // Test not stackable items.
        for (int i = 0; i < 10; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol + i), inventoryID);
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol + 4 + i), terrariaLikeInventoryID);
        }

        // Testing stackable items.
        for (uint i = 0; i < 256; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Ore), inventoryID);
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Ore), materialBag.inventoryID.ID);
        }

        // Set basic inventory draw to on at the beggining:
        GetInventory(inventoryID).hasInventoryToolBarDraw = true;
        GetInventory(inventoryID).hasInventoryDraw = true;
        GetInventory(equipmentInventoryID).hasInventoryDraw = true;
        // Set Draw to false for custom inventories.
        GetInventory(terrariaLikeInventoryID).hasInventoryToolBarDraw = false;
        GetInventory(customRestrictionInventoryID).hasInventoryDraw = false;
        GetInventory(materialBag.inventoryID.ID).hasInventoryDraw = false;

        Init = true;
    }

    public void Update()
    {
        // check if the sprite atlas textures needs to be updated
        for(int type = 0; type < GameState.SpriteAtlasManager.AtlasArray.Length; type++)
        {
            GameState.SpriteAtlasManager.UpdateAtlasTexture(type);
        }

        // check if the tile sprite atlas textures needs to be updated
        for(int type = 0; type < GameState.TileSpriteAtlasManager.Length; type++)
        {
            GameState.TileSpriteAtlasManager.UpdateAtlasTexture(type);
        }

        //  Open Inventory with Tab.        
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Tab))
        {
            GetInventory(defaultInventoryID).hasInventoryToolBarDraw = !GetInventory(defaultInventoryID).hasInventoryToolBarDraw;
            GetInventory(defaultInventoryID).hasInventoryDraw = !GetInventory(defaultInventoryID).hasInventoryDraw;

            GetInventory(defaultEquipmentID).hasInventoryDraw = !GetInventory(defaultEquipmentID).hasInventoryDraw;

            GetInventory(terrariaLikeInventoryID).hasInventoryToolBarDraw = !GetInventory(terrariaLikeInventoryID).hasInventoryToolBarDraw;
            GetInventory(terrariaLikeInventoryID).hasInventoryDraw = !GetInventory(terrariaLikeInventoryID).hasInventoryDraw;

            GetInventory(customRestrictionInventoryID).hasInventoryDraw = !GetInventory(customRestrictionInventoryID).hasInventoryDraw;
            GetInventory(materialBag.inventoryID.ID).hasInventoryDraw = !GetInventory(materialBag.inventoryID.ID).hasInventoryDraw;
        }
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseDown)
            GameState.InventoryMouseSelectionSystem.OnMouseDown(Contexts.sharedInstance, inventoryList);

        if (UnityEngine.Event.current.type == UnityEngine.EventType.MouseUp)
            GameState.InventoryMouseSelectionSystem.OnMouseUP(Contexts.sharedInstance, inventoryList);

        if (UnityEngine.Event.current.type != UnityEngine.EventType.Repaint)
            return;

        GameState.InventoryMouseSelectionSystem.Update(Contexts.sharedInstance);

        inventoryDrawSystem.Draw(Contexts.sharedInstance, inventoryList);
    }

    private void Initialize()
    {
        GameResources.Initialize();
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
        GameState.InventoryCreationApi.SetRestriction(1, Enums.ItemGroups.Helmet);
        GameState.InventoryCreationApi.SetRestriction(3, Enums.ItemGroups.Ring);
        GameState.InventoryCreationApi.SetRestriction(4, Enums.ItemGroups.Armour);
        GameState.InventoryCreationApi.SetRestriction(5, Enums.ItemGroups.Gloves);
        GameState.InventoryCreationApi.SetRestriction(7, Enums.ItemGroups.Belt);
        GameState.InventoryCreationApi.SetSize(3, 3);
        GameState.InventoryCreationApi.SetInventoryPos(1_650f, 430f);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new UnityEngine.Color(0.0f, 0.70f, 0.55f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new UnityEngine.Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();
    }
}
