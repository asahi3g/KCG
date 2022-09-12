using Enums.Tile;
using KMath;
using PlanetTileMap;
using UnityEngine;

namespace Collisions
{
    public class RayRendererDebug : MonoBehaviour
    {
        public void Init(Line2D line)
        {
            Line = line;
            color = Color.green;
        }
        
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

        // Function for ray casting tile grid, that changes ray color if collided with only first not empty tile
        // Works with front tiles
        public void RayTileCollisionCheck(TileMap tileMap)
        {
            var rayCoordinates = RayCastCheck.GetRayCoordinates((Vec2i) Line.A, (Vec2i) Line.B);

            foreach (var coordinate in rayCoordinates)
            {
                var tile = tileMap.GetTile(coordinate.X, coordinate.Y);
                if (tile.FrontTileID != TileID.Air)
                {
                    color = Color.red;
                    return;
                }

                color = Color.green;
            }
        }
    }
}

