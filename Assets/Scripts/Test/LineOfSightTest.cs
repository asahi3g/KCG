//imports UnityEngine

using Collisions;
using Enums.PlanetTileMap;
using KMath;
using KMath.Random;
using System;

public class LineOfSightTest : UnityEngine.MonoBehaviour
{
    [UnityEngine.SerializeField] UnityEngine.Material Material;

    public CircleSectorRenderDebug CircleSector;
    public AgentEntity Player;
    
    UnityEngine.Color standard = new(1.0f, 1.0f, 1.0f, 0.6f);
    UnityEngine.Color hitColor = new(1.0f, 0.0f, 0.0f, 0.6f);
    Vec2i mapSize = new(64, 32);
    float theta = 0;
    bool mousePressed = false;
    bool PressedW = false;
    bool PressedS = false;
    bool PressedD = false;
    bool PressedA = false;

    void Start()
    {
        UnityEngine.Debug.Log("Click to change position and direction of segment");
        UnityEngine.Debug.Log("If segment intersect with agent it turns red.");

        UnityEngine.GameObject circleSegment = new UnityEngine.GameObject("CircleSegment");

        CircleSector = circleSegment.AddComponent<CircleSectorRenderDebug>();

        CircleSector.angle = 40.0f;
        CircleSector.radius = 6.0f;
        CircleSector.color = standard;
        GameResources.Initialize();

        ref var planet = ref GameState.Planet;
        planet.Init(mapSize);
        planet.InitializeSystems(Material, transform);
        Player = planet.AddAgent(new Vec2f(mapSize.X / 2, mapSize.Y / 2), Enums.AgentType.FlyingSlime, 0);
        Player.AddAgentsLineOfSight(new CircleSector());

        GenerateMap();
    }

    private void Update()
    {
        Vec2f pos = Player.agentPhysicsState.Position; 
        pos += Player.agentSprite2D.Size / 2.0f;
        CircleSector.SetPos(pos);

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
            mousePressed = true;
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.Mouse0))
            mousePressed = false;
        
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.W))
            PressedW = true;
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.W))
            PressedW = false;

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.S))
            PressedS = true;
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.S))
            PressedS = false;

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.D))
            PressedD = true;
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.D))
            PressedD = false;

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.A))
            PressedA = true;
        if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.A))
            PressedA = false;

        Vec2f direction = Vec2f.Zero;
        if (PressedW)
            direction = new Vec2f(0f, 1f);
        if (PressedS)
            direction = new Vec2f(0f, -1f);
        if (PressedD)
            direction = new Vec2f(1f, 0f);
        if (PressedA)
            direction = new Vec2f(-1f, 0f);

        const float SPEED = 10.0F;
        Player.agentPhysicsState.Acceleration =  direction * SPEED / Physics.Constants.TimeToMax;

        if (mousePressed)
        {
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            Vec2f end = new Vec2f(worldPosition.x, worldPosition.y);
            Vec2f dir = end - pos;
            float newTheta = UnityEngine.Mathf.Atan2(dir.Y, dir.X) * UnityEngine.Mathf.Rad2Deg;
            CircleSector.Rotate(newTheta - theta);
            theta = newTheta;
            CircleSector.radius = dir.Magnitude;
        }
        
        ref var planet = ref GameState.Planet;
        planet.Update(UnityEngine.Time.deltaTime, Material, transform);

        Vec2f sectorDir = new Vec2f(MathF.Cos(theta * UnityEngine.Mathf.Deg2Rad), MathF.Sin(theta * UnityEngine.Mathf.Deg2Rad));

        ref CircleSector circleSector = ref Player.agentsLineOfSight.ConeSight;
        circleSector.Radius = CircleSector.radius;
        circleSector.Fov = CircleSector.angle * UnityEngine.Mathf.Deg2Rad;
        circleSector.StartPos = pos;
        circleSector.Dir = sectorDir.Normalized;

        var agentList = planet.AgentList;

        for (int i = 0; i < agentList.Length; i++)
        {
            AgentEntity entity = agentList.Get(i);
            if (entity.agentID.ID == Player.agentID.ID) continue;

            bool intersect = LineOfSight.CanSee(Player.agentID.ID, entity.agentID.ID);
            if (intersect)
            {
                CircleSector.color = hitColor;
                return;
            }
        }

        CircleSector.color = standard;
    }

    private void OnDrawGizmos()
    {
        GameState.Planet.DrawDebug();
    }

    void GenerateMap()
    {
        DateTime date = DateTime.Now;
        int seed = date.Year * 10000 + date.Month * 100
            + date.Day + date.Hour + date.Minute + date.Second;

        XorShift64Star random = new XorShift64Star();
        random.SetSeed((ulong)seed);
        
        ref var planet = ref GameState.Planet;
        ref var tileMap = ref planet.TileMap;

        for (int j = tileMap.MapSize.Y - 1; j >= 0; j--)
        {
            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                int rand = (int)(random.GetUInt32() % 100);

                int y = tileMap.MapSize.Y - 1 - j;

                if (rand <= 10 && !(y == mapSize.Y / 2 && i == mapSize.X / 2))
                    tileMap.SetFrontTile(i, y, TileID.Moon);
                else 
                {
                    tileMap.SetFrontTile(i, y, TileID.Air);
                    tileMap.SetBackTile(i, y, TileID.Glass);
                }

                if (rand >= 98)
                {
                    planet.AddAgent(new Vec2f(i, tileMap.MapSize.Y - 1 - j), 
                        Enums.AgentType.FlyingSlime);
                }
            }
        }
    }
}
