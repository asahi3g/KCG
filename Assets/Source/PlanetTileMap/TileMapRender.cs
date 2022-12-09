using Enums.PlanetTileMap;
using System;
using UnityEngine;
using Utility;

namespace PlanetTileMap
{

    public class TileMapRenderer
    {
        FrameMesh[] LayerMeshes;
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        public static bool TileCollisionDebugging = false;

        public void Initialize(Material material, Transform transform, int drawOrder)
        {
            LayerMeshes = new FrameMesh[LayerCount];

            for (int i = 0; i < LayerCount; i++)
            {
                LayerMeshes[i] = new FrameMesh(("layerMesh" + i), material, transform,
                    GameState.TileSpriteAtlasManager.GetSpriteAtlas(0), drawOrder + i);
            }
        }

        public void UpdateMidLayerMesh()
        {
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            LayerMeshes[(int)MapLayerType.Mid].Clear();

            if (TileCollisionDebugging)
                return;

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
            
            var planet = GameState.Planet;
            int index = 0;
            for (int y = (int)(bottomLeft.y - 10); y < planet.TileMap.MapSize.Y && y <= (topRight.y + 10); y++)
            {
                for (int x = (int)(bottomLeft.x - 10); x < planet.TileMap.MapSize.X && x <= (bottomRight.x + 10); x++)
                {
                    if (x >= 0 && y >= 0)
                    {
                        ref var tile = ref planet.TileMap.GetTile(x, y);

                        if (tile.MidTileID == TileID.Error ||
                        tile.MidTileID == TileID.Air ||
                        tile.MidTileID == TileID.FirstNonEmptyTile) continue;

                        if (tile.MidTileSpriteID >= 0)
                        {
                            ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.MidTileID);
                            var spriteId = tile.MidTileSpriteID;

                            if (spriteId >= 0)
                            {
                                {
                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(spriteId).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Mid].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Mid].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }

                                if (tile.DrawType == TileDrawType.Composited)
                                {
                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(tile.CompositeTileSpriteID).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                   /* if (!ObjectMesh.isOnScreen(x, y))
                                        continue;*/

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Mid].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Mid].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }
                            }
                        }


                    }
                }
            }
        }

        public void UpdateFrontLayerMesh()
        {
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

            LayerMeshes[(int)MapLayerType.Front].Clear();

            var planet = GameState.Planet;
            int index = 0;
            for (int y = (int)(bottomLeft.y - 10); y < planet.TileMap.MapSize.Y && y <= (topRight.y + 10); y++)
            {
                for (int x = (int)(bottomLeft.x - 10); x < planet.TileMap.MapSize.X && x <= (bottomRight.x + 10); x++)
                {
                    if (x >= 0 && y >= 0)
                    {

                        ref var tile = ref planet.TileMap.GetTile(x, y);

                        if (tile.FrontTileID == TileID.Error ||
                        tile.FrontTileID == TileID.Air ||
                        tile.FrontTileID == TileID.FirstNonEmptyTile) continue;

                        if (tile.FrontTileSpriteID >= 0)
                        {

                            ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);
                            var spriteId = tile.FrontTileSpriteID;

                            if (spriteId >= 0)
                            {
                                {

                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(spriteId).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Front].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Front].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
#if DEBUG
                                    if (TileCollisionDebugging)
                                        continue;
#endif
                                }

                                if (tile.DrawType == TileDrawType.Composited)
                                {
                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(tile.CompositeTileSpriteID).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    /*if (!ObjectMesh.isOnScreen(x, y))
                                        continue;*/

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Front].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Front].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdateBackLayerMesh()
        {
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            LayerMeshes[(int)MapLayerType.Back].Clear();

            if (TileCollisionDebugging)
                return;

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

            var planet = GameState.Planet;
            int index = 0;
            for (int y = (int)(bottomLeft.y - 10); y < planet.TileMap.MapSize.Y && y <= (topRight.y + 10); y++)
            {
                for (int x = (int)(bottomLeft.x - 10); x < planet.TileMap.MapSize.X && x <= (bottomRight.x + 10); x++)
                {
                    if (x >= 0 && y >= 0)
                    {
                        ref var tile = ref planet.TileMap.GetTile(x, y);

                        if (tile.BackTileID == TileID.Error ||
                        tile.BackTileID == TileID.Air ||
                        tile.BackTileID == TileID.FirstNonEmptyTile) continue;

                        if (tile.BackTileSpriteID >= 0)
                        {

                            ref TileProperty tileProperty = ref GameState.TileCreationApi.GetTileProperty(tile.BackTileID);
                            var spriteId = tile.BackTileSpriteID;

                            if (spriteId >= 0)
                            {
                                {
                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(spriteId).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Back].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Back].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }

                                if (tile.DrawType == TileDrawType.Composited)
                                {
                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(tile.CompositeTileSpriteID).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    // Update UVs
                                    LayerMeshes[(int)MapLayerType.Back].UpdateUV(textureCoords, (index) * 4);
                                    // Update Vertices
                                    LayerMeshes[(int)MapLayerType.Back].UpdateVertex((index * 4), x, y, width, height);
                                    index++;
                                }
                            }
                        }


                    }
                }
            }
        }

      

        public void DrawLayer(MapLayerType planetLayer)
        {
            GameState.Renderer.DrawFrame(ref LayerMeshes[(int)planetLayer], GameState.TileSpriteAtlasManager.GetSpriteAtlas(0));
        }
    }
}
