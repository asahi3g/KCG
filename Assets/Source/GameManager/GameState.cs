using Agent;
using Inventory;
using Projectile;
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

    #region Sprites

    public static readonly Sprites.SpriteAtlasManager SpriteAtlasManager;
    public static readonly Sprites.SpriteLoader SpriteLoader;

    #endregion

    #region Mech
    public static readonly Mech.MechCreationApi MechCreationApi;
    public static readonly Mech.MechSpawnSystem MechSpawnerSystem;
    public static readonly Mech.MeshBuilderSystem MechMeshBuilderSystem;

    #endregion

    #region Agent
    public static readonly Agent.AgentCreationApi AgentCreationApi;
    public static readonly Agent.AgentSpawnerSystem AgentSpawnerSystem;
    public static readonly Agent.EnemyAiSystem EnemyAiSystem;
    public static readonly Agent.MeshBuilderSystem AgentMeshBuilderSystem;
    public static readonly Agent.MovableSystem AgentMovableSystem;
    public static readonly Agent.ProcessCollisionSystem AgentProcessCollisionSystem;

    #endregion

    #region Inventory
    public static readonly Inventory.InventoryManager InventoryManager;
    public static readonly Inventory.DrawSystem InventoryDrawSystem;
    #endregion

    #region Item
    public static readonly Item.SpawnerSystem ItemSpawnSystem;
    public static readonly Item.PickUpSystem ItemPickUpSystem;
    public static readonly Item.MeshBuilderSystem ItemMeshBuilderSystem;
    public static readonly Item.MovementSystem ItemMovableSystem;
    public static readonly Item.ProcessCollisionSystem ItemProcessCollisionSystem;
    public static readonly Item.ItemCreationApi ItemCreationApi;
    #endregion

    #region Projectile
    public static readonly Projectile.ProjectileCreationApi ProjectileCreationApi;
    public static readonly Projectile.ProcessCollisionSystem ProjectileCollisionSystem;
    public static readonly Projectile.MovementSystem ProjectileMovementSystem;
    public static readonly Projectile.SpawnerSystem ProjectileSpawnerSystem;
    public static readonly Projectile.MeshBuilderSystem ProjectileMeshBuilderSystem;
    #endregion

    #region FloatingText
    public static readonly FloatingText.FloatingTextUpdateSystem FloatingTextUpdateSystem;
    public static readonly FloatingText.FloatingTextSpawnerSystem FloatingTextSpawnerSystem;
    public static readonly FloatingText.FloatingTextDrawSystem FloatingTextDrawSystem;
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


    static GameState()
    {
        SpriteLoader = new Sprites.SpriteLoader();
        TileSpriteAtlasManager = new PlanetTileMap.TileAtlasManager(SpriteLoader);
        SpriteAtlasManager = new Sprites.SpriteAtlasManager(SpriteLoader);
        TileCreationApi = new PlanetTileMap.TileCreationApi();
        TileMapRenderer = new PlanetTileMap.TileMapRenderer();
        FileLoadingManager = new Utility.FileLoadingManager();
        InputProcessSystem = new ECSInput.InputProcessSystem();
        AgentCreationApi = new Agent.AgentCreationApi();
        AgentSpawnerSystem = new Agent.AgentSpawnerSystem(AgentCreationApi);
        AgentProcessCollisionSystem = new Agent.ProcessCollisionSystem();
        AgentMovableSystem = new Agent.MovableSystem();
        AgentMeshBuilderSystem = new Agent.MeshBuilderSystem();
        MechCreationApi = new Mech.MechCreationApi();
        MechSpawnerSystem = new Mech.MechSpawnSystem(MechCreationApi);
        InventoryManager = new Inventory.InventoryManager();
        InventoryDrawSystem = new Inventory.DrawSystem();
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
        ProjectileMovementSystem = new Projectile.MovementSystem(ProjectileCreationApi);
        ProjectileSpawnerSystem = new Projectile.SpawnerSystem(ProjectileCreationApi);
        ProjectileMeshBuilderSystem = new Projectile.MeshBuilderSystem();
        MechMeshBuilderSystem = new Mech.MeshBuilderSystem();
        Renderer = new Utility.Render();
    }
}
