//imports UnityEngine

using Collisions;
using Enums.PlanetTileMap;
using KMath;
using Planet;

public class SquareWithRenderer : UnityEngine.MonoBehaviour
{
    // Renderer
    public UnityEngine.LineRenderer lineRenderer;
    public UnityEngine.Shader       shader;
    public UnityEngine.Material     material;
    
    // Current color of square
    public UnityEngine.Color color;
    
    // Used by renderer
    public UnityEngine.Vector3[] corners   = new UnityEngine.Vector3[4];
    public const float LineThickness = 0.1f;
    
    public AABox2D Box;
    public bool isDraggable;
    public bool isDraggableStarted;
    
    public void Init()
    {
        // Initialize test shader, material, and renderer
        shader                 = UnityEngine.Shader.Find("Hidden/Internal-Colored");
        material               = new UnityEngine.Material(shader)
        {
            hideFlags = UnityEngine.HideFlags.HideAndDontSave
        };
        lineRenderer               = gameObject.AddComponent<UnityEngine.LineRenderer>();
        lineRenderer.material      = material;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop          = true;

        lineRenderer.startWidth    = lineRenderer.endWidth = LineThickness;

        lineRenderer.sortingOrder = 10;

        corners[0]             = new UnityEngine.Vector3();
        corners[1]             = new UnityEngine.Vector3();
        corners[2]             = new UnityEngine.Vector3();
        corners[3]             = new UnityEngine.Vector3();

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

public class RayCastTest : UnityEngine.MonoBehaviour
{
    public UnityEngine.GameObject Grid;
    public SquareWithRenderer squareWithRenderer;
    public RayRendererDebug rayRenderer;


    public UnityEngine.Material Material;
    public Vec2i mapSize = new Vec2i(32, 32);
    
    // Start is called before the first frame update

    void Start()
    {
        UnityEngine.Camera.main.gameObject.transform.position = new UnityEngine.Vector3(mapSize.X / 2f, mapSize.Y / 2f, -10);
        
        squareWithRenderer = CreateSquare(new Vec2f(2f, 2f), new Vec2f(2f, 4f), UnityEngine.Color.green, true);
        squareWithRenderer.lineRenderer.sortingOrder = 12;
        
        var ray = new UnityEngine.GameObject("Ray");
        rayRenderer = ray.AddComponent<RayRendererDebug>();
        rayRenderer.Init(new Line2D(squareWithRenderer.Box.center, new Vec2f(0f, 0f)));

        var planet = GameState.Planet;
        planet.Init(mapSize);
        planet.InitializeSystems(Material, transform);
        
        GenerateMap();
    }

    private void Update()
    {
        if(squareWithRenderer.isDraggable)
        {
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            if(UnityEngine.Input.GetMouseButton(0) && Collisions.Collisions.PointOverlapRect(mouse.x, mouse.y, squareWithRenderer.Box.xmin, squareWithRenderer.Box.xmax, squareWithRenderer.Box.ymin, squareWithRenderer.Box.ymax)
               || squareWithRenderer.isDraggableStarted)
            {
                squareWithRenderer.isDraggableStarted = true;
                squareWithRenderer.Box = new AABox2D(new Vec2f(mouse.x - 1f, mouse.y - 2f), new Vec2f(2f, 4f));
                rayRenderer.Line.A = squareWithRenderer.Box.center;
            }
            
            if (!UnityEngine.Input.GetMouseButton(0))
            {
                squareWithRenderer.isDraggableStarted = false;
            }
        }

        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
        {
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            UnityEngine.Debug.Log($"{(int)mouse.x}, {(int)mouse.y}");
        }
        
        rayRenderer.RayTileCollisionCheck();

        RegenerateMap();
        
        GameState.Planet.Update(UnityEngine.Time.deltaTime);
    }

    SquareWithRenderer CreateSquare(Vec2f position, Vec2f size, UnityEngine.Color color, bool isDraggable, string name = "Square")
    {
        var square = new UnityEngine.GameObject(name);
        var sRenderer = square.AddComponent<SquareWithRenderer>();
        sRenderer.Init();
        sRenderer.Box = new AABox2D(position, size);
        sRenderer.color = color;
        sRenderer.isDraggable = isDraggable;

        return sRenderer;
    }
    
    void GenerateMap()
    {
        var planet = GameState.Planet;
        CreateSquare(new Vec2f(0f, 0f),  new Vec2f(16f, 16f), UnityEngine.Color.white, false, "Chunk_1").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(16f, 0f), new Vec2f(16f, 16f), UnityEngine.Color.white, false, "Chunk_2").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(0f, 16f), new Vec2f(16f, 16f), UnityEngine.Color.white, false, "Chunk_3").lineRenderer.sortingOrder = 11;
        CreateSquare(new Vec2f(16f, 16f),new Vec2f(16f, 16f), UnityEngine.Color.white, false, "Chunk_4").lineRenderer.sortingOrder = 11;

        Grid = new UnityEngine.GameObject("Grid");

        for (int x = 0; x < mapSize.X; x++)
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                CreateSquare(new Vec2f(x, y), new Vec2f(1f, 1f), UnityEngine.Color.gray, false, $"tile_{x}_{y}").gameObject.transform.SetParent(Grid.transform);
            }
        }
        
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
}
