using Enums;
using Enums.PlanetTileMap;
using KMath;
using Planet;
using UnityEngine;

public class Game : Singleton<Game>
{
    [SerializeField] private PlanetRenderer _planet;
    [SerializeField] private Player _player;

    private IPlanetCreationResult _currentPlanet;
    private CharacterRenderer _currentPlayer;
    
    public readonly CharacterRenderer.Event onPlayerCharacterAdded = new CharacterRenderer.Event();

    protected override void Awake()
    {
        base.Awake();
        GameResources.Initialize();
    }

    protected override void Start()
    {
        base.Start();

        _planet.Initialize(_player.GetCamera().GetMain(), OnPlanetCreationSuccess, OnPlanetCreationFailed);

        void OnPlanetCreationSuccess(IPlanetCreationResult result)
        {
            Debug.Log($"Planet creation successful fileName[{result.GetFileName()}] size[{result.GetMapSize()}]");
            _currentPlanet = result;

            // Create player
            if (_planet.CreateCharacter(new Vec2f(10f, 10f), AgentType.Player, out CharacterRenderer characterRenderer))
            {
                _currentPlayer = characterRenderer;
                _player.GetCamera().SetTarget(_currentPlayer.transform, true);
                onPlayerCharacterAdded.Invoke(_currentPlayer);
            }
        }
        
        void OnPlanetCreationFailed(IError error)
        {
            Debug.LogError($"Planet creation failed, reason: {error.GetMessage()}");
        }
    }


    private void Update()
    {
        if (_currentPlanet != null)
        {
            UpdateMainGameLoop(Time.deltaTime, Application.targetFrameRate, 30f, _currentPlanet.GetPlanet());
        }
    }

    public bool GetCurrentPlayerCharacter(out CharacterRenderer character)
    {
        character = _currentPlayer;
        return character != null;
    }

    private void UpdateMainGameLoop(float deltaTime, float targetFrameRate, float targetPhysicsRate, PlanetState planetState)
    {
        float frameTime = 1.0f / targetPhysicsRate;

        /*TimeState.Deficit += deltaTime;

        while (TimeState.Deficit >= frameTime)
        {
            TimeState.Deficit -= frameTime;
            // do a server/client tick right here
            {
                TimeState.TickTime++;


            }

        }*/

        PlanetTileMap.TileMapGeometry.BuildGeometry(planetState.TileMap);

        // check if the sprite atlas teSetTilextures needs to be updated
        for(int type = 0; type < GameState.SpriteAtlasManager.AtlasArray.Length; type++)
        {
            GameState.SpriteAtlasManager.UpdateAtlasTexture(type);
        }

        // check if the tile sprite atlas textures needs to be updated
        for(int type = 0; type < GameState.TileSpriteAtlasManager.Length; type++)
        {
            GameState.TileSpriteAtlasManager.UpdateAtlasTexture(type);
        }

        // calling all the systems we have

        //GameState.InputProcessSystem.Update();
        _player.GetInput().Tick();
        
        // Movement Systems
        GameState.AgentIKSystem.Update(planetState.EntitasContext.agent);
        GameState.AgentProcessPhysicalState.Update(frameTime);
        GameState.AgentMovementSystem.Update();
        GameState.AgentModel3DMovementSystem.Update();
        GameState.ItemMovableSystem.Update();
        GameState.VehicleMovementSystem.UpdateEx();
        GameState.PodMovementSystem.UpdateEx();
        GameState.ProjectileMovementSystem.Update();


        GameState.AgentModel3DAnimationSystem.Update();
        GameState.LootDropSystem.Update();
        GameState.FloatingTextUpdateSystem.Update(frameTime);
        GameState.AnimationUpdateSystem.Update(frameTime);
        GameState.ItemPickUpSystem.Update();
        GameState.ActionSchedulerSystem.Update();
        GameState.ActionCoolDownSystem.Update(deltaTime);
        GameState.ParticleEmitterUpdateSystem.Update();
        GameState.ParticleUpdateSystem.Update();
        GameState.ProjectileProcessState.Update();
        GameState.PodAISystem.Update();
        GameState.VehicleAISystem.Update();

        // Collision systems.
        GameState.AgentProcessCollisionSystem.Update(planetState.EntitasContext.agent);
        GameState.ItemProcessCollisionSystem.Update();
        GameState.ParticleProcessCollisionSystem.Update();
        GameState.ProjectileCollisionSystem.UpdateEx(deltaTime);
        GameState.VehicleCollisionSystem.Update();
        GameState.PodCollisionSystem.Update();
        GameState.MechPlantGrowthSystem.Update();

        GameState.AgentProcessStats.Update();
        GameState.SensorUpdateSystem.Update();
        GameState.BehaviorTreeUpdateSystem.Update();
        GameState.BlackboardUpdatePosition.Update();

        _player.GetCamera().Tick(deltaTime);

        planetState.TileMap.UpdateTileSprites();

        if (GameState.TGenGrid is {Initialized: true})
        {
            GameState.TGenGrid.Update();
            GameState.TGenRenderMapMesh.UpdateMesh(GameState.TGenGrid);
            GameState.TGenRenderMapMesh.Draw();
        }

        // Update Meshes.
        GameState.TileMapRenderer.UpdateBackLayerMesh();
        GameState.TileMapRenderer.UpdateMidLayerMesh();
        GameState.TileMapRenderer.UpdateFrontLayerMesh();
        GameState.ItemMeshBuilderSystem.UpdateMesh();
        GameState.AgentMeshBuilderSystem.UpdateMesh();
        GameState.VehicleMeshBuilderSystem.UpdateMesh();
        GameState.PodMeshBuilderSystem.UpdateMesh();
        GameState.ProjectileMeshBuilderSystem.UpdateMesh();
        GameState.ParticleMeshBuilderSystem.UpdateMesh();
        GameState.MechMeshBuilderSystem.UpdateMesh();

        // Draw Frames.
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Back);
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Mid);
        GameState.TileMapRenderer.DrawLayer(MapLayerType.Front);
        GameState.Renderer.DrawFrame(ref GameState.AgentMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Agent));
        GameState.Renderer.DrawFrame(ref GameState.ItemMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.VehicleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Vehicle));
        GameState.Renderer.DrawFrame(ref GameState.PodMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Vehicle));
        GameState.Renderer.DrawFrame(ref GameState.ProjectileMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.ParticleMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Particle));
        GameState.Renderer.DrawFrame(ref GameState.MechMeshBuilderSystem.Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Mech));

        
        GameState.AgentModel3DMovementSystem.Update();
        GameState.AgentModel3DAnimationSystem.Update();

        GameState.FloatingTextDrawSystem.Draw(10000);

        // Delete Entities.
        GameState.ProjectileDeleteSystem.Update();
    }

    
}
