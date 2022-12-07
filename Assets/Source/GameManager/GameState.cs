using Audio;
using Planet;
using Loader;

// <a href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors">Static Constructor</a>
public static class GameState
{
    public static Planet.PlanetState Planet;
    
    #region Aninmation
    public static readonly Animation.AnimationManager AnimationManager;
    public static readonly Animation.UpdateSystem AnimationUpdateSystem;
    #endregion

    #region AI
    public static readonly AI.Movement.PathFinding PathFinding;
    public static readonly AI.Movement.DrawDebugSystem PathFindingDebugSystem;
    public static readonly AI.Movement.PositionScoreSystem MovementPositionScoreSystem;
    public static readonly AI.BlackboardManager BlackboardManager;
    public static readonly AI.UpdatePosition BlackboardUpdatePosition;
    public static readonly NodeSystem.NodeManager NodeManager;
    public static readonly NodeSystem.ActionManager ActionManager;
    public static readonly NodeSystem.ConditionManager ConditionManager;
    public static readonly BehaviorTree.BehaviorTreeManager BehaviorTreeManager;
    public static readonly BehaviorTree.UpdateSystem BehaviorTreeUpdateSystem;
    public static readonly Sensor.SensorManager SensorManager;
    public static readonly Sensor.UpdateSystem SensorUpdateSystem;
    public static readonly AI.SquadBehvaior.SquadManager SquadManager;
    public static readonly AI.SquadBehvaior.SquadUpdateSystem SquadUpdateSystem;
    #endregion

    #region PlayerActions
    public static readonly Node.CreationSystem            ActionCreationSystem;
    public static readonly Node.SchedulerSystem           ActionSchedulerSystem;
    public static readonly ActionCoolDown.CoolDownSystem  ActionCoolDownSystem;
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

    #region DarkGreyBackground

    public static readonly TGen.DarkGreyBackground.BackgroundGrid BackgroundGrid;
    public static readonly TGen.DarkGreyBackground.RenderGridOverlay BackgroundGridOverlay;
    public static readonly TGen.DarkGreyBackground.RenderMapBorder BackgroundRenderMapBorder;
    public static readonly TGen.DarkGreyBackground.RenderMapMesh BackgroundRenderMapMesh;

    #endregion

    #region Sprites

    public static readonly Sprites.SpriteAtlasManager SpriteAtlasManager;
    public static readonly SpriteLoader SpriteLoader;

    #endregion

    #region Mech
    public static readonly Mech.MechCreationApi MechCreationApi;
    public static readonly Mech.MechSpawnSystem MechSpawnerSystem;
    public static readonly Mech.MeshBuilderSystem MechMeshBuilderSystem;
    public static readonly Mech.MouseInteractionSystem MechMouseInteractionSystem;
    public static readonly Mech.PlantGrowthSystem MechPlantGrowthSystem;
    #endregion

    #region Agent
    public static readonly Agent.AgentCreationApi AgentCreationApi;
    public static readonly Agent.AgentSpawnerSystem AgentSpawnerSystem;
    public static readonly Agent.MeshBuilderSystem AgentMeshBuilderSystem;
    public static readonly Agent.MovementSystem AgentMovementSystem;
    public static readonly Agent.AgentIKSystem AgentIKSystem;
    public static readonly Agent.ProcessPhysicalState AgentProcessPhysicalState;
    public static readonly Agent.ProcessCollisionSystem AgentProcessCollisionSystem;
    public static readonly Agent.Model3DMovementSystem AgentAgent3DModelMovementSystem;
    public static readonly Agent.Model3DAnimationSystem AgentAgent3DModelAnimationSystem;
    public static readonly Agent.MouseInteractionSystem AgentMouseInteractionSystem;
    public static readonly Agent.ProcessState AgentProcessState;

    public static readonly Agent.AgentEffectSystem AgentEffectSystem;
    public static readonly Agent.AgentMoveListPropertiesManager AgentMoveListPropertiesManager;

    public static readonly Agent.AgentMovementAnimationTable AgentMovementAnimationTable;
    #endregion

    public static readonly Collisions.LinePropertiesManager LinePropertiesManager;
    public static readonly Collisions.PointCreationApi PointCreationApi;
    public static readonly Collisions.GeometryPropertiesManager GeometryPropertiesManager;

    public static readonly Collisions.AdjacencyPropertiesManager AdjacencyPropertiesManager;

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

    public static readonly Utility.FileLoadingManager FileLoadingManager;
    public static readonly ECSInput.InputProcessSystem InputProcessSystem;

    #region Projectile
    public static readonly Projectile.ProjectileCreationApi ProjectileCreationApi;
    public static readonly Projectile.ProcessCollisionSystem ProjectileCollisionSystem;
    public static readonly Projectile.MovementSystem ProjectileMovementSystem;
    public static readonly Projectile.SpawnerSystem ProjectileSpawnerSystem;
    public static readonly Projectile.MeshBuilderSystem ProjectileMeshBuilderSystem;
    public static readonly Projectile.ProcessOnHit ProjectileProcessOnHit;
    public static readonly Projectile.ProcessState ProjectileProcessState;
    public static readonly Projectile.DeleteSystem ProjectileDeleteSystem;
    public static readonly Projectile.DebugSystem ProjectileDebugSystem;
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

    public static readonly Vehicle.Pod.PodCreationApi PodCreationApi;
    public static readonly Vehicle.Pod.ProcessCollisionSystem PodCollisionSystem;
    public static readonly Vehicle.Pod.MovementSystem PodMovementSystem;
    public static readonly Vehicle.Pod.SpawnerSystem PodSpawnerSystem;
    public static readonly Vehicle.Pod.MeshBuilderSystem PodMeshBuilderSystem;
    public static readonly Vehicle.Pod.AISystem PodAISystem;
    #endregion

    #region Particle
    public static readonly Particle.ParticleEffectPropertiesManager ParticleEffectPropertiesManager;
    public static readonly Particle.ParticlePropertiesManager ParticlePropertiesManager;
    public static readonly Particle.ParticleEmitterPropertiesManager ParticleEmitterPropertiesManager;
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
    // outdated
    public static readonly KGUI.GUIManager GUIManager;

    #endregion

    #region Audio

    public static AudioSystem AudioSystem;

    #endregion

    public static readonly Prefab.PrefabManager PrefabManager;


    public static void InitStage1()
    {
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

    public static void InitStage2()
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


    static GameState()
    {
        Planet = new PlanetState();

        PrefabManager = new Prefab.PrefabManager();
    

        PathFinding = new AI.Movement.PathFinding();
        PathFindingDebugSystem = new AI.Movement.DrawDebugSystem();
        PathFindingDebugSystem = new AI.Movement.DrawDebugSystem();
        MovementPositionScoreSystem = new AI.Movement.PositionScoreSystem();
        BlackboardManager = new AI.BlackboardManager();
        BlackboardUpdatePosition = new AI.UpdatePosition();
        NodeManager =   new NodeSystem.NodeManager();
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

        //TODO(): move these out of here
        InitStage1();
        InitStage2();
    }
}
