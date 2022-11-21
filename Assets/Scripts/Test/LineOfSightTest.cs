//imports UnityEngine

using Collisions;
using Enums.PlanetTileMap;
using KMath;
using KMath.Random;
using System;
using UnityEngine;

public class LineOfSightTest : MonoBehaviour
{
    [SerializeField] Material Material;

    public CircleSectorRenderDebug CircleSector;
    public AgentEntity Player;
    
    UnityEngine.Color standard = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.6f);
    UnityEngine.Color hitColor = new UnityEngine.Color(1.0f, 0.0f, 0.0f, 0.6f);
    Vec2i mapSize = new Vec2i(64, 32);
    float theta = 0;
    bool mousePressed = false;
    bool PressedW = false;
    bool PressedS = false;
    bool PressedD = false;
    bool PressedA = false;

    void Start()
    {
        Debug.Log("Click to change position and direction of segment");
        Debug.Log("If segment intersect with agent it turns red.");

        GameObject circleSegment = new GameObject("CircleSegment");

        CircleSector = circleSegment.AddComponent<CircleSectorRenderDebug>();

        CircleSector.angle = 180f;
        CircleSector.radius = 6.0f;
        CircleSector.color = standard;
        GameResources.Initialize();

        ref var planet = ref GameState.Planet;
        planet.Init(mapSize);
        planet.InitializeSystems(Material, transform);
        Player = planet.AddAgent(new Vec2f(mapSize.X / 2, mapSize.Y / 2), Enums.AgentType.FlyingSlime, 0);
        Player.ReplaceAgentsLineOfSight(new CircleSector());

        GenerateMap();
    }

    private void Update()
    {
        Vec2f pos = Player.agentPhysicsState.Position; 
        pos += Player.agentSprite2D.Size / 2.0f;
        CircleSector.SetPos(pos);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            mousePressed = true;
        if (Input.GetKeyUp(KeyCode.Mouse0))
            mousePressed = false;
        
        if (Input.GetKeyDown(KeyCode.W))
            PressedW = true;
        if (Input.GetKeyUp(KeyCode.W))
            PressedW = false;

        if (Input.GetKeyDown(KeyCode.S))
            PressedS = true;
        if (Input.GetKeyUp(KeyCode.S))
            PressedS = false;

        if (Input.GetKeyDown(KeyCode.D))
            PressedD = true;
        if (Input.GetKeyUp(KeyCode.D))
            PressedD = false;

        if (Input.GetKeyDown(KeyCode.A))
            PressedA = true;
        if (Input.GetKeyUp(KeyCode.A))
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
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vec2f end = new Vec2f(worldPosition.x, worldPosition.y);
            Vec2f dir = end - pos;
            float newTheta = Mathf.Atan2(dir.Y, dir.X) * Mathf.Rad2Deg;
            CircleSector.Rotate(newTheta - theta);
            theta = newTheta;
            CircleSector.radius = dir.Magnitude;
        }
        
        ref var planet = ref GameState.Planet;
        planet.Update(Time.deltaTime); 

        Vec2f sectorDir = new Vec2f(MathF.Cos(theta * Mathf.Deg2Rad), MathF.Sin(theta * Mathf.Deg2Rad));

        ref CircleSector circleSector = ref Player.agentsLineOfSight.ConeSight;
        circleSector.Radius = CircleSector.radius;
        circleSector.Fov = CircleSector.angle * Mathf.Deg2Rad;
        circleSector.StartPos = pos;
        circleSector.Dir = sectorDir.Normalized;

        var agentList = planet.AgentList;

        for (int i = 0; i < agentList.Length; i++)
        {
            AgentEntity entity = agentList.Get(i);
            if (entity.agentID.ID == Player.agentID.ID) continue;

            bool intersect = LineOfSight.CanSeeUnalert(Player.agentID.ID, entity.agentID.ID);
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
