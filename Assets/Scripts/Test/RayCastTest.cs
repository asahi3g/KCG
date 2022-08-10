using System;
using Enums.Tile;
using KMath;
using Planet;
using UnityEngine;

public class SquareWithRenderer : MonoBehaviour
{
    // Renderer
    public LineRenderer lineRenderer;
    public Shader       shader;
    public Material     material;
    
    // Current color of square
    public Color color;
    
    // Used by renderer
    public Vector3[] corners   = new Vector3[4];
    public const float LineThickness = 0.1f;
    
    public AABox2D Box;
    public bool isDraggable;
    public bool isDraggableStarted;
    
    public void Init()
    {
        // Initialize test shader, material, and renderer
        shader                 = Shader.Find("Hidden/Internal-Colored");
        material               = new Material(shader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        lineRenderer               = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material      = material;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop          = true;

        lineRenderer.startWidth    = lineRenderer.endWidth = LineThickness;

        lineRenderer.sortingOrder = 10;

        corners[0]             = new Vector3();
        corners[1]             = new Vector3();
        corners[2]             = new Vector3();
        corners[3]             = new Vector3();

        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        material.SetInt("_ZWrite", 0);
        material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
    }
    
    void Update() 
    {
        corners[0].x = Box.xmin + LineThickness;
        corners[1].x = Box.xmin + LineThickness;
        corners[2].x = Box.xmax;
        corners[3].x = Box.xmax;

        corners[0].y = Box.ymax;
        corners[1].y = Box.ymin + LineThickness;
        corners[2].y = Box.ymin + LineThickness;
        corners[3].y = Box.ymax;

        corners[0].z = -0.1f;
        corners[1].z = -0.1f;
        corners[2].z = -0.1f;
        corners[3].z = -0.1f;

        lineRenderer.SetPositions(corners);
        lineRenderer.positionCount = 4;

        lineRenderer.startColor = lineRenderer.endColor = color;
    }
}

public class LineWithRenderer : MonoBehaviour
{
    public Line2D Line;
    
    // Renderer
    public LineRenderer lineRenderer;
    public Shader       shader;
    public Material     material;
    
    // Current color of square
    public Color color;
    
    // Used by renderer
    public Vector3[] corners   = new Vector3[2];
    public const float LineThickness = 0.1f;

    void Start() 
    {
        // Initialize test shader, material, and renderer
        shader                 = Shader.Find("Hidden/Internal-Colored");
        material               = new Material(shader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
        lineRenderer               = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material      = material;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop          = true;

        lineRenderer.startWidth    = lineRenderer.endWidth = LineThickness;

        lineRenderer.sortingOrder = 10;

        corners[0]             = new Vector3();
        corners[1]             = new Vector3();

        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        material.SetInt("_ZWrite", 0);
        material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
    }
    
    void Update()
    {
        corners[0].x = Line.A.X + LineThickness;
        corners[1].x = Line.B.X + LineThickness;

        corners[0].y = Line.A.Y;
        corners[1].y = Line.B.Y + LineThickness;

        corners[0].z = -0.1f;
        corners[1].z = -0.1f;

        lineRenderer.SetPositions(corners);
        lineRenderer.positionCount = 2;

        lineRenderer.startColor = lineRenderer.endColor = color;
    }
}

public class RayCastTest : MonoBehaviour
{
    public GameObject Grid;
    public SquareWithRenderer squareWithRenderer;
    public LineWithRenderer lineWithRenderer;
    
    public PlanetState Planet;
    public Material Material;
    public Vec2i mapSize = new(32, 32);
    
    // Start is called before the first frame update

    void Start()
    {
        Camera.main.gameObject.transform.position = new Vector3(mapSize.X / 2f, mapSize.Y / 2f, -10);
        
        squareWithRenderer = CreateSquare(new Vec2f(2f, 2f), new Vec2f(2f, 4f), Color.green, true);

        var ray = new GameObject("Ray");
        lineWithRenderer = ray.AddComponent<LineWithRenderer>();
        lineWithRenderer.color = Color.red;
        
        GameResources.Initialize();
        Planet = new PlanetState();
        Planet.Init(mapSize);
        Planet.InitializeSystems(Material, transform);
        
        GenerateMap();
    }

    private void Update()
    {
        if(squareWithRenderer.isDraggable)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Input.GetMouseButton(0) && Collisions.Collisions.PointOverlapRect(mouse.x, mouse.y, squareWithRenderer.Box.xmin, squareWithRenderer.Box.xmax, squareWithRenderer.Box.ymin, squareWithRenderer.Box.ymax)
               || squareWithRenderer.isDraggableStarted)
            {
                squareWithRenderer.isDraggableStarted = true;
                squareWithRenderer.Box = new AABox2D(new Vec2f(mouse.x - 1f, mouse.y - 2f), new Vec2f(2f, 4f));
            }
            
            if (!Input.GetMouseButton(0))
            {
                squareWithRenderer.isDraggableStarted = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Debug.Log($"{(int)mouse.x}, {(int)mouse.y}");
        }

        lineWithRenderer.Line.A = squareWithRenderer.Box.center;

        var coordinates = Collisions.RayCastCheck.GetRayCoordinates((Vec2i) lineWithRenderer.Line.A, (Vec2i) lineWithRenderer.Line.B);
        
        RegenerateMap();
        
        Planet.Update(Time.deltaTime, Material, transform);
    }

    SquareWithRenderer CreateSquare(Vec2f position, Vec2f size, Color color, bool isDraggable, string name = "Square")
    {
        var square = new GameObject(name);
        var sRenderer = square.AddComponent<SquareWithRenderer>();
        sRenderer.Init();
        sRenderer.Box = new AABox2D(position, size);
        sRenderer.color = color;
        sRenderer.isDraggable = isDraggable;

        return sRenderer;
    }
    
    void GenerateMap()
    {
        CreateSquare(new Vec2f(0f, 0f),  new Vec2f(16f, 16f), Color.white, false, "Chunk_1").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(16f, 0f), new Vec2f(16f, 16f), Color.white, false, "Chunk_2").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(0f, 16f), new Vec2f(16f, 16f), Color.white, false, "Chunk_3").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(16f, 16f),new Vec2f(16f, 16f), Color.white, false, "Chunk_4").lineRenderer.sortingOrder = 11;

        Grid = new GameObject("Grid");

        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                CreateSquare(new Vec2f(x, y), new Vec2f(1f, 1f), Color.gray, false, $"tile_{x}_{y}").gameObject.transform.SetParent(Grid.transform);
            }
        }
        
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
}
