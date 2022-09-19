/// <summary>
/// <a href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors">Static Constructor</a>
/// </summary>

public class GameState
{
    //public static readonly Sprites.UnityImage2DCache UnityImage2DCache;

    #region Atinmation
    public static readonly Animation.AnimationManager AnimationManager;
    public static readonly Animation.UpdateSystem AnimationUpdateSystem;
    #endregion

    #region AI
    public static readonly AI.Movement.PathFinding PathFinding;
    public static readonly AI.Movement.DrawDebugSystem PathFindingDebugSystem;
    #endregion

    #region Action
    public static readonly Action.ActionPropertyManager     ActionPropertyManager;
    public static readonly Action.ActionCreationSystem      ActionCreationSystem;
    public static readonly Action.ActionSchedulerSystem     ActionSchedulerSystem;
    public static readonly Action.InitializeSystem          ActionInitializeSystem;
    public static readonly Action.CoolDownSystem            ActionCoolDownSystem;
    #endregion

    #region Tile

    public static readonly PlanetTileMap.TileAtlasManager TileSpriteAtlasManager;
    public static readonly PlanetTileMap.TileCreationApi TileCreationApi;
    public static readonly PlanetTileMap.TileMapRenderer TileMapRenderer;

    #endregion

    #region TGen

    public static readonly TGen.Grid TGenGrid;
    public static readonly TGen.RenderGridOverlay TGenRenderGridOverlay;
    public static readonly TGen.RenderMapBorder TGenRenderMapBorder;
    public static readonly TGen.RenderMapMesh TGenRenderMapMesh;

    #endregion

    #region Sprites

    public static readonly Sprites.SpriteAtlasManager SpriteAtlasManager;
    public static readonly Sprites.SpriteLoader SpriteLoader;

    #endregion

    #region Mech
    public static readonly Mech.MechCreationApi MechCreationApi;
    public static readonly Mech.MechSpawnSystem MechSpawnerSystem;
    public static readonly Mech.MeshBuilderSystem MechMeshBuilderSystem;
    public static readonly Mech.MechGUIDrawSystem MechGUIDrawSystem;
    public static readonly Mech.MouseInteractionSystem MechMouseInteractionSystem;
    #endregion

    #region Agent
    public static readonly Agent.AgentCreationApi AgentCreationApi;
    public static readonly Agent.AgentSpawnerSystem AgentSpawnerSystem;
    public static readonly Agent.EnemyAiSystem EnemyAiSystem;
    public static readonly Agent.MeshBuilderSystem AgentMeshBuilderSystem;
    public static readonly Agent.MovementSystem AgentMovementSystem;
    public static readonly Agent.ProcessPhysicalState AgentProcessPhysicalState;
    public static readonly Agent.ProcessCollisionSystem AgentProcessCollisionSystem;
    public static readonly Agent.Model3DMovementSystem AgentModel3DMovementSystem;
    public static readonly Agent.Model3DAnimationSystem AgentModel3DAnimationSystem;
    public static readonly Agent.MouseInteractionSystem AgentMouseInteractionSystem;
    #endregion

    #region Inventory
    public static readonly Inventory.CreationApi InventoryCreationApi;
    public static readonly Inventory.InventoryManager InventoryManager;
    public static readonly Inventory.DrawSystem InventoryDrawSystem;
    public static readonly Inventory.MouseSelectionSystem InventoryMouseSelectionSystem;
    public static readonly Inventory.WindowScaleSystem InventoryWindowScaleSystem;
    #endregion

    #region Item
    public static readonly Item.SpawnerSystem ItemSpawnSystem;
    public static readonly Item.PickUpSystem ItemPickUpSystem;
    public static readonly Item.MeshBuilderSystem ItemMeshBuilderSystem;
    public static readonly Item.MovementSystem ItemMovableSystem;
    public static readonly Item.ProcessCollisionSystem ItemProcessCollisionSystem;
    public static readonly Item.ItemCreationApi ItemCreationApi;
    #endregion

    #region Loot
    public static readonly LootDrop.CreationApi LootTableCreationAPI;
    public static readonly LootDrop.LootDropSystem LootDropSystem;
    #endregion

    #region Projectile
    public static readonly Projectile.ProjectileCreationApi ProjectileCreationApi;
    public static readonly Projectile.ProcessCollisionSystem ProjectileCollisionSystem;
    public static readonly Projectile.MovementSystem ProjectileMovementSystem;
    public static readonly Projectile.SpawnerSystem ProjectileSpawnerSystem;
    public static readonly Projectile.MeshBuilderSystem ProjectileMeshBuilderSystem;
    public static readonly Projectile.ProcessOnHit ProjectileProcessOnHit;
    #endregion

    #region FloatingText
    public static readonly FloatingText.FloatingTextUpdateSystem FloatingTextUpdateSystem;
    public static readonly FloatingText.FloatingTextSpawnerSystem FloatingTextSpawnerSystem;
    public static readonly FloatingText.FloatingTextDrawSystem FloatingTextDrawSystem;
    #endregion

    #region Vehicle
    public static readonly Vehicle.VehicleCreationApi VehicleCreationApi;
    public static readonly Vehicle.ProcessCollisionSystem VehicleCollisionSystem;
    public static readonly Vehicle.MovementSystem VehicleMovementSystem;
    public static readonly Vehicle.SpawnerSystem VehicleSpawnerSystem;
    public static readonly Vehicle.MeshBuilderSystem VehicleMeshBuilderSystem;
    public static readonly Vehicle.AISystem VehicleAISystem;
    #endregion

    public static readonly Utility.FileLoadingManager FileLoadingManager;
    public static readonly ECSInput.InputProcessSystem InputProcessSystem;

    #region Particle
    public static readonly Particle.ParticleCreationApi ParticleCreationApi;
    public static readonly Particle.ParticleEmitterCreationApi ParticleEmitterCreationApi;
    public static readonly Particle.ParticleEmitterUpdateSystem ParticleEmitterUpdateSystem;
    public static readonly Particle.ParticleUpdateSystem ParticleUpdateSystem;
    public static readonly Particle.ParticleEmitterSpawnerSystem ParticleEmitterSpawnerSystem;
    public static readonly Particle.ParticleSpawnerSystem ParticleSpawnerSystem;
    public static readonly Particle.ParticleProcessCollisionSystem ParticleProcessCollisionSystem;
    public static readonly Particle.MeshBuilderSystem ParticleMeshBuilderSystem;
    #endregion

    #region Render
    public static Utility.Render Renderer;
    #endregion

    #region GUI/HUD
    public static KGUI.Elements.ElementSpawnerSystem ElementSpawnerSystem;
    public static KGUI.Elements.ElementDrawSystem ElementDrawSystem;
    public static KGUI.Elements.ElementUpdateSystem ElementUpdateSystem;
    #endregion


    public static void InitStage1()
    {
        TileSpriteAtlasManager.InitStage1(SpriteLoader);
        SpriteAtlasManager.InitStage1(SpriteLoader);
    }

    public static void InitStage2()
    {
        TileSpriteAtlasManager.InitStage2();
        SpriteAtlasManager.InitStage2();
    }


    static GameState()
    {
        PathFinding = new AI.Movement.PathFinding();

        PathFindingDebugSystem = new AI.Movement.DrawDebugSystem();

        SpriteLoader = new Sprites.SpriteLoader();

        TileSpriteAtlasManager = new PlanetTileMap.TileAtlasManager();

        SpriteAtlasManager = new Sprites.SpriteAtlasManager();
        

        TileCreationApi = new PlanetTileMap.TileCreationApi();
        TileMapRenderer = new PlanetTileMap.TileMapRenderer();

        FileLoadingManager = new Utility.FileLoadingManager();

        InputProcessSystem = new ECSInput.InputProcessSystem();

        AgentCreationApi = new Agent.AgentCreationApi();
        AgentSpawnerSystem = new Agent.AgentSpawnerSystem(AgentCreationApi);
        AgentProcessCollisionSystem = new Agent.ProcessCollisionSystem();
        AgentProcessPhysicalState = new Agent.ProcessPhysicalState();
        AgentMovementSystem = new Agent.MovementSystem();
        AgentMeshBuilderSystem = new Agent.MeshBuilderSystem();
        AgentModel3DMovementSystem = new Agent.Model3DMovementSystem();
        AgentModel3DAnimationSystem = new Agent.Model3DAnimationSystem();
        AgentMouseInteractionSystem = new Agent.MouseInteractionSystem();

        MechCreationApi = new Mech.MechCreationApi();
        MechSpawnerSystem = new Mech.MechSpawnSystem(MechCreationApi);

        InventoryManager = new Inventory.InventoryManager();
        InventoryDrawSystem = new Inventory.DrawSystem();
        InventoryCreationApi = new Inventory.CreationApi();
        InventoryMouseSelectionSystem = new Inventory.MouseSelectionSystem();
        InventoryWindowScaleSystem = new Inventory.WindowScaleSystem();

        EnemyAiSystem = new Agent.EnemyAiSystem();

        AnimationManager = new Animation.AnimationManager();

        FloatingTextUpdateSystem = new FloatingText.FloatingTextUpdateSystem();
        FloatingTextSpawnerSystem = new FloatingText.FloatingTextSpawnerSystem();
        FloatingTextDrawSystem = new FloatingText.FloatingTextDrawSystem();

        AnimationUpdateSystem = new Animation.UpdateSystem();

        //UnityImage2DCache = new Sprites.UnityImage2DCache();

        ItemCreationApi = new Item.ItemCreationApi();
        ItemSpawnSystem = new Item.SpawnerSystem();
        ItemPickUpSystem = new Item.PickUpSystem();
        ItemMeshBuilderSystem = new Item.MeshBuilderSystem();
        ItemMovableSystem = new Item.MovementSystem();
        ItemProcessCollisionSystem = new Item.ProcessCollisionSystem();

        LootTableCreationAPI = new LootDrop.CreationApi();
        LootDropSystem = new LootDrop.LootDropSystem();

        ActionPropertyManager = new Action.ActionPropertyManager();
        ActionCreationSystem = new Action.ActionCreationSystem();
        ActionSchedulerSystem = new Action.ActionSchedulerSystem();
        ActionInitializeSystem = new Action.InitializeSystem();
        ActionCoolDownSystem = new Action.CoolDownSystem();

        ParticleCreationApi = new Particle.ParticleCreationApi();
        ParticleEmitterCreationApi = new Particle.ParticleEmitterCreationApi();
        ParticleEmitterUpdateSystem = new Particle.ParticleEmitterUpdateSystem(ParticleEmitterCreationApi, ParticleCreationApi);
        ParticleMeshBuilderSystem = new Particle.MeshBuilderSystem();
        ParticleUpdateSystem = new Particle.ParticleUpdateSystem();
        ParticleEmitterSpawnerSystem = new Particle.ParticleEmitterSpawnerSystem(ParticleEmitterCreationApi, ParticleCreationApi);
        ParticleSpawnerSystem = new Particle.ParticleSpawnerSystem(ParticleCreationApi);
        ParticleProcessCollisionSystem = new Particle.ParticleProcessCollisionSystem();

        ProjectileCreationApi = new Projectile.ProjectileCreationApi();
        ProjectileCollisionSystem = new Projectile.ProcessCollisionSystem();
        ProjectileMovementSystem = new Projectile.MovementSystem();
        ProjectileSpawnerSystem = new Projectile.SpawnerSystem();
        ProjectileMeshBuilderSystem = new Projectile.MeshBuilderSystem();
        ProjectileProcessOnHit = new Projectile.ProcessOnHit();

        MechMeshBuilderSystem = new Mech.MeshBuilderSystem();
        MechGUIDrawSystem = new Mech.MechGUIDrawSystem();
        MechMouseInteractionSystem = new Mech.MouseInteractionSystem();

        Renderer = new Utility.Render();

        TGenGrid = new TGen.Grid();
        TGenRenderGridOverlay = new TGen.RenderGridOverlay();
        TGenRenderMapBorder = new TGen.RenderMapBorder();
        TGenRenderMapMesh = new TGen.RenderMapMesh();

        ElementSpawnerSystem = new KGUI.Elements.ElementSpawnerSystem();
        ElementUpdateSystem = new KGUI.Elements.ElementUpdateSystem();
        ElementDrawSystem = new KGUI.Elements.ElementDrawSystem();

        VehicleCreationApi = new Vehicle.VehicleCreationApi();
        VehicleCollisionSystem = new Vehicle.ProcessCollisionSystem();
        VehicleMovementSystem = new Vehicle.MovementSystem(VehicleCreationApi);
        VehicleSpawnerSystem = new Vehicle.SpawnerSystem(VehicleCreationApi);
        VehicleMeshBuilderSystem = new Vehicle.MeshBuilderSystem();
        VehicleAISystem = new Vehicle.AISystem(VehicleCreationApi);









        //TODO(): move these out of here
        InitStage1();
        InitStage2();
    }
}
