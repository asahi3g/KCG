using UnityEngine;
using KMath;
using Enums.Tile;
using Planet;
using PlanetTileMap;

class PlanterTest : MonoBehaviour
{
    [SerializeField] Material Material;
    public PlanetState Planet;

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
        Planet.Update(Time.deltaTime, Material, transform);
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        Planet.DrawHUD(Player);
        if (Event.current.type != EventType.Repaint)
            return;

        KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);
    }

    private void OnDrawGizmos()
    {
        Planet.DrawDebug();

        // Set the color of gizmos
        Gizmos.color = Color.green;

        // Draw a cube around the map
        if (Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

        // Draw lines around player if out of bounds
        if (Player != null)
            if (Player.agentPhysicsState.Position.X - 10.0f >= Planet.TileMap.MapSize.X)
            {
                // Out of bounds

                // X+
                Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector3(Player.agentPhysicsState.Position.X + 10.0f,Player.agentPhysicsState.Position.Y));

                // X-
                Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector2(Player.agentPhysicsState.Position.X-10.0f,Player.agentPhysicsState.Position.Y));

                // Y+
                Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector2(Player.agentPhysicsState.Position.X,Player.agentPhysicsState.Position.Y + 10.0f));

                // Y-
                Gizmos.DrawLine(new Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new Vector2(Player.agentPhysicsState.Position.X,Player.agentPhysicsState.Position.Y - 10.0f));
            }

        // Draw Chunk Visualizer
        ChunkVisualizer.Draw(Planet.TileMap, 0.5f, 0.0f);
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        GameResources.Initialize();

        // Generating the map
        Vec2i mapSize = new Vec2i(32, 32);
        Planet = new Planet.PlanetState();
        Planet.Init(mapSize);

        GenerateMap();
        SpawnStuff();

        Planet.InitializeSystems(Material, transform);
        Planet.InitializeHUD();

        InventoryID = Player.agentInventory.InventoryID;

        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.WaterBottle, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.MajestyPalm, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.SagoPalm, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.DracaenaTrifasciata, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(GameState.InventoryManager, InventoryID, Enums.ItemType.HarvestTool, Planet.EntitasContext);
        //Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ScannerTool, Planet.EntitasContext);
    }

    void GenerateMap()
    {
        ref var tileMap = ref Planet.TileMap;

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
        Player = Planet.AddPlayer(new Vec2f(2, 2), 0);
        Planet.AddMech(new Vec2f(4, 2), Enums.MechType.Planter);
        Planet.AddMech(new Vec2f(8, 2), Enums.MechType.Planter);
        Planet.AddMech(new Vec2f(12, 2), Enums.MechType.Planter);
        Planet.AddMech(new Vec2f(4, 4), Enums.MechType.Light);
        Planet.AddMech(new Vec2f(8, 4), Enums.MechType.Light);
        Planet.AddMech(new Vec2f(12, 4), Enums.MechType.Light);
        Planet.AddUIText("SampleText", new Vec2f(-250.67f, 94.3f), new Vec2f(200, 120));
    }
}
