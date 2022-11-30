using UnityEngine;
using KMath;
using System.Collections.Generic;

namespace TGen.DarkGreyBackground
{
    public class RenderGridOverlay
    {
        public Utility.FrameMesh Mesh;

        private Color gridColor = new Color(0.0F, 0.0F, 0.0F, 1.0F);

        private float gridThickness = 0.05F;

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

        // Tile CollisionIsotope
        public int PlanetSheet;
        public int StarSheet;

        public int Planet1;
        public int Planet2;
        public int Planet3;
        public int Planet4;
        public int Planet5;
        public int Planet6;
        public int Planet7;
        public int Planet8;
        public int Planet9;
        public int Planet10;

        public int Star1;
        public int Star2;
        public int Star3;
        public int Star4;
        public int Star5;
        public int Star6;
        public int Star7;
        public int Star8;
        public int Star9;
        public int Star10;

        public void InitializeResources()
        {
            PlanetSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\PlanetBackground\\StarField\\Stars\\galaxy_256x256.png", 32, 32);
            StarSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\PlanetBackground\\StarField\\Stars\\starfield_test_16x16_tiles_8x8_tile_grid_128x128.png", 16, 16);

            // TileIsotypes.
            Planet1 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 5, 0, 0);
            Planet2 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 6, 0, 0);
            Planet3 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 7, 0);
            Planet4 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 5, 4, 0);
            Planet5 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 5, 7, 0);
            Planet6 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 4, 0);
            Planet7 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 6, 0);
            Planet8 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 0, 0);
            Planet9 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 2, 0);
            Planet10 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(PlanetSheet, 4, 3, 0);

            Star1 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 3, 0, 0);
            Star2 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 0, 0, 0);
            Star3 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 2, 0, 0);
            Star4 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 2, 2, 0);
            Star5 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 2, 3, 0);
            Star6 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 3, 1, 0);
            Star7 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 3, 2, 0);
            Star8 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 3, 3, 0);
            Star9 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 0, 1, 0);
            Star10 = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(StarSheet, 0, 2, 0);
        }

    }
}