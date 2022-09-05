using Collisions;
using Enums.Tile;
using KMath;
using KMath.Random;
using Planet;
using System;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] Material Material;

    public CircleSectorRenderDebug CircleSector;

    PlanetState Planet;
    Color standard = new Color(1.0f, 1.0f, 1.0f, 0.8f);
    Color hitColor = new Color(1.0f, 0.0f, 0.0f, 0.8f);
    Vec2i mapSize = new(64, 32);
    float theta = 0;

    void Start()
    {
        Debug.Log("Click to change position and direction of segment");
        Debug.Log("If segment intersect with agent it turns red.");

        GameObject circleSegment = new GameObject("CircleSegment");

        CircleSector = circleSegment.AddComponent<CircleSectorRenderDebug>();

        CircleSector.angle = 40.0f;
        CircleSector.radius = 6.0f;
        CircleSector.color = standard;

        GameResources.Initialize();

        Planet = new Planet.PlanetState();
        Planet.Init(mapSize);
        Planet.InitializeSystems(Material, transform);

        GenerateMap();
    }

    private void Update()
    {
        Vec2f pos = CircleSector.getPos();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vec2f end = new Vec2f(worldPosition.x, worldPosition.y);
            Vec2f dir = end - pos;
            float newTheta = Mathf.Atan2(dir.Y, dir.X) * Mathf.Rad2Deg;
            CircleSector.transform.RotateAround(new Vector3(pos.X, pos.Y, 0.0f), Vector3.forward, newTheta - theta);
            theta = newTheta;

            CircleSector.radius = dir.Magnitude;
        }
        Planet.Update(Time.deltaTime, Material, transform);

        Vec2f direction = new Vec2f(MathF.Cos(theta), MathF.Sin(theta));

        for (int i = 0; i < Planet.AgentList.Length; i++)
        {
            AgentEntity entity = Planet.AgentList.Get(i);
            Agent.PhysicsStateComponent physicsState = entity.agentPhysicsState;
            Physics.Box2DColliderComponent box2DCollider = entity.physicsBox2DCollider;

            AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);
            bool intersect = LineOfSightTest.AABBIntersectSector(ref entityBoxBorders, CircleSector.radius,
                CircleSector.angle, pos, direction);

            if (intersect)
            {
                CircleSector.color = hitColor;
                return;
            }
        }

        CircleSector.color = standard;
    }

    void GenerateMap()
    {
        var date = DateTime.Now;
        int seed = date.Year * 10000 + date.Month * 100
            + date.Day + date.Hour + date.Minute + date.Second;

        XorShift64Star random = new XorShift64Star();
        random.SetSeed((ulong)seed);

        ref var tileMap = ref Planet.TileMap;

        for (int j = tileMap.MapSize.Y - 1; j >= 0; j--)
        {
            for (int i = 0; i < tileMap.MapSize.X; i++)
            {
                int rand = (int)random.GetUInt32() % 100;

                if (rand >= 98)
                {
                    Planet.AddAgent(new Vec2f(i, tileMap.MapSize.Y - 1 - j), 
                        Enums.AgentType.FlyingSlime);
                }

                tileMap.SetFrontTile(i, tileMap.MapSize.Y - 1 - j, TileID.Air);
                tileMap.SetBackTile(i, tileMap.MapSize.Y - 1 - j, TileID.Moon);
            }
        }
    }
}
