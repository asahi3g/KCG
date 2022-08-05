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
    int terrariaLikeInventoryID;
    int customRestrictionInventoryID;

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

        AgentEntity playerEntity = context.agent.CreateEntity();
        playerEntity.AddAgentID(agnetID);
        playerEntity.isAgentPlayer = true;
        playerEntity.AddAgentInventory(GameState.InventoryCreationApi.CreateDefaultInventory(),
            GameState.InventoryCreationApi.CreateDefaultRestrictionInventory());
        int inventoryID = playerEntity.agentInventory.InventoryID;

        inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Helmet), inventoryID);
        inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Suit), inventoryID);


        // Test not stackable items.
        for (int i = 0; i < 10; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol + i), inventoryID);
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Pistol+4 + i), terrariaLikeInventoryID);
        }

        // Testing stackable items.
        for (uint i = 0; i < 256; i++)
        {
            inventoryManager.AddItem(context, itemSpawnSystem.SpawnInventoryItem(context, Enums.ItemType.Ore), inventoryID);
        }
        Init = true;

        // Set basic inventory draw to on at the beggining:
        ref Inventory.InventoryModel inventory = ref GameState.InventoryCreationApi.Get(inventoryID);
        inventory.InventoryFlags |= Inventory.InventoryModel.Flags.Draw;
        inventory.InventoryFlags |= Inventory.InventoryModel.Flags.DrawToolBar; // Toggling the bit

        inventoryID = playerEntity.agentInventory.EquipmentInventoryID;
        ref Inventory.InventoryModel secondInventory = ref GameState.InventoryCreationApi.Get(inventoryID);
        secondInventory.InventoryFlags |= Inventory.InventoryModel.Flags.Draw;
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
                inventory.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw;        // Toggling the bit
                inventory.InventoryFlags ^= Inventory.InventoryModel.Flags.DrawToolBar; // Toggling the bit

                inventoryID = player.agentInventory.EquipmentInventoryID;
                ref Inventory.InventoryModel inventoryEquipment = ref GameState.InventoryCreationApi.Get(inventoryID);
                inventoryEquipment.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw; // Toggling the bit

                ref Inventory.InventoryModel terrariaLikeInventory = ref GameState.InventoryCreationApi.Get(terrariaLikeInventoryID);
                terrariaLikeInventory.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw; // Toggling the bit
                terrariaLikeInventory.InventoryFlags ^= Inventory.InventoryModel.Flags.DrawToolBar; // Toggling the bit

                ref Inventory.InventoryModel cutomEquipmentInventory = ref GameState.InventoryCreationApi.Get(customRestrictionInventoryID);
                cutomEquipmentInventory.InventoryFlags ^= Inventory.InventoryModel.Flags.Draw; // Toggling the bit
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

        GameState.InventoryMouseSelectionSystem.Update(Contexts.sharedInstance);

        inventoryDrawSystem.Draw(Contexts.sharedInstance);
    }

    private void Initialize()
    {
       GameResources.Initialize();

        terrariaLikeInventoryID = GameState.InventoryCreationApi.Create();
        GameState.InventoryCreationApi.SetAllSlotsAsActive();
        GameState.InventoryCreationApi.SetToolBar();
        GameState.InventoryCreationApi.SetSize(10, 5);
        GameState.InventoryCreationApi.SetInventoryPos(50, 680);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new Color(0.29f, 0.31f, 0.59f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();

        ref Inventory.InventoryModel inventory = ref GameState.InventoryCreationApi.Get(terrariaLikeInventoryID);
        inventory.InventoryFlags &= ~Inventory.InventoryModel.Flags.DrawToolBar; // Set Draw toolBar to false.

        customRestrictionInventoryID = GameState.InventoryCreationApi.Create();
        GameState.InventoryCreationApi.SetActiveSlot(1);
        GameState.InventoryCreationApi.SetActiveSlot(3);
        GameState.InventoryCreationApi.SetActiveSlot(4);
        GameState.InventoryCreationApi.SetActiveSlot(5);
        GameState.InventoryCreationApi.SetActiveSlot(7);
        GameState.InventoryCreationApi.SetDefaultRestrictionTexture();
        GameState.InventoryCreationApi.SetRestriction(1, Enums.ItemGroups.HELMET);
        GameState.InventoryCreationApi.SetRestriction(3, Enums.ItemGroups.RING);
        GameState.InventoryCreationApi.SetRestriction(4, Enums.ItemGroups.ARMOUR);
        GameState.InventoryCreationApi.SetRestriction(5, Enums.ItemGroups.GLOVES);
        GameState.InventoryCreationApi.SetRestriction(7, Enums.ItemGroups.BELT);
        GameState.InventoryCreationApi.SetSize(3, 3);
        GameState.InventoryCreationApi.SetInventoryPos(1_650f, 430f);
        GameState.InventoryCreationApi.SetDefaultSlotColor(new Color(0.0f, 0.70f, 0.55f, 0.75f));
        GameState.InventoryCreationApi.SetSelectedtSlotColor(new Color(1f, 0.92f, 0.016f, 0.75f));
        GameState.InventoryCreationApi.SetSlotOffset(5);
        GameState.InventoryCreationApi.SetTileSize(75);
        GameState.InventoryCreationApi.End();
    }
}
