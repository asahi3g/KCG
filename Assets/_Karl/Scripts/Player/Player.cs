using Agent;
using Inventory;
using UnityEngine;

public class Player : BaseMonoBehaviour
{
    [SerializeField] private Identifier _identifier;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerCamera _camera;
    
    private AgentRenderer _currentPlayer;
    
    public readonly AgentRenderer.Event onPlayerAgentCreated = new AgentRenderer.Event();

    public PlayerInput GetInput() => _input;
    public PlayerCamera GetCamera() => _camera;

    public void SetAgentRenderer(AgentRenderer agentRenderer)
    {
        ClearAgentRenderer();
        
        _currentPlayer = agentRenderer;
        
        // Set player camera new target
        App.Instance.GetPlayer().GetCamera().SetTarget(_currentPlayer == null ? null : _currentPlayer.transform, true);
        

        if (_currentPlayer != null)
        {
            AgentEntity agentEntity = agentRenderer.GetAgent();
            
            // Setup inventory
            if (agentEntity.hasAgentInventory)
            {
                int inventoryID = agentEntity.agentInventory.InventoryID;
                InventoryEntityComponent inventoryEntityComponent = GameState.Planet.GetInventoryEntityComponent(inventoryID);
                
                // Add some test items
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Pistol);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SMG);
                
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.PlacementTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveTileTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemyGunnerTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.SpawnEnemySwordmanTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConstructionTool);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GeometryPlacementTool);
                
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPotion, 5);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
                
                UIViewInventory inventory = App.Instance.GetUI().GetView<UIViewInventory>();
                inventory.SetInventoryEntityComponent(inventoryEntityComponent);
                inventory.GetSelection().onSelectWithPrevious.AddListener(OnInventorySelectionEvent);

                SetViewInventoryVisibility(true);
                
                Debug.Log("Agent Renderer Set");
            }
            else Debug.LogWarning("Player has no inventory");
            
            // Set stats
            if (agentEntity.hasAgentStats)
            {
                UIViewStats stats = App.Instance.GetUI().GetView<UIViewStats>();
                stats.SetStats(agentEntity.agentStats);
                
                SetViewStatsVisibility(true);
            }
            else Debug.LogWarning("Player has no stats");
        }

        onPlayerAgentCreated.Invoke(_currentPlayer);
    }

    private void SetViewStatsVisibility(bool isVisible)
    {
        App.Instance.GetUI().GetView<UIViewStats>().GetGroup().GetIdentifier().Alter(_identifier, isVisible);
    }
    
    private void SetViewInventoryVisibility(bool isVisible)
    {
        App.Instance.GetUI().GetView<UIViewInventory>().GetGroup().GetIdentifier().Alter(_identifier, isVisible);
    }

    public void ClearAgentRenderer()
    {
        if (_currentPlayer == null) return;
        _currentPlayer = null;
        
        // Clear camera target
        App.Instance.GetPlayer().GetCamera().ClearTarget();
        
        // Unsubscribe inventory
        UIViewInventory inventory = App.Instance.GetUI().GetView<UIViewInventory>();
        inventory.GetSelection().onSelectWithPrevious.RemoveListener(OnInventorySelectionEvent);
        inventory.Clear();
        
        // Hide views
        SetViewStatsVisibility(false);
        SetViewInventoryVisibility(false);
    }
    
    public bool GetCurrentPlayerAgent(out AgentRenderer character)
    {
        character = _currentPlayer;
        return character != null;
    }

    private void OnInventorySelectionEvent(UIContentElement previous, UIContentElement selected)
    {
        if (GetCurrentPlayerAgent(out AgentRenderer character))
        {
            UIContentElementInventorySlot slot = (UIContentElementInventorySlot)selected;

            // Slot unselected
            if (slot == null)
            {
                character.GetAgent().SetModel3DWeapon(Model3DWeaponType.None);
                return;
            }

            // Some slot selected
            if (slot.GetItem(out ItemInventoryEntity itemInventoryEntity))
            {
                character.GetAgent().SetModel3DWeapon(itemInventoryEntity.itemType.Type);
            }
            else
            {
                character.GetAgent().SetModel3DWeapon(Model3DWeaponType.None);
            }
        }
    }
}
