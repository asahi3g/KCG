using Agent;
using Enums;
using Enums.PlanetTileMap;
using Inventory;
using Planet;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseMonoBehaviour
{
    [SerializeField] private Identifier _identifier;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlanetCamera _camera;

    private PlanetLoader.Result _currentPlanet;
    private AgentRenderer _currentPlayer;
    
    public readonly PlanetCreationEvent onCurrentPlanetChanged = new PlanetCreationEvent();
    public readonly AgentRenderer.Event onPlayerAgentCreated = new AgentRenderer.Event();

    public PlayerInput GetInput() => _input;
    public PlanetCamera GetCamera() => _camera;
    public AgentRenderer GetRenderer() => _currentPlayer;


    public class PlanetCreationEvent : UnityEvent<PlanetLoader.Result>{}
    
    
    private void Update()
    {
        if (_currentPlanet != null)
        {
            UpdateMainGameLoop(Time.deltaTime, Application.targetFrameRate, 30f, _currentPlanet.GetPlanetState());
        }
    }

    public void SetCurrentPlanet(PlanetLoader.Result planetRenderer)
    {
        _currentPlanet = planetRenderer;
        onCurrentPlanetChanged.Invoke(_currentPlanet);
    }
    

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
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Sword);
                 Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.ConcussionGrenade);
                 Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.GoldCoin, 4);
                Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.HealthPotion, 5);
                //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.RemoveMech);
                

                UIViewInventory inventory = App.Instance.GetUI().GetView<UIViewInventory>();
                inventory.SetInventoryEntityComponent(inventoryEntityComponent);
                inventory.GetSelection().onSelectWithPrevious.AddListener(OnInventorySelectionEvent);

                SetViewInventoryVisibility(true);
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
    
    private void UpdateMainGameLoop(float deltaTime, float targetFrameRate, float targetPhysicsRate, PlanetState planetState)
    {
        float frameTime = 1.0f / targetPhysicsRate;

        /*TimeState.Deficit += deltaTime;

        while (TimeState.Deficit >= frameTime)
        {
            TimeState.Deficit -= frameTime;
            // do a server/client tick right here
            {
                TimeState.TickTime++;
            }
        }*/

        PlanetTileMap.TileMapGeometry.BuildGeometry(planetState.TileMap);

        // check if the sprite atlas teSetTilextures needs to be updated
        GameState.SpriteAtlasManager.UpdateAtlasTextures();

        // check if the tile sprite atlas textures needs to be updated
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();

        // calling all the systems we have

        //GameState.InputProcessSystem.Update();
        App.Instance.GetPlayer().GetInput().Tick();
        
        //GameState.InputProcessSystem.Update();

        // Movement Systems
        GameState.AgentIKSystem.Update(planetState.EntitasContext.agent);
        GameState.AgentProcessPhysicalState.Update(frameTime);
        GameState.AgentMovementSystem.Update();
        GameState.AgentAgent3DModelMovementSystem.Update();
        GameState.ItemMovableSystem.Update();
        GameState.VehicleMovementSystem.UpdateEx();
        GameState.PodMovementSystem.UpdateEx();
        GameState.ProjectileMovementSystem.Update();

        GameState.AgentEffectSystem.Update(frameTime);
        GameState.AgentAgent3DModelAnimationSystem.Update();
        GameState.LootDropSystem.Update();
        GameState.FloatingTextUpdateSystem.Update(frameTime);
        GameState.AnimationUpdateSystem.Update(frameTime);
        GameState.ItemPickUpSystem.Update();
        GameState.ActionSchedulerSystem.Update();
        GameState.ActionCoolDownSystem.Update(deltaTime);
        GameState.ParticleEmitterUpdateSystem.Update();
        GameState.ParticleUpdateSystem.Update();
        GameState.ProjectileProcessState.Update();
        GameState.PodAISystem.Update();
        GameState.VehicleAISystem.Update();

        // Collision systems.
        GameState.AgentProcessCollisionSystem.Update(planetState.EntitasContext.agent);
        GameState.ItemProcessCollisionSystem.Update();
        GameState.ParticleProcessCollisionSystem.Update();
        GameState.ProjectileCollisionSystem.UpdateEx(deltaTime);
        GameState.VehicleCollisionSystem.Update();
        GameState.PodCollisionSystem.Update();
        GameState.MechPlantGrowthSystem.Update();

        GameState.AgentProcessState.Update();
        GameState.SensorUpdateSystem.Update();
        GameState.BehaviorTreeUpdateSystem.Update();
        GameState.SquadUpdateSystem.Update();
        GameState.BlackboardUpdatePosition.Update();

        App.Instance.GetPlayer().GetCamera().Tick(deltaTime);

        planetState.TileMap.UpdateTileSprites();

        if (GameState.TGenGrid is {Initialized: true})
        {
            GameState.TGenGrid.Update();
            GameState.TGenRenderMapMesh.UpdateMesh(GameState.TGenGrid);
            GameState.TGenRenderMapMesh.Draw();
        }

        // Update Meshes.
        GameState.TileMapRenderer.UpdateBackLayerMesh();
        GameState.TileMapRenderer.UpdateMidLayerMesh();
        GameState.TileMapRenderer.UpdateFrontLayerMesh();
        GameState.ItemMeshBuilderSystem.UpdateMesh();
        GameState.AgentMeshBuilderSystem.UpdateMesh();
        GameState.VehicleMeshBuilderSystem.UpdateMesh();
        GameState.PodMeshBuilderSystem.UpdateMesh();
        GameState.ProjectileMeshBuilderSystem.UpdateMesh();
        GameState.ParticleMeshBuilderSystem.UpdateMesh();
        GameState.MechMeshBuilderSystem.UpdateMesh();

        // Draw Frames.
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Back);
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Mid);
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
        GameState.Renderer.DrawFrame(ref GameState.AgentMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Agent));
        GameState.Renderer.DrawFrame(ref GameState.ItemMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.VehicleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Vehicle));
        GameState.Renderer.DrawFrame(ref GameState.PodMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Vehicle));
        GameState.Renderer.DrawFrame(ref GameState.ProjectileMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.ParticleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.MechMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Mech));

        
        GameState.AgentAgent3DModelMovementSystem.Update();
        GameState.AgentAgent3DModelAnimationSystem.Update();

        GameState.FloatingTextDrawSystem.Draw(10000);

        // Delete Entities.
        GameState.ProjectileDeleteSystem.Update();
    }
}
