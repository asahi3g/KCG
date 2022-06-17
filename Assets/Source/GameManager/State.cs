namespace Game
{
    /// <summary>
    /// <a href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-constructors">Static Constructor</a>
    /// </summary>
    public class State
    {
        public static readonly Animation.AnimationManager AnimationManager;
        public static readonly Animation.UpdateSystem AnimationUpdateSystem;

        #region Tile

        public static readonly Tile.SpriteAtlasManager TileSpriteAtlasManager;
        public static readonly Tile.CreationApi TileCreationApi;

        #endregion

        #region Sprites

        public static readonly Sprites.SpriteAtlasManager SpriteAtlasManager;
        public static readonly Sprites.SpriteLoader SpriteLoader;
        public static readonly Sprites.UnityImage2DCache UnityImage2DCache;

        #endregion

        #region Agent

        public static readonly Agent.SpawnerSystem SpawnerSystem;
        public static readonly Agent.DrawSystem DrawSystem;
        public static readonly Agent.EnemyAiSystem EnemyAiSystem;

        #endregion

        #region Physics

        public static readonly Physics.MovableSystem MovableSystem;
        public static readonly Physics.ProcessCollisionSystem ProcessCollisionSystem;

        #endregion

        #region Inventory

        public static readonly Inventory.ManagerSystem InventoryManagerSystem;
        public static readonly Inventory.DrawSystem InventoryDrawSystem;

        #endregion

        #region FloatingText

        public static readonly FloatingText.UpdateSystem FloatingTextUpdateSystem;
        public static readonly FloatingText.SpawnerSystem FloatingTextSpawnerSystem;
        public static readonly FloatingText.DrawSystem FloatingTextDrawSystem;

        #endregion
        
        public static readonly Item.DrawSystem ItemDrawSystem;
        public static readonly Item.SpawnerSystem ItemSpawnSystem;

        public static readonly FileLoadingManager FileLoadingManager;
        public static readonly ECSInput.ProcessSystem ProcessSystem;


        static State()
        {
            Contexts entitasContext = Contexts.sharedInstance;

            TileSpriteAtlasManager = new Tile.SpriteAtlasManager();
            SpriteAtlasManager = new Sprites.SpriteAtlasManager();
            TileCreationApi = new Tile.CreationApi();
            SpriteLoader = new Sprites.SpriteLoader();
            FileLoadingManager = new FileLoadingManager();
            ProcessSystem = new ECSInput.ProcessSystem();
            SpawnerSystem = new Agent.SpawnerSystem();
            MovableSystem = new Physics.MovableSystem();
            DrawSystem = new Agent.DrawSystem();
            InventoryDrawSystem = new Inventory.DrawSystem();
            InventoryManagerSystem = new Inventory.ManagerSystem();
            ProcessCollisionSystem = new Physics.ProcessCollisionSystem();
            EnemyAiSystem = new Agent.EnemyAiSystem();
            AnimationManager = new Animation.AnimationManager();
            FloatingTextUpdateSystem = new FloatingText.UpdateSystem();
            FloatingTextSpawnerSystem = new FloatingText.SpawnerSystem(entitasContext);
            FloatingTextDrawSystem = new FloatingText.DrawSystem();
            AnimationUpdateSystem = new Animation.UpdateSystem();
            ItemDrawSystem = new Item.DrawSystem(entitasContext);
            ItemSpawnSystem = new Item.SpawnerSystem(entitasContext);
            UnityImage2DCache = new Sprites.UnityImage2DCache();
        }
    }
}