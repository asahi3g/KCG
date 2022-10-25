using UnityEngine;
using System;

public class GameResources
{
    private static bool IsInitialized = false; 

    public static void Initialize()
    {
        if (!IsInitialized)
        {
            long beginTime = DateTime.Now.Ticks;

            IsInitialized = true;

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

            Debug.Log("2d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");
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
        GameState.ParticleCreationApi.InitializeResources();
    }

    private static void CreateParticleEmitters()
    {
        GameState.ParticleCreationApi.InitializeEmitterResources();
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
}
