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
    bool Init = false;

    [SerializeField] Material material;

    public void Start()
    {
        Initialize();

        context = Contexts.sharedInstance;
        inventoryManager = new Inventory.InventoryManager();
        itemSpawnSystem = new Item.SpawnerSystem();
        inventoryDrawSystem = new Inventory.DrawSystem();
        GameState.Renderer.Initialize(material);

        // Create Agent and inventory.
        int agnetID = 0;
        int inventoryWidth = 8;
        int inventoryHeight = 6;

        AgentEntity playerEntity = context.agent.CreateEntity();
        playerEntity.AddAgentID(agnetID);
        playerEntity.isAgentPlayer = true;
        playerEntity.AddAgentInventory(GameState.InventoryCreationApi.CreateDefaultInventory(),
            GameState.InventoryCreationApi.CreateDefaultRestrictionInventory());
        int inventoryID = playerEntity.agentInventory.InventoryID;

        // Add item to tool bar.
         inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol), inventoryID);

        // Test not stackable items.
        for (uint i = 0; i < 10; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol), inventoryID);
        }

        // Testing stackable items.
        for (uint i = 0; i < 10; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Rock), inventoryID);
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.RockDust), inventoryID);
        }
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
                ref Inventory.InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
                inventory.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw; // Toggling the bit

                inventoryID = player.agentInventory.EquipmentInventoryID;
                ref Inventory.InventoryModel inventoryEquipment = ref GameState.InventoryCreationApi.Get(inventoryID);
                inventoryEquipment.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw; // Toggling the bit
            }
        }
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        if (Event.current.type == EventType.MouseDown)
            GameState.InventoryMouseSelectionSystem.OnMouseDown(Contexts.sharedInstance);

        if (Event.current.type == EventType.MouseUp)
            GameState.InventoryMouseSelectionSystem.OnMouseUP(Contexts.sharedInstance);

        if (Event.current.type != EventType.Repaint)
            return;

        inventoryDrawSystem.Draw(Contexts.sharedInstance);
        GameState.InventoryMouseSelectionSystem.Draw(Contexts.sharedInstance);
    }

    private void Initialize()
    {
        int GunSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Pistol\\gun-temp.png", 44, 25);
        int RockSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\MaterialIcons\\Rock\\rock1.png", 16, 16);
        int RockDustSpriteSheet =
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Rock\\rock1_dust.png", 16, 16);

        int GunIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(GunSpriteSheet, 0, 0, Enums.AtlasType.Particle);
        int RockIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RockSpriteSheet, 0, 0, Enums.AtlasType.Particle);
        int RockDustIcon = GameState.SpriteAtlasManager.CopySpriteToAtlas(RockDustSpriteSheet, 0, 0, Enums.AtlasType.Particle);

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Pistol, "Pistol");
        GameState.ItemCreationApi.SetTexture(GunIcon);
        GameState.ItemCreationApi.SetInventoryTexture(GunIcon);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.Rock, "Rock");
        GameState.ItemCreationApi.SetTexture(RockIcon);
        GameState.ItemCreationApi.SetInventoryTexture(RockIcon);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();

        GameState.ItemCreationApi.CreateItem(Enums.ItemType.RockDust, "RockDust");
        GameState.ItemCreationApi.SetTexture(RockDustIcon);
        GameState.ItemCreationApi.SetInventoryTexture(RockDustIcon);
        GameState.ItemCreationApi.SetStackable(99);
        GameState.ItemCreationApi.EndItem();
    }
}
