//imports UnityEngine

using System;

public class GameResources
{
    private static bool isInitialized; 

    public static void Initialize()
    {
        if (!isInitialized)
        {
            long beginTime = DateTime.Now.Ticks;
            
            CreateDropTables();
            InitializeTGenTiles();

            CreateTiles();
            CreateAnimations();
            CreateItems();
            CreateAgents();
            CreateParticles();
            CreateParticleEmitters();
            CreateProjectiles();
            CreateMechs();
            CreateVehicles();

            UnityEngine.Debug.Log("2d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");

            isInitialized = true;
        }
    }


    private static void CreateDropTables()
    {
        GameState.LootTableCreationAPI.InitializeResources();
    }

    private static void InitializeTGenTiles()
    {
        GameState.TGenRenderGridOverlay.InitializeResources();
    }

    private static void CreateTiles()
    {
        GameState.TileCreationApi.InitializeResources();
    }

    private static void CreateAnimations()
    {
        GameState.AnimationManager.InitializeResources();
    }

    public static void CreateItems()
    {
        GameState.ItemCreationApi.InitializeResources();
    }

    private static void CreateAgents()
    {
        GameState.AgentCreationApi.InitializeResources();
    }

    private static void CreateMechs()
    {
        GameState.MechCreationApi.InitializeResources();
    }

    private static void CreateParticles()
    {
        GameState.ParticlePropertiesManager.InitializeResources();
    }

    private static void CreateParticleEmitters()
    {
        GameState.ParticlePropertiesManager.InitializeEmitterResources();
    }

    private static void CreateProjectiles()
    {
        GameState.ProjectileCreationApi.InitializeResources();
    }

    private static void CreateVehicles()
    {
        GameState.VehicleCreationApi.InitializeResources();
        GameState.PodCreationApi.InitializeResources();
    }

    private static void CreatePrefabs()
    {
        GameState.PrefabManager.InitializeResources();
    }
}
