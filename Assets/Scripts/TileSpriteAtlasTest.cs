using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Planet.Unity
{
    //Note: TileMap should be mostly controlled by GameManager


    //Note(Mahdi): we are just testing and making sure everything is working
    // before we move things out of here
    // there will be things like rendering, collision, TileMap
    // that are not supposed to be here.

    class TileSpriteAtlasTest : MonoBehaviour
    {
        //public string TileMap = "Moonbunker/Moon Bunker.tmx";
        [SerializeField] Material Material;

        public static string BaseDir => Application.streamingAssetsPath;

        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> verticies = new List<Vector3>();

        Vector2 MapOffset = new Vector2(-3.0f, 4.0f);

        static bool InitTiles = false;
        

        public void Start()
        {
            if (!InitTiles)
            {
                LoadSprites();
                InitTiles = true;
            }
        }

        public void Update()
        {
                       //remove all children MeshRenderer
            foreach(var mr in GetComponentsInChildren<MeshRenderer>())
                if (Application.isPlaying)
                    Destroy(mr.gameObject);
                else
                    DestroyImmediate(mr.gameObject);

            DrawSpriteAtlas();
            DrawSprite(2, 1, 1.0f, 1.0f, 0);
            DrawSprite(2, -1, 1.0f, 1.0f, 3);
        }

        // create the sprite atlas for testing purposes
        public void LoadSprites()
        {
            int MetalSlabsTileSheet = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\BuildingBlocks\\Metal\\Slabs\\Tiles_metal_slabs.png", 16, 16);
            int StoneBulkheads = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Blocks\\BuildingBlocks\\Stone\\Bulkheads\\Tiles_stone_bulkheads.png", 16, 16);
            int TilesMoon = 
                        GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Tiles\\Terrains\\Tiles_Moon.png", 16, 16);
            int OreTileSheet = 
            GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Items\\Ores\\Gems\\Hexagon\\gem_hexagon_1.png", 16, 16);


            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(MetalSlabsTileSheet, 0, 0, 0);
            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(MetalSlabsTileSheet, 1, 0, 0);
            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(MetalSlabsTileSheet, 4, 0, 0);
            GameState.TileSpriteAtlasManager.CopyTileSpriteToAtlas16To32(MetalSlabsTileSheet, 5, 0, 0);
        }

        // drawing the sprite atlas
        void DrawSpriteAtlas()
        {
            ref Sprites.SpriteAtlas atlas = ref GameState.TileSpriteAtlasManager.GetSpriteAtlas(0);
            Sprites.Sprite sprite = new Sprites.Sprite();
            sprite.Texture = atlas.Texture;
            sprite.TextureCoords = new Vector4(0, 0, 1, 1);
            Utility.Render.DrawSprite(-3, -1, 
                  atlas.Width, atlas.Height, sprite, Instantiate(Material), transform);
        }

        void DrawSprite(float x, float y, float w, float h, int spriteId)
        {
            var sprite = GameState.TileSpriteAtlasManager.GetSprite(spriteId);

            Utility.Render.DrawSprite(x, y, w, h, sprite, Instantiate(Material), transform);
        }

        public struct R
        {
            public float X;
            public float Y;
            public float W;
            public float H;

            public R(float x, float y, float w, float h)
            {
                X = x;
                Y = y;
                W = w;
                H = h;
            }
        }
        private static R CalcVisibleRect()
        {
            var cam = Camera.main;
            var pos = cam.transform.position;
            var height = 2f * cam.orthographicSize;
            var width = height * cam.aspect;
            var visibleRect = new R(pos.x - width / 2, pos.y - height / 2, width, height);
            return visibleRect;
        }

        // we use this helper function to generate a unity Texture2D
        // from pixels
        private Texture2D CreateTextureFromRGBA(byte[] rgba, int w, int h)
        {

            var res = new Texture2D(w, h, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point
            };

            var pixels = new Color32[w * h];
            for (int x = 0 ; x < w; x++)
            for (int y = 0 ; y < h; y++)
            { 
                int index = (x + y * w) * 4;
                var r = rgba[index];
                var g = rgba[index + 1];
                var b = rgba[index + 2];
                var a = rgba[index + 3];

                pixels[x + y * w] = new Color32((byte)r, (byte)g, (byte)b, (byte)a);
            }
            res.SetPixels32(pixels);
            res.Apply();

            return res;
        }
        
    }
}

