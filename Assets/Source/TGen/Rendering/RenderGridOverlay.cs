using UnityEngine;
using KMath;
using System.Collections.Generic;

namespace TGen
{
    public class RenderGridOverlay
    {
        public Utility.FrameMesh Mesh;

        private Color gridColor = new(0.5F, 0.5F, 0.5F, 0.5F);

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

        // Tile CollisionIsotope
        public int FB_R0000Sheet;
        public int FB_R0001Sheet;
        public int FB_R0010Sheet;
        public int FB_R0011Sheet;
        public int FB_R0100Sheet;
        public int FB_R0101Sheet;
        public int FB_R0110Sheet;
        public int FB_R0111Sheet;
        public int FB_R1000Sheet;
        public int FB_R1001Sheet;
        public int FB_R1010Sheet;
        public int FB_R1011Sheet;
        public int FB_R1100Sheet;
        public int FB_R1101Sheet;
        public int FB_R1110Sheet;
        public int FB_R1111Sheet;
        public int EmptyBlockSheet;

        //TGen
        /*public static int TGen_SB_R0,

            // HalfBlock
            TGen_HB_R0,
            TGen_HB_R1,
            TGen_HB_R2,
            TGen_HB_R3,

            //TriangleBlock
            TGen_TB_R0,
            TGen_TB_R1,
            TGen_TB_R2,
            TGen_TB_R3,
            TGen_TB_R4,
            TGen_TB_R5,
            TGen_TB_R6,
            TGen_TB_R7,

            //LBlock
            TGen_LB_R0,
            TGen_LB_R1,
            TGen_LB_R2,
            TGen_LB_R3,
            TGen_LB_R4,
            TGen_LB_R5,
            TGen_LB_R6,
            TGen_LB_R7,

            //HalfTriangleBlock
            TGen_HTB_R0,
            TGen_HTB_R1,
            TGen_HTB_R2,
            TGen_HTB_R3,
            TGen_HTB_R4,
            TGen_HTB_R5,
            TGen_HTB_R6,
            TGen_HTB_R7,

            //QuarterPlatform
            TGen_QP_R0,
            TGen_QP_R1,
            TGen_QP_R2,
            TGen_QP_R3,

            //HalfPlatform
            TGen_HP_R0,
            TGen_HP_R1,
            TGen_HP_R2,
            TGen_HP_R3,

            //FullPlatform
            TGen_FP_R0,
            TGen_FP_R1,
            TGen_FP_R2,
            TGen_FP_R3;*/

        public int TGenBlockSpriteSheet;
        public int[] TGenIsotypeSprites;

        public void InitializeResources()
        {
            FB_R0000Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0000.png", 32, 32);
            FB_R0001Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0001.png", 32, 32);
            FB_R0010Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0010.png", 32, 32);
            FB_R0011Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0011.png", 32, 32);
            FB_R0100Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0100.png", 32, 32);
            FB_R0101Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0101.png", 32, 32);
            FB_R0110Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0110.png", 32, 32);
            FB_R0111Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A0111.png", 32, 32);
            FB_R1000Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1000.png", 32, 32);
            FB_R1001Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1001.png", 32, 32);
            FB_R1010Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1010.png", 32, 32);
            FB_R1011Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1011.png", 32, 32);
            FB_R1100Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1100.png", 32, 32);
            FB_R1101Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1101.png", 32, 32);
            FB_R1110Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1110.png", 32, 32);
            FB_R1111Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\SB_A1111.png", 32, 32);
            EmptyBlockSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\TileCollision\\EmptyBlock.png", 32, 32);

            // TileIsotypes.
            FB_R0000Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0000Sheet, 0, 0, 0);
            FB_R0001Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0001Sheet, 0, 0, 0);
            FB_R0010Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0010Sheet, 0, 0, 0);
            FB_R0011Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0011Sheet, 0, 0, 0);
            FB_R0100Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0100Sheet, 0, 0, 0);
            FB_R0101Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0101Sheet, 0, 0, 0);
            FB_R0110Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0110Sheet, 0, 0, 0);
            FB_R0111Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R0111Sheet, 0, 0, 0);
            FB_R1000Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1000Sheet, 0, 0, 0);
            FB_R1001Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1001Sheet, 0, 0, 0);
            FB_R1010Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1010Sheet, 0, 0, 0);
            FB_R1011Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1011Sheet, 0, 0, 0);
            FB_R1100Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1100Sheet, 0, 0, 0);
            FB_R1101Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1101Sheet, 0, 0, 0);
            FB_R1110Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1110Sheet, 0, 0, 0);
            FB_R1111Sheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(FB_R1111Sheet, 0, 0, 0);
            EmptyBlockSheet = GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas(EmptyBlockSheet, 0, 0, 0);

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