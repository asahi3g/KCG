using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryTest : MonoBehaviour
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

    bool Init = false;

    [SerializeField] Material material;

    public void Start()
    {
        Initialize();

        context = Contexts.sharedInstance;
        inventoryManager = new Inventory.InventoryManager();
        itemSpawnSystem = new Item.SpawnerSystem();
        inventoryDrawSystem = new Inventory.DrawSystem();
        inventoryList = new Inventory.InventoryList();
        GameState.Renderer.Initialize(material);

        // Create Agent and inventory.
        int agnetID = 0;

        int inventoryID = 0;
        int equipmentInventoryID = 1;
        inventoryList.Add(GameState.InventoryManager.CreateDefaultInventory(context, inventoryID));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, 
            GameState.InventoryCreationApi.GetDefaultRestrictionInventoryModelID(), equipmentInventoryID));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, terrariaLikeInventoryModelID, terrariaLikeInventoryID));
        inventoryList.Add(GameState.InventoryManager.CreateInventory(context, customRestrictionInventoryModelID, customRestrictionInventoryID));

        AgentEntity playerEntity = context.agent.CreateEntity();
        playerEntity.AddAgentID(agnetID);
        playerEntity.isAgentPlayer = true;
        playerEntity.AddAgentInventory(inventoryID, equipmentInventoryID, false);

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
        }

        // Set basic inventory draw to on at the beggining:
        inventoryList.Get(inventoryID).hasInventoryToolBarDraw = true;
        inventoryList.Get(inventoryID).hasInventoryDraw = true;
        inventoryList.Get(equipmentInventoryID).hasInventoryDraw = true;
        // Set Draw to false for custom inventories.
        inventoryList.Get(terrariaLikeInventoryID).hasInventoryToolBarDraw = false;
        inventoryList.Get(customRestrictionInventoryID).hasInventoryDraw = false;

        Init = true;
    }

    public void Update()
    {
        // check if the sprite atlas textures needs to be updated
        for(int type = 0; type < GameState.SpriteAtlasManager.Length; type++)
        {
            GameState.SpriteAtlasManager.UpdateAtlasTexture(type);
        }

        // check if the tile sprite atlas textures needs to be updated
        for(int type = 0; type < GameState.TileSpriteAtlasManager.Length; type++)
        {
            GameState.TileSpriteAtlasManager.UpdateAtlasTexture(type);
        }

        //  Open Inventory with Tab.        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            var players = context.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPlayer, AgentMatcher.AgentInventory));
            foreach (var player in players)
            {
                int inventoryID = player.agentInventory.InventoryID;
                inventoryList.Get(inventoryID).hasInventoryToolBarDraw = !inventoryList.Get(inventoryID).hasInventoryToolBarDraw;
                inventoryList.Get(inventoryID).hasInventoryDraw = !inventoryList.Get(inventoryID).hasInventoryDraw;

                inventoryID = player.agentInventory.EquipmentInventoryID;
                inventoryList.Get(inventoryID).hasInventoryDraw = !inventoryList.Get(inventoryID).hasInventoryDraw;

                inventoryList.Get(terrariaLikeInventoryID).hasInventoryToolBarDraw = !inventoryList.Get(terrariaLikeInventoryID).hasInventoryToolBarDraw;
                inventoryList.Get(terrariaLikeInventoryID).hasInventoryDraw = !inventoryList.Get(terrariaLikeInventoryID).hasInventoryDraw;

                inventoryList.Get(customRestrictionInventoryID).hasInventoryDraw = !inventoryList.Get(customRestrictionInventoryID).hasInventoryDraw;
            }
        }
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        if (Event.current.type == EventType.MouseDown)
            GameState.InventoryMouseSelectionSystem.OnMouseDown(Contexts.sharedInstance, inventoryList);

        if (Event.current.type == EventType.MouseUp)
            GameState.InventoryMouseSelectionSystem.OnMouseUP(Contexts.sharedInstance, inventoryList);

        if (Event.current.type != EventType.Repaint)
            return;

        GameState.InventoryMouseSelectionSystem.Update(Contexts.sharedInstance);

        inventoryDrawSystem.Draw(Contexts.sharedInstance, inventoryList);
    }

    private void Initialize()
    {
        GameResources.Initialize();
        GameState.InventoryCreationApi.Initialize();

        terrariaLikeInventoryModelID = GameState.InventoryCreationApi.Create();
        GameState.InventoryCreationApi.SetAllSlotsAsActive();
        GameState.InventoryCreationApi.SetToolBar();
        GameState.InventoryCreationApi.SetSize(10, 5);
        GameState.InventoryCreationApi.SetInventoryPos(50, 680);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new Color(0.29f, 0.31f, 0.59f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new Color(1f, 0.92f, 0.016f, 0.75f));
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
        GameState.InventoryCreationApi.SetDefaultSlotColor(new Color(0.0f, 0.70f, 0.55f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();
    }
}
