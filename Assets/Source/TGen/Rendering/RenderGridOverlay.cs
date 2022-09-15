using UnityEngine;
using Enums.Tile;
using KMath;
using Item;
using Animancer;
using HUD;
using PlanetTileMap;
using Planet;
using Mech;
using System.Collections.Generic;

namespace TGen
{
    public class RenderGridOverlay
    {
        public Utility.FrameMesh Mesh;

        private Color gridColor = new Color(0.5F, 0.5F, 0.5F, 0.5F);

        private float gridThickness = 0.1F;

        public void Initialize(Material material, Transform transform, int w, int h, int drawOrder = 0)
        {          
            Mesh = new Utility.FrameMesh("GridOverlayGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), drawOrder);

            var mr = Mesh.obj.GetComponent<MeshRenderer>();
            mr.sharedMaterial.color = gridColor;

            var mf = Mesh.obj.GetComponent<MeshFilter>();
            var mesh = mf.sharedMesh;

            List<int> triangles = new List<int>();
            List<Vector3> verticies = new List<Vector3>();

            for (int x = 0; x < w; x++)
            {
                Vec2f topLeft = new Vec2f(x, h - 1F);
                Vec2f BottomLeft = new Vec2f(x, 0);
                Vec2f BottomRight = new Vec2f(x + gridThickness, 0);
                Vec2f TopRight = new Vec2f(x + gridThickness, h - 1F);

                var p0 = new Vector3(BottomLeft.X, BottomLeft.Y, 0);
                var p1 = new Vector3(TopRight.X, TopRight.Y, 0);
                var p2 = new Vector3(topLeft.X, topLeft.Y, 0);
                var p3 = new Vector3(BottomRight.X, BottomRight.Y, 0);

                verticies.Add(p0);
                verticies.Add(p1);
                verticies.Add(p2);
                verticies.Add(p3);

                triangles.Add(verticies.Count - 4);
                triangles.Add(verticies.Count - 2);
                triangles.Add(verticies.Count - 3);
                triangles.Add(verticies.Count - 4);
                triangles.Add(verticies.Count - 3);
                triangles.Add(verticies.Count - 1);
            }
            
            for (int y = 0; y < h; y++)
            {
                Vec2f topLeft = new Vec2f(0, y + gridThickness);
                Vec2f BottomLeft = new Vec2f(0, y);
                Vec2f BottomRight = new Vec2f(w - 1F, y);
                Vec2f TopRight = new Vec2f(w - 1F, y + gridThickness);

                var p0 = new Vector3(BottomLeft.X, BottomLeft.Y, 0);
                var p1 = new Vector3(TopRight.X, TopRight.Y, 0);
                var p2 = new Vector3(topLeft.X, topLeft.Y, 0);
                var p3 = new Vector3(BottomRight.X, BottomRight.Y, 0);

                verticies.Add(p0);
                verticies.Add(p1);
                verticies.Add(p2);
                verticies.Add(p3);

                triangles.Add(verticies.Count - 4);
                triangles.Add(verticies.Count - 2);
                triangles.Add(verticies.Count - 3);
                triangles.Add(verticies.Count - 4);
                triangles.Add(verticies.Count - 3);
                triangles.Add(verticies.Count - 1);
            }

            mesh.SetVertices(verticies);
            mesh.SetTriangles(triangles, 0);
        }

        public int TGenBlockSpriteSheet;
        public int[] TGenIsotypeSprites;

        public void InitializeResources()
        {
            TGenBlockSpriteSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\Test\\testBlocks.png", 32, 32);

            var emptySprite = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\EmptyBlock.png", 32, 32);

            int tileCount = 42;

            TGenIsotypeSprites = new int[tileCount];

            TGenIsotypeSprites[0] = GameState.SpriteAtlasManager.CopySpriteToAtlas(emptySprite, 0, 0, Enums.AtlasType.TGen);

            TGenIsotypeSprites[1] = GameState.SpriteAtlasManager.CopySpriteToAtlas(TGenBlockSpriteSheet, 1, 1, Enums.AtlasType.TGen);

            var row = 3;
            var column = 1;

            for (int i = 2; i < tileCount; i++)
            {
                TGenIsotypeSprites[i] = GameState.SpriteAtlasManager.CopySpriteToAtlas(TGenBlockSpriteSheet, column, row, Enums.AtlasType.TGen);

                column += 2;

                if (column > 8)
                {
                    column = 1;
                    row += 2;
                }
            }
        }

    }
}