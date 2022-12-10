//import UnityEngine

using System;
using Collisions;
using CollisionsTest;
using Enums.PlanetTileMap;
using KMath;
using Planet;
using UnityEngine;

public class RectangleTileCollisionTest_v2 : UnityEngine.MonoBehaviour 
{
    struct Rect
    {
        public float xmin;
        public float xmax;
        public float ymin;
        public float ymax;
    }
    
    // 16x16 background grid

    public UnityEngine.Material Material;
    
    public TestSquare square;
    public TestSquare R0; //not used except tests
    public TestSquare R1;
    public TestSquare R2;

    AgentEntity player;

    // Colors for primary test squares
    public UnityEngine.Color square_color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    // Amount of squares in grid
    public Vec2i mapSize   = new Vec2i(32, 32);

    // Size of square 1
    public Vec2f square_size = new Vec2f(5, 5);

    public TestSquare SetSquare(Vec2f center, Vec2f halfSize)
    {
        var square_obj = new UnityEngine.GameObject
        {
            name = "Test square"
        };
        var square       = square_obj.AddComponent<TestSquare>();
        square.xmin      = center.X - halfSize.X;
        square.xmax      = center.X + halfSize.X;
        square.ymin      = center.Y - halfSize.Y;
        square.ymax      = center.Y + halfSize.Y;
        square.draggable = true;

        return square;
    }

    public void SetRegions(float r_xmin, float r_xmax, float r_ymin, float r_ymax, Vec2f velocity)
    {
        var box1_xmin = r_xmin;
        var box1_xmax = r_xmax;
        var box1_ymin = r_ymin;
        var box1_ymax = r_ymax;

        var box2_xmin = r_xmin + velocity.X;
        var box2_xmax = r_xmax + velocity.X;
        var box2_ymin = r_ymin + velocity.Y;
        var box2_ymax = r_ymax + velocity.Y;


        // r - region
        // r1 - region1 and etc.

        int r1_xmin = velocity.X >= 0f ? (int)Math.Floor(box1_xmin) : (int)Math.Floor(box2_xmin);       //round down
        int r1_xmax = velocity.X >= 0f ? (int)Math.Ceiling(box2_xmax) : (int)Math.Ceiling(box1_xmax);   //round up
        int r1_ymin = velocity.Y >= 0f ? (int)Math.Floor(box1_ymax) : (int)Math.Floor(box2_ymin);       //round down
        int r1_ymax = velocity.Y >= 0f ? (int)Math.Ceiling(box2_ymax) : (int)Math.Ceiling(box1_ymin);   //round up

        int r2_xmin = velocity.X >= 0f ? (int)Math.Floor(box1_xmax) : (int)Math.Floor(box2_xmin);       //round down
        int r2_xmax = velocity.X >= 0f ? (int)Math.Ceiling(box2_xmax) : (int)Math.Ceiling(box1_xmin);   //round up
        // We don't need to test blocks that are inside of region 1 square
        int r2_ymin = velocity.Y >= 0f ? (int)Math.Floor(box1_ymin) : r1_ymax;                          //round down
        int r2_ymax = velocity.Y >= 0f ? r1_ymin : (int)Math.Ceiling(box1_ymax);                        //round up

        R1.xmin = r1_xmin;
        R1.xmax = r1_xmax;
        R1.ymin = r1_ymin;
        R1.ymax = r1_ymax;

        R2.xmin = r2_xmin;
        R2.xmax = r2_xmax;
        R2.ymin = r2_ymin;
        R2.ymax = r2_ymax;

        R1.no_collision_color = UnityEngine.Color.yellow;
        R1.color              = UnityEngine.Color.yellow;
        
        R2.no_collision_color = UnityEngine.Color.magenta;
        R2.color              = UnityEngine.Color.magenta;
    }

    void Start()
    {
        UnityEngine.GameObject.Find("Main Camera").transform.position = new UnityEngine.Vector3(mapSize.X / 2f, mapSize.Y / 2f, -10);

        var halfSize1 = new Vec2f(square_size.X / 2f, square_size.Y / 2f);
        var center1 = new Vec2f(halfSize1.X, halfSize1.Y);
        square = SetSquare(center1, halfSize1);
        square.color              = square_color;
        
        var region1_obj = new UnityEngine.GameObject
        {
            name = "Test Region 1"
        };
        R1       = region1_obj.AddComponent<TestSquare>();

        var region2_obj = new UnityEngine.GameObject
        {
            name = "Test Region 2"
        };
        R2       = region2_obj.AddComponent<TestSquare>();
        SetRegions(square.xmin, square.xmax, square.ymin, square.ymax, new Vec2f(3f, 3f));

        // Generating the map
        var planet = GameState.Planet;
        planet.Init(mapSize);
        player = new AgentEntity();

        var entities = planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.AgentPhysicsState));
        foreach (var entity in entities)
        {
            if (entity.isAgentPlayer)
                player = entity;

        }

        planet.InitializeSystems(Material, transform);

        GenerateMap();
    }

    void GenerateMap()
    {
        var planet = GameState.Planet;
        
        var halfSize = new Vec2f(16f / 2f, 16f / 2f);
        
        var chunk1Pos = new Vec2f(halfSize.X, halfSize.Y);
        var chunk1 = SetSquare(chunk1Pos, halfSize);
        chunk1.draggable = false;
        chunk1.color              = UnityEngine.Color.white;
        
        var chunk2Pos = new Vec2f(halfSize.X + 16f, halfSize.Y);
        var chunk2 = SetSquare(chunk2Pos, halfSize);
        chunk2.draggable = false;
        chunk2.color              = UnityEngine.Color.white;
        
        var chunk3Pos = new Vec2f(halfSize.X, halfSize.Y + 16f);
        var chunk3 = SetSquare(chunk3Pos, halfSize);
        chunk3.draggable = false;
        chunk3.color              = UnityEngine.Color.white;
        
        var chunk4Pos = new Vec2f(halfSize.X + 16f, halfSize.Y + 16f);
        var chunk4 = SetSquare(chunk4Pos, halfSize);
        chunk4.draggable = false;
        chunk4.color              = UnityEngine.Color.white;
        
        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = mapSize.Y - 3; y < mapSize.Y; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = mapSize.X - 3; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = 0; x < mapSize.X - 2; x++)
        {
            for (int y = 0; y < mapSize.Y - 2; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Air);
            }
        }
    }
    void RegenerateMap()
    {
        var planet = GameState.Planet;
        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = mapSize.Y - 3; y < mapSize.Y; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = mapSize.X - 3; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = 0; x < mapSize.X - 2; x++)
        {
            for (int y = 0; y < mapSize.Y - 2; y++)
            {
                planet.TileMap.SetFrontTile(x, y, TileID.Air);
            }
        }
    }

    void Update()
    {
        if(square.draggable) 
        {
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            if(UnityEngine.Input.GetMouseButton(0) && Collisions.Collisions.PointOverlapRect(mouse.x, mouse.y, square.xmin, square.xmax, square.ymin, square.ymax)) 
            {
                square.xmin = mouse.x - square_size.X / 2f;
                square.xmax = mouse.x + square_size.X / 2f;
                square.ymin = mouse.y - square_size.Y / 2f;
                square.ymax = mouse.y + square_size.Y / 2f;
                SetRegions(square.xmin, square.xmax, square.ymin, square.ymax, new Vec2f(3f, 3f));
            }
        }

        // Reset colors for all squares
        RegenerateMap();

        var velocity = new Vec2f(3f, 3f);
        var square_halfsize = new Vec2f((square.xmax - square.xmin) / 2f, (square.ymax - square.ymin) / 2f);
        var hit = TileCollisions.GetCollisionHitAABB_AABB(square.xmin, square.xmax, square.ymin, square.ymax, velocity);
        var circleHit = CircleTileMapSweepCollision.GetCollisionHitCircle_AABB(2.5f, new Vec2f(square.xmin + square_halfsize.X, square.ymin + square_halfsize.Y), velocity);

        if (hit.time <= 1)
        {
            UnityEngine.Debug.Log(Collisions.Collisions.RectOverlapRect(
                square.xmin + velocity.X, square.xmax + velocity.X,
                square.ymin + velocity.Y, square.ymax + velocity.Y, 
                hit.point.X, hit.point.X + 1, hit.point.Y, hit.point.Y + 1));
        }
        else
        {
            UnityEngine.Debug.Log("False");
        }

        GameState.Planet.Update(UnityEngine.Time.deltaTime);
    }
}
