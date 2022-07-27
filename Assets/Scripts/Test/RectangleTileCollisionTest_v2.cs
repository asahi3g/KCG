using System;
using System.Linq;
using Collisions;
using Enums.Tile;
using KMath;
using Planet;
using PlanetTileMap;
using UnityEngine;

public class RectangleTileCollisionTest_v2 : MonoBehaviour 
{
    struct Rect
    {
        public float xmin;
        public float xmax;
        public float ymin;
        public float ymax;
    }
    
    // 16x16 background grid
    public PlanetState Planet;
    public Material Material;
    
    public TestSquare square;
    public TestSquare R0; //not used except tests
    public TestSquare R1;
    public TestSquare R2;

    // Colors for primary test squares
    public Color square_color = new(1.0f, 1.0f, 1.0f, 1.0f);

    // Amount of squares in grid
    public Vec2i mapSize   = new(32, 32);

    // Size of square 1
    public Vec2f square_size = new(5, 5);

    public TestSquare SetSquare(Vec2f center, Vec2f halfSize)
    {
        var square_obj = new GameObject
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

        R1.no_collision_color = Color.yellow;
        R1.color              = Color.yellow;
        
        R2.no_collision_color = Color.magenta;
        R2.color              = Color.magenta;
    }

    void Start()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(mapSize.X / 2f, mapSize.Y / 2f, -10);

        var halfSize1 = new Vec2f(square_size.X / 2f, square_size.Y / 2f);
        var center1 = new Vec2f(halfSize1.X, halfSize1.Y);
        square = SetSquare(center1, halfSize1);
        square.color              = square_color;
        
        var region1_obj = new GameObject
        {
            name = "Test Region 1"
        };
        R1       = region1_obj.AddComponent<TestSquare>();

        var region2_obj = new GameObject
        {
            name = "Test Region 2"
        };
        R2       = region2_obj.AddComponent<TestSquare>();
        SetRegions(square.xmin, square.xmax, square.ymin, square.ymax, new Vec2f(3f, 3f));

        GameResources.Initialize();

        // Generating the map
        Planet = new PlanetState();
        Planet.Init(mapSize);

        Planet.InitializeSystems(Material, transform);
        
        GenerateMap();
    }

    void GenerateMap()
    {
        var halfSize = new Vec2f(16f / 2f, 16f / 2f);
        
        var chunk1Pos = new Vec2f(halfSize.X, halfSize.Y);
        var chunk1 = SetSquare(chunk1Pos, halfSize);
        chunk1.draggable = false;
        chunk1.color              = Color.white;
        
        var chunk2Pos = new Vec2f(halfSize.X + 16f, halfSize.Y);
        var chunk2 = SetSquare(chunk2Pos, halfSize);
        chunk2.draggable = false;
        chunk2.color              = Color.white;
        
        var chunk3Pos = new Vec2f(halfSize.X, halfSize.Y + 16f);
        var chunk3 = SetSquare(chunk3Pos, halfSize);
        chunk3.draggable = false;
        chunk3.color              = Color.white;
        
        var chunk4Pos = new Vec2f(halfSize.X + 16f, halfSize.Y + 16f);
        var chunk4 = SetSquare(chunk4Pos, halfSize);
        chunk4.draggable = false;
        chunk4.color              = Color.white;
        
        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = mapSize.Y - 3; y < mapSize.Y; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = mapSize.X - 3; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = 0; x < mapSize.X - 2; x++)
        {
            for (int y = 0; y < mapSize.Y - 2; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Air);
            }
        }
    }

    void RegenerateMap()
    {
        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = mapSize.Y - 3; y < mapSize.Y; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = mapSize.X - 3; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Glass);
            }
        }
        
        for (int x = 0; x < mapSize.X - 2; x++)
        {
            for (int y = 0; y < mapSize.Y - 2; y++)
            {
                Planet.TileMap.SetFrontTile(x, y, TileID.Air);
            }
        }
    }

    Hit GetCollisionHitAABB_AABB(Vec2f velocity)
    {
        var R1_tiles = TileCollisions.GetTilesRegionBroadPhase(Planet.TileMap, (int)R1.xmin, (int)R1.xmax, (int)R1.ymin, (int)R1.ymax);
        var R2_tiles = TileCollisions.GetTilesRegionBroadPhase(Planet.TileMap, (int)R2.xmin, (int)R2.xmax, (int)R2.ymin, (int)R2.ymax);

        float lowestTime = float.MaxValue;
        Vec2i nearestTilePos = default;

        for (int i = 0; i < R1_tiles.Length; i++)
        {
            var tile = new Rect
            {
                xmin = R1_tiles[i].X,
                xmax = R1_tiles[i].X + 1,
                ymin = R1_tiles[i].Y,
                ymax = R1_tiles[i].Y + 1
            };
            var distance = TileCollisions.CalculateDistanceAABB_AABB(square.xmin, square.xmax, square.ymin, square.ymax,
                tile.xmin, tile.xmax, tile.ymin, tile.ymax, velocity);
            if (distance / velocity.Y < lowestTime)
            {
                lowestTime = distance / velocity.Y;
                nearestTilePos = R1_tiles[i];
            }
        }
        
        for (int i = 0; i < R2_tiles.Length; i++)
        {
            var tile = new Rect
            {
                xmin = R2_tiles[i].X,
                xmax = R2_tiles[i].X + 1,
                ymin = R2_tiles[i].Y,
                ymax = R2_tiles[i].Y + 1
            };
            var distance = TileCollisions.CalculateDistanceAABB_AABB(square.xmin, square.xmax, square.ymin, square.ymax,
                tile.xmin, tile.xmax, tile.ymin, tile.ymax, velocity);
            var newTime = distance / velocity.X;
            if (newTime < lowestTime)
            {
                lowestTime = newTime;
                nearestTilePos = R1_tiles[i];
            }
        }

        return new Hit
        {
            point = (Vec2f) nearestTilePos,
            time = lowestTime
        };
    }

    void Update()
    {
        if(square.draggable) 
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetMouseButton(0) && Collisions.Collisions.PointOverlapRect(mouse.x, mouse.y, square.xmin, square.xmax, square.ymin, square.ymax)) 
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
        var hit = GetCollisionHitAABB_AABB(velocity);

        if (hit.time <= 1)
        {
            Debug.Log(Collisions.Collisions.RectOverlapRect(
                square.xmin + velocity.X, square.xmax + velocity.X,
                square.ymin + velocity.Y, square.ymax + velocity.Y, 
                hit.point.X, hit.point.X + 1, hit.point.Y, hit.point.Y + 1));
        }
        else
        {
            Debug.Log("False");
        }

        Planet.Update(Time.deltaTime, Material, transform);
    }
}
