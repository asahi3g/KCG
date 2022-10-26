//imports UnityEngine

using KMath;
using Enums.Tile;
using PlanetTileMap;

class PlanterTest : UnityEngine.MonoBehaviour
{
    [SerializeField] Material Material;


    AgentEntity Player;
    int InventoryID;
    static bool Init = false;

    public void Start()
    {
        if (!Init)
        {
            Initialize();
            Init = true;
        }
    }

    public void Update()
    {
        GameState.Planet.Update(Time.deltaTime, Material, transform);
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        GameState.Planet.DrawHUD(Player);
        if (Event.current.type != EventType.Repaint)
            return;

        KGUI.Statistics.StatisticsDisplay.DrawStatistics();
    }

    private void OnDrawGizmos()
    {
        GameState.Planet.DrawDebug();

        // Set the color of gizmos
        UnityEngine.Gizmos.color = UnityEngine.Color.green;

        // Draw a cube around the map
        if (GameState.Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(GameState.Planet.TileMap.MapSize.X, GameState.Planet.TileMap.MapSize.Y, 0.0f));

        // Draw lines around player if out of bounds
        if (Player != null)
            if (Player.agentPhysicsState.Position.X - 10.0f >= GameState.Planet.TileMap.MapSize.X)
            {
                // Out of bounds

                // X+
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X + 10.0f,Player.agentPhysicsState.Position.Y));

                // X-
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector2(Player.agentPhysicsState.Position.X-10.0f,Player.agentPhysicsState.Position.Y));

                // Y+
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector2(Player.agentPhysicsState.Position.X,Player.agentPhysicsState.Position.Y + 10.0f));

                // Y-
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector2(Player.agentPhysicsState.Position.X,Player.agentPhysicsState.Position.Y - 10.0f));
            }

        // Draw Chunk Visualizer
        ChunkVisualizer.Draw(0.5f, 0.0f);
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        GameResources.Initialize();

        // Generating the map
        Vec2i mapSize = new Vec2i(32, 32);

        GameState.Planet.Init(mapSize);

        GenerateMap();
        SpawnStuff();

        GameState.Planet.InitializeSystems(Material, transform);
        GameState.Planet.InitializeHUD();

        InventoryID = Player.agentInventory.InventoryID;

        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.PlacementTool, GameState.Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.WaterBottle, GameState.Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.MajestyPalm, GameState.Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.SagoPalm, GameState.Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.DracaenaTrifasciata, GameState.Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.HarvestTool, GameState.Planet.EntitasContext);
        //Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ScannerTool, GameState.Planet.EntitasContext);
    }

    void GenerateMap()
    {
        ref var tileMap = ref GameState.Planet.TileMap;

        for (int j = 0; j < tileMap.MapSize.Y; j++)
        {
            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                TileID frontTile;

                if (i >= tileMap.MapSize.X / 2)
                {
                    if (j % 2 == 0 && i == tileMap.MapSize.X / 2)
                    {
                        frontTile = TileID.Moon;
                    }
                    else
                    {
                        frontTile = TileID.Glass;
                    }
                }
                else
                {
                    if (j % 3 == 0 && i == tileMap.MapSize.X / 2 + 1)
                    {
                        frontTile = TileID.Glass;
                    }
                    else
                    {
                        frontTile = TileID.Moon;
                    }
                }

                if (j is > 1 and < 6 || (j > 8 + i))
                {
                    frontTile = TileID.Air;
                }


                tileMap.SetFrontTile(i, j, frontTile);
            }
        }
    }

    void SpawnStuff()
    {
        Player = GameState.Planet.AddPlayer(new Vec2f(2, 2), 0);
        GameState.Planet.AddMech(new Vec2f(4, 2), Enums.MechType.Planter);
        GameState.Planet.AddMech(new Vec2f(8, 2), Enums.MechType.Planter);
        GameState.Planet.AddMech(new Vec2f(12, 2), Enums.MechType.Planter);
        GameState.Planet.AddMech(new Vec2f(4, 4), Enums.MechType.Light);
        GameState.Planet.AddMech(new Vec2f(8, 4), Enums.MechType.Light);
        GameState.Planet.AddMech(new Vec2f(12, 4), Enums.MechType.Light);
    }
}
