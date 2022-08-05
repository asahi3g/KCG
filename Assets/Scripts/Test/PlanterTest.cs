using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Planet.Unity;
using Planet;

class PlanterTest : MonoBehaviour
{
    [SerializeField] Material Material;

    public PlanetState Planet;
    Inventory.InventoryManager inventoryManager;
    Inventory.DrawSystem inventoryDrawSystem;

    AgentEntity Player;
    int PlayerID;

    int CharacterSpriteId;
    int InventoryID;

    public static int HumanoidCount = 1;
    GameObject[] HumanoidArray;

    AnimationClip IdleAnimationClip;
    AnimationClip RunAnimationClip;
    AnimationClip WalkAnimationClip;
    AnimationClip GolfSwingClip;

    AnimancerComponent[] AnimancerComponentArray;

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
        bool run = Input.GetKeyDown(KeyCode.R);
        bool walk = Input.GetKeyDown(KeyCode.W);
        bool idle = Input.GetKeyDown(KeyCode.I);
        bool golf = Input.GetKeyDown(KeyCode.G);

        for (int i = 0; i < HumanoidCount; i++)
        {
            if (run)
            {
                AnimancerComponentArray[i].Play(RunAnimationClip, 0.25f);
            }
            else if (walk)
            {
                AnimancerComponentArray[i].Play(WalkAnimationClip, 0.25f);
            }
            else if (idle)
            {
                AnimancerComponentArray[i].Play(IdleAnimationClip, 0.25f);
            }
            else if (golf)
            {
                AnimancerComponentArray[i].Play(GolfSwingClip, 0.25f);
            }
        }

        InventoryEntity Inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryIDID(InventoryID);
        int selectedSlot = Inventory.inventoryEntity.SelectedSlotID;

        ItemInventoryEntity item = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, InventoryID, selectedSlot);
        if (item != null)
        {
            ItemProprieties itemProperty = GameState.ItemCreationApi.Get(item.itemType.Type);
            if (itemProperty.IsTool())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameState.ActionCreationSystem.CreateAction(Planet.EntitasContext, itemProperty.ToolActionType,
                        Player.agentID.ID, item.itemID.ID);
                }
            }
        }

        Planet.Update(Time.deltaTime, Material, transform, Player);
        //   Vector2 playerPosition = Player.Entity.agentPosition2D.Value;

        // transform.position = new Vector3(playerPosition.x - 6.0f, playerPosition.y - 6.0f, -10.0f);
    }

    private void OnGUI()
    {
        if (!Init)
            return;

        if (Event.current.type != EventType.Repaint)
            return;

        // Draw Statistics
        KGUI.Statistics.StatisticsDisplay.DrawStatistics(ref Planet);

        inventoryDrawSystem.Draw(Planet.EntitasContext);
    }

    private void OnDrawGizmos()
    {
        // Set the color of gizmos
        Gizmos.color = Color.green;

        // Draw a cube around the map
        if (Planet.TileMap != null)
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(Planet.TileMap.MapSize.X, Planet.TileMap.MapSize.Y, 0.0f));

        // Draw lines around player if out of bounds
        if (Player != null)
            if (Player.agentPosition2D.Value.X - 10.0f >= Planet.TileMap.MapSize.X)
            {
                // Out of bounds

                // X+
                Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector3(Player.agentPosition2D.Value.X+10.0f,Player.agentPosition2D.Value.Y));

                // X-
                Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector2(Player.agentPosition2D.Value.X-10.0f,Player.agentPosition2D.Value.Y));

                // Y+
                Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector2(Player.agentPosition2D.Value.X,Player.agentPosition2D.Value.Y + 10.0f));

                // Y-
                Gizmos.DrawLine(new Vector3(Player.agentPosition2D.Value.X, Player.agentPosition2D.Value.Y, 0.0f), new Vector2(Player.agentPosition2D.Value.X,Player.agentPosition2D.Value.Y - 10.0f));
            }

        // Draw Chunk Visualizer
        Admin.AdminAPI.DrawChunkVisualizer(Planet.TileMap);
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        // get the 3d model from the scene
        //GameObject humanoid = GameObject.Find("DefaultHumanoid");

        GameObject prefab = Engine3D.AssetManager.Singelton.GetModel(Engine3D.ModelType.Stander);

        HumanoidArray = new GameObject[HumanoidCount];
        AnimancerComponentArray = new AnimancerComponent[HumanoidCount];

        for (int i = 0; i < HumanoidCount; i++)
        {
            HumanoidArray[i] = Instantiate(prefab);
            HumanoidArray[i].transform.position = new Vector3(5.0f, 20.0f, -1.0f);

            Vector3 eulers = HumanoidArray[i].transform.rotation.eulerAngles;
            HumanoidArray[i].transform.rotation = Quaternion.Euler(0, eulers.y + 90, 0);

        }


        // create an animancer object and give it a reference to the Animator component
        for (int i = 0; i < HumanoidCount; i++)
        {
            GameObject animancerComponent = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
            // get the animator component from the game object
            // this component is used by animancer
            AnimancerComponentArray[i] = animancerComponent.GetComponent<AnimancerComponent>();
            AnimancerComponentArray[i].Animator = HumanoidArray[i].GetComponent<Animator>();
        }


        IdleAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Idle);
        RunAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Run);
        WalkAnimationClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Walk);
        GolfSwingClip = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.Flip);


        // play the idle animation
        for (int i = 0; i < HumanoidCount; i++)
        {
            AnimancerComponentArray[i].Play(IdleAnimationClip);
        }

        Application.targetFrameRate = 60;

        inventoryManager = new Inventory.InventoryManager();
        inventoryDrawSystem = new Inventory.DrawSystem();

        GameResources.Initialize();

        // Generating the map
        Vec2i mapSize = new Vec2i(32, 32);
        Planet = new Planet.PlanetState();
        Planet.Init(mapSize);

        /*var camera = Camera.main;
        Vector3 lookAtPosition = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
        Planet.TileMap = TileMapManager.Load("map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);*/

        GenerateMap();
        SpawnStuff();

        Planet.InitializeSystems(Material, transform, Player);

        //TileMapManager.Save(Planet.TileMap, "map.kmap");

        InventoryID = Player.agentInventory.InventoryID;

        // Admin API Spawn Items
        Admin.AdminAPI.SpawnItem(Enums.ItemType.Pistol, Planet.EntitasContext);
        Admin.AdminAPI.SpawnItem(Enums.ItemType.Ore, Planet.EntitasContext);

        // Admin API Add Items
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementTool, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.PlacementToolBack, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.WaterBottle, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.MajestyPalm, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.SagoPalm, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.DracaenaTrifasciata, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.HarvestTool, Planet.EntitasContext);
        Admin.AdminAPI.AddItem(inventoryManager, InventoryID, Enums.ItemType.ScannerTool, Planet.EntitasContext);
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
        ref var tileMap = ref Planet.TileMap;
        System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

        float spawnHeight = tileMap.MapSize.Y - 2;

        Player = Planet.AddPlayer(new Vec2f(2, 2));
        PlayerID = Player.agentID.ID;

        Planet.AddMech(new Vec2f(4, 2), Mech.MechType.Planter);
        Planet.AddMech(new Vec2f(8, 2), Mech.MechType.Planter);
        Planet.AddMech(new Vec2f(12, 2), Mech.MechType.Planter);

        Planet.AddMech(new Vec2f(4, 4), Mech.MechType.Light);
        Planet.AddMech(new Vec2f(8, 4), Mech.MechType.Light);
        Planet.AddMech(new Vec2f(12, 4), Mech.MechType.Light);

        Planet.AddUIText("SampleText", new Vec2f(-250.67f, 94.3f), new Vec2f(200, 120));
    }
}
