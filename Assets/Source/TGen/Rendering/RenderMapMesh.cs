using Enums.Tile;
using System;
using UnityEngine;
using Utility;

namespace TGen
{
    
    public class RenderMapMesh
    {
        FrameMesh Mesh;
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        public static bool TileCollisionDebugging = false;

        public void Initialize(Material material, Transform transform, int drawOrder)
        {
            Mesh = new FrameMesh(("TGenMesh"), material, transform,
                    GameState.TileSpriteAtlasManager.GetSpriteAtlas(0), drawOrder);           
        }

        public void UpdateMesh(Grid grid)
        {
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            Mesh.Clear();

            if (TileCollisionDebugging)
                return;

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

            int index = 0;
            for (int y = (int)(bottomLeft.y - 10); y < grid.GridTiles.GetLength(1) && y <= (topRight.y + 10); y++)
            {
                for (int x = (int)(bottomLeft.x - 10); x < grid.GridTiles.GetLength(0) && x <= (bottomRight.x + 10); x++)
                {
                    if (!Utility.ObjectMesh.isOnScreen(x, y))
                        continue;

                    if (x >= 0 && y >= 0)
                    {
                        ref var tile = ref grid.GridTiles[x, y];

                        if (tile.TileIsoType > 1)
                        {
                            var spriteId = GetSpriteIDFromIsotype((BlockTypeAndRotation)tile.TileIsoType);

                            if (spriteId >= 0)
                            {
                                {
                                    Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.TGen).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    // Update UVs
                                    Mesh.UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    Mesh.UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }
                            }
                        }


                    }
                }
            }
        }

        public int GetSpriteIDFromIsotype(BlockTypeAndRotation isotype)
        {
            return (int)isotype - 1;
        }

        public void Draw()
        {
            GameState.Renderer.DrawFrame(ref Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.TGen));
        }
    }
}