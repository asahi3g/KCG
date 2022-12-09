using Audio;
using Planet;
using Loader;

// <a href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors">Static Constructor</a>
public static class GameState
{
    public static Planet.PlanetState Planet;
    
    #region Aninmation
    public static Animation.AnimationManager AnimationManager;
    public static Animation.UpdateSystem AnimationUpdateSystem;
    #endregion

    #region AI
    public static AI.Movement.PathFinding PathFinding;
    public static AI.Movement.DrawDebugSystem PathFindingDebugSystem;
    public static AI.Movement.PositionScoreSystem MovementPositionScoreSystem;
    public static AI.BlackboardManager BlackboardManager;
    public static AI.UpdatePosition BlackboardUpdatePosition;
    public static NodeSystem.NodeManager NodeManager;
    public static NodeSystem.ActionManager ActionManager;
    public static NodeSystem.ConditionManager ConditionManager;
    public static BehaviorTree.BehaviorTreeManager BehaviorTreeManager;
    public static BehaviorTree.UpdateSystem BehaviorTreeUpdateSystem;
    public static Sensor.SensorManager SensorManager;
    public static Sensor.UpdateSystem SensorUpdateSystem;
    public static AI.SquadBehvaior.SquadManager SquadManager;
    public static AI.SquadBehvaior.SquadUpdateSystem SquadUpdateSystem;
    #endregion

    #region PlayerActions
    public static Node.CreationSystem            ActionCreationSystem;
    public static Node.SchedulerSystem           ActionSchedulerSystem;
    public static ActionCoolDown.CoolDownSystem  ActionCoolDownSystem;
    #endregion

    #region Tile

    public static PlanetTileMap.TileAtlasManager TileSpriteAtlasManager;
    public static PlanetTileMap.TileCreationApi TileCreationApi;
    public static PlanetTileMap.TileMapRenderer TileMapRenderer;

    #endregion

    #region TGen

    public static TGen.Grid TGenGrid;
    public static TGen.RenderGridOverlay TGenRenderGridOverlay;
    public static TGen.RenderMapBorder TGenRenderMapBorder;
    public static TGen.RenderMapMesh TGenRenderMapMesh;

    #endregion

    #region DarkGreyBackground

    public static TGen.DarkGreyBackground.BackgroundGrid BackgroundGrid;
    public static TGen.DarkGreyBackground.RenderGridOverlay BackgroundGridOverlay;
    public static TGen.DarkGreyBackground.RenderMapBorder BackgroundRenderMapBorder;
    public static TGen.DarkGreyBackground.RenderMapMesh BackgroundRenderMapMesh;

    #endregion

    #region Sprites

    public static Sprites.SpriteAtlasManager SpriteAtlasManager;
    public static SpriteLoader SpriteLoader;

    #endregion

    #region Mech
    public static Mech.MechCreationApi MechCreationApi;
    public static Mech.MechSpawnSystem MechSpawnerSystem;
    public static Mech.MeshBuilderSystem MechMeshBuilderSystem;
    public static Mech.MouseInteractionSystem MechMouseInteractionSystem;
    public static Mech.PlantGrowthSystem MechPlantGrowthSystem;
    #endregion

    #region Agent
    public static Agent.AgentCreationApi AgentCreationApi;
    public static Agent.AgentSpawnerSystem AgentSpawnerSystem;
    public static Agent.MeshBuilderSystem AgentMeshBuilderSystem;
    public static Agent.MovementSystem AgentMovementSystem;
    public static Agent.AgentIKSystem AgentIKSystem;
    public static Agent.ProcessPhysicalState AgentProcessPhysicalState;
    public static Agent.ProcessCollisionSystem AgentProcessCollisionSystem;
    public static Agent.Model3DMovementSystem AgentAgent3DModelMovementSystem;
    public static Agent.Model3DAnimationSystem AgentAgent3DModelAnimationSystem;
    public static Agent.MouseInteractionSystem AgentMouseInteractionSystem;
    public static Agent.ProcessState AgentProcessState;

    public static Agent.AgentEffectSystem AgentEffectSystem;
    public static Agent.AgentMoveListPropertiesManager AgentMoveListPropertiesManager;

    public static Agent.AgentMovementAnimationTable AgentMovementAnimationTable;
    #endregion

    public static Collisions.LinePropertiesManager LinePropertiesManager;
    public static Collisions.PointCreationApi PointCreationApi;
    public static Collisions.GeometryPropertiesManager GeometryPropertiesManager;

    public static Collisions.AdjacencyPropertiesManager AdjacencyPropertiesManager;

    #region Inventory
    public static Inventory.CreationApi InventoryCreationApi;
    public static Inventory.InventoryManager InventoryManager;
    public static Inventory.DrawSystem InventoryDrawSystem;
    public static Inventory.MouseSelectionSystem InventoryMouseSelectionSystem;
    public static Inventory.WindowScaleSystem InventoryWindowScaleSystem;
    #endregion

    #region Item
    public static Item.SpawnerSystem ItemSpawnSystem;
    public static Item.PickUpSystem ItemPickUpSystem;
    public static Item.MeshBuilderSystem ItemMeshBuilderSystem;
    public static Item.MovementSystem ItemMovableSystem;
    public static Item.ProcessCollisionSystem ItemProcessCollisionSystem;
    public static Item.ItemCreationApi ItemCreationApi;
    #endregion

    #region Loot
    public static LootDrop.CreationApi LootTableCreationAPI;
    public static LootDrop.LootDropSystem LootDropSystem;
    #endregion

    public static Utility.FileLoadingManager FileLoadingManager;
    public static ECSInput.InputProcessSystem InputProcessSystem;

    #region Projectile
    public static Projectile.ProjectileCreationApi ProjectileCreationApi;
    public static Projectile.ProcessCollisionSystem ProjectileCollisionSystem;
    public static Projectile.MovementSystem ProjectileMovementSystem;
    public static Projectile.SpawnerSystem ProjectileSpawnerSystem;
    public static Projectile.MeshBuilderSystem ProjectileMeshBuilderSystem;
    public static Projectile.ProcessOnHit ProjectileProcessOnHit;
    public static Projectile.ProcessState ProjectileProcessState;
    public static Projectile.DeleteSystem ProjectileDeleteSystem;
    public static Projectile.DebugSystem ProjectileDebugSystem;
    #endregion

    #region FloatingText
    public static FloatingText.FloatingTextUpdateSystem FloatingTextUpdateSystem;
    public static FloatingText.FloatingTextSpawnerSystem FloatingTextSpawnerSystem;
    public static FloatingText.FloatingTextDrawSystem FloatingTextDrawSystem;
    #endregion

    #region Vehicle
    public static Vehicle.VehicleCreationApi VehicleCreationApi;
    public static Vehicle.ProcessCollisionSystem VehicleCollisionSystem;
    public static Vehicle.MovementSystem VehicleMovementSystem;
    public static Vehicle.SpawnerSystem VehicleSpawnerSystem;
    public static Vehicle.MeshBuilderSystem VehicleMeshBuilderSystem;
    public static Vehicle.AISystem VehicleAISystem;

    public static Vehicle.Pod.PodCreationApi PodCreationApi;
    public static Vehicle.Pod.ProcessCollisionSystem PodCollisionSystem;
    public static Vehicle.Pod.MovementSystem PodMovementSystem;
    public static Vehicle.Pod.SpawnerSystem PodSpawnerSystem;
    public static Vehicle.Pod.MeshBuilderSystem PodMeshBuilderSystem;
    public static Vehicle.Pod.AISystem PodAISystem;
    #endregion

    #region Particle
    public static Particle.ParticleEffectPropertiesManager ParticleEffectPropertiesManager;
    public static Particle.ParticlePropertiesManager ParticlePropertiesManager;
    public static Particle.ParticleEmitterPropertiesManager ParticleEmitterPropertiesManager;
    public static Particle.ParticleEmitterUpdateSystem ParticleEmitterUpdateSystem;
    public static Particle.ParticleUpdateSystem ParticleUpdateSystem;
    public static Particle.ParticleEmitterSpawnerSystem ParticleEmitterSpawnerSystem;
    public static Particle.ParticleSpawnerSystem ParticleSpawnerSystem;
    public static Particle.ParticleProcessCollisionSystem ParticleProcessCollisionSystem;
    public static Particle.MeshBuilderSystem ParticleMeshBuilderSystem;
    #endregion

    #region Render
    public static Utility.Render Renderer;


    #endregion

    #region GUI/HUD
    // outdated
    public static KGUI.GUIManager GUIManager;

    #endregion

    #region Audio

    public static AudioSystem AudioSystem;

    #endregion

    public static Prefab.PrefabManager PrefabManager;


    private static void InitStage1()
    {
        Planet = new PlanetState();

        PrefabManager = new Prefab.PrefabManager();

        PathFinding = new AI.Movement.PathFinding();
        PathFindingDebugSystem = new AI.Movement.DrawDebugSystem();
        PathFindingDebugSystem = new AI.Movement.DrawDebugSystem();
        MovementPositionScoreSystem = new AI.Movement.PositionScoreSystem();
        BlackboardManager = new AI.BlackboardManager();
        BlackboardUpdatePosition = new AI.UpdatePosition();
        NodeManager = new NodeSystem.NodeManager();
        ActionManager = new NodeSystem.ActionManager();
        ConditionManager = new NodeSystem.ConditionManager();
        BehaviorTreeManager = new BehaviorTree.BehaviorTreeManager();
        BehaviorTreeUpdateSystem = new BehaviorTree.UpdateSystem();
        SensorManager = new Sensor.SensorManager();
        SensorUpdateSystem = new Sensor.UpdateSystem();
        SquadManager = new AI.SquadBehvaior.SquadManager();
        SquadUpdateSystem = new AI.SquadBehvaior.SquadUpdateSystem();

        SpriteLoader = new SpriteLoader();
        TileSpriteAtlasManager = new PlanetTileMap.TileAtlasManager();
        SpriteAtlasManager = new Sprites.SpriteAtlasManager();

        FileLoadingManager = new Utility.FileLoadingManager();
        InputProcessSystem = new ECSInput.InputProcessSystem();

        TileCreationApi = new PlanetTileMap.TileCreationApi();
        TileMapRenderer = new PlanetTileMap.TileMapRenderer();

        AgentCreationApi = new Agent.AgentCreationApi();
        AgentSpawnerSystem = new Agent.AgentSpawnerSystem();
        AgentProcessCollisionSystem = new Agent.ProcessCollisionSystem();
        AgentProcessPhysicalState = new Agent.ProcessPhysicalState();
        AgentMovementSystem = new Agent.MovementSystem();
        AgentIKSystem = new Agent.AgentIKSystem();
        AgentMeshBuilderSystem = new Agent.MeshBuilderSystem();
        AgentAgent3DModelMovementSystem = new Agent.Model3DMovementSystem();
        AgentAgent3DModelAnimationSystem = new Agent.Model3DAnimationSystem();
        AgentMouseInteractionSystem = new Agent.MouseInteractionSystem();
        AgentProcessState = new Agent.ProcessState();
        AgentMoveListPropertiesManager = new Agent.AgentMoveListPropertiesManager();
        AgentEffectSystem = new Agent.AgentEffectSystem();
        AgentMovementAnimationTable = new Agent.AgentMovementAnimationTable();

        LinePropertiesManager = new Collisions.LinePropertiesManager();
        PointCreationApi = new Collisions.PointCreationApi();
        GeometryPropertiesManager = new Collisions.GeometryPropertiesManager();
        AdjacencyPropertiesManager = new Collisions.AdjacencyPropertiesManager();

        MechCreationApi = new Mech.MechCreationApi();
        MechSpawnerSystem = new Mech.MechSpawnSystem();

        InventoryManager = new Inventory.InventoryManager();
        InventoryDrawSystem = new Inventory.DrawSystem();
        InventoryCreationApi = new Inventory.CreationApi();
        InventoryMouseSelectionSystem = new Inventory.MouseSelectionSystem();
        InventoryWindowScaleSystem = new Inventory.WindowScaleSystem();

        AnimationManager = new Animation.AnimationManager();

        FloatingTextUpdateSystem = new FloatingText.FloatingTextUpdateSystem();
        FloatingTextSpawnerSystem = new FloatingText.FloatingTextSpawnerSystem();
        FloatingTextDrawSystem = new FloatingText.FloatingTextDrawSystem();

        AnimationUpdateSystem = new Animation.UpdateSystem();

        ItemCreationApi = new Item.ItemCreationApi();
        ItemSpawnSystem = new Item.SpawnerSystem();
        ItemPickUpSystem = new Item.PickUpSystem();
        ItemMeshBuilderSystem = new Item.MeshBuilderSystem();
        ItemMovableSystem = new Item.MovementSystem();
        ItemProcessCollisionSystem = new Item.ProcessCollisionSystem();

        LootTableCreationAPI = new LootDrop.CreationApi();
        LootDropSystem = new LootDrop.LootDropSystem();

        ActionCreationSystem = new Node.CreationSystem();
        ActionSchedulerSystem = new Node.SchedulerSystem();
        ActionCoolDownSystem = new ActionCoolDown.CoolDownSystem();

        ParticleEffectPropertiesManager = new Particle.ParticleEffectPropertiesManager();
        ParticlePropertiesManager = new Particle.ParticlePropertiesManager();
        ParticleEmitterPropertiesManager = new Particle.ParticleEmitterPropertiesManager();
        ParticleEmitterUpdateSystem = new Particle.ParticleEmitterUpdateSystem();
        ParticleMeshBuilderSystem = new Particle.MeshBuilderSystem();
        ParticleUpdateSystem = new Particle.ParticleUpdateSystem();
        ParticleEmitterSpawnerSystem = new Particle.ParticleEmitterSpawnerSystem();
        ParticleSpawnerSystem = new Particle.ParticleSpawnerSystem();
        ParticleProcessCollisionSystem = new Particle.ParticleProcessCollisionSystem();

        ProjectileCreationApi = new Projectile.ProjectileCreationApi();
        ProjectileCollisionSystem = new Projectile.ProcessCollisionSystem();
        ProjectileMovementSystem = new Projectile.MovementSystem();
        ProjectileSpawnerSystem = new Projectile.SpawnerSystem();
        ProjectileMeshBuilderSystem = new Projectile.MeshBuilderSystem();
        ProjectileProcessOnHit = new Projectile.ProcessOnHit();
        ProjectileProcessState = new Projectile.ProcessState();
        ProjectileDeleteSystem = new Projectile.DeleteSystem();
        ProjectileDebugSystem = new Projectile.DebugSystem();

        Renderer = new Utility.Render();

        TGenGrid = new TGen.Grid();
        TGenRenderGridOverlay = new TGen.RenderGridOverlay();
        TGenRenderMapBorder = new TGen.RenderMapBorder();
        TGenRenderMapMesh = new TGen.RenderMapMesh();

        BackgroundGrid = new TGen.DarkGreyBackground.BackgroundGrid();
        BackgroundGridOverlay = new TGen.DarkGreyBackground.RenderGridOverlay();
        BackgroundRenderMapBorder = new TGen.DarkGreyBackground.RenderMapBorder();
        BackgroundRenderMapMesh = new TGen.DarkGreyBackground.RenderMapMesh();

        GUIManager = new KGUI.GUIManager();

        VehicleCreationApi = new Vehicle.VehicleCreationApi();
        VehicleCollisionSystem = new Vehicle.ProcessCollisionSystem();
        VehicleMovementSystem = new Vehicle.MovementSystem();
        VehicleSpawnerSystem = new Vehicle.SpawnerSystem();
        VehicleMeshBuilderSystem = new Vehicle.MeshBuilderSystem();
        VehicleAISystem = new Vehicle.AISystem();

        PodCreationApi = new Vehicle.Pod.PodCreationApi();
        PodCollisionSystem = new Vehicle.Pod.ProcessCollisionSystem();
        PodMovementSystem = new Vehicle.Pod.MovementSystem();
        PodSpawnerSystem = new Vehicle.Pod.SpawnerSystem();
        PodMeshBuilderSystem = new Vehicle.Pod.MeshBuilderSystem();
        PodAISystem = new Vehicle.Pod.AISystem();

        MechCreationApi = new Mech.MechCreationApi();
        MechSpawnerSystem = new Mech.MechSpawnSystem();
        MechMeshBuilderSystem = new Mech.MeshBuilderSystem();
        MechMouseInteractionSystem = new Mech.MouseInteractionSystem();
        MechPlantGrowthSystem = new Mech.PlantGrowthSystem();

        AudioSystem = new AudioSystem();

        TileSpriteAtlasManager.InitStage1(SpriteLoader);
        SpriteAtlasManager.InitStage1(SpriteLoader);
        AgentMovementAnimationTable.InitStage1();
        PointCreationApi.InitStage1();
        LinePropertiesManager.InitStage1();
        GeometryPropertiesManager.InitStage1();
        AdjacencyPropertiesManager.InitStage1();
        GUIManager.InitStage1();
        ParticleEffectPropertiesManager.InitStage1();
        AgentMoveListPropertiesManager.InitStage1();
        AgentEffectSystem.InitStage1();
        AudioSystem.InitStage1();
        VehicleCreationApi.InitStage1();
        PodCreationApi.InitStage1();
        MechCreationApi.InitStage1();
        ProjectileCreationApi.InitStage1();
        ParticleEmitterUpdateSystem.InitStage1();
        ParticleEmitterSpawnerSystem.InitStage1();
        ParticleSpawnerSystem.InitStage1();
        VehicleAISystem.InitStage1();
        VehicleMovementSystem.InitStage1();
        VehicleSpawnerSystem.InitStage1();
        PodMovementSystem.InitStage1();
        PodSpawnerSystem.InitStage1();
    }

    private static void InitStage2()
    {
        TileSpriteAtlasManager.InitStage2();
        SpriteAtlasManager.InitStage2();
        AgentMovementAnimationTable.InitStage2();
        PointCreationApi.InitStage2();
        LinePropertiesManager.InitStage2();
        GeometryPropertiesManager.InitStage2();
        AdjacencyPropertiesManager.InitStage2();
        GUIManager.InitStage2();
        ParticleEffectPropertiesManager.InitStage2();
        AgentMoveListPropertiesManager.InitStage2();
        AgentEffectSystem.InitStage2();
        AudioSystem.InitStage2(null);
        VehicleCreationApi.InitStage2();
        PodCreationApi.InitStage2();
        MechCreationApi.InitStage2();
        ProjectileCreationApi.InitStage2();
        ParticleEmitterUpdateSystem.InitStage2(ParticleEmitterPropertiesManager, ParticlePropertiesManager);
        ParticleEmitterSpawnerSystem.InitStage2(ParticleEmitterPropertiesManager, ParticlePropertiesManager);
        ParticleSpawnerSystem.InitStage2(ParticlePropertiesManager);
        VehicleAISystem.InitStage2(VehicleCreationApi);
        VehicleMovementSystem.InitStage2(VehicleCreationApi);
        VehicleSpawnerSystem.InitStage2(VehicleCreationApi);
        PodMovementSystem.InitStage2(PodCreationApi);
        PodSpawnerSystem.InitStage2(PodCreationApi);
    }


    public static void Initialize()
    {
        //TODO(): move these out of here
        InitStage1();
        InitStage2();

        GameResources.Initialize();
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
        GameState.SpriteAtlasManager.UpdateAtlasTextures();
    }
}
