using Enums.PlanetTileMap;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Utility;

namespace PlanetTileMap
{

    public class TileMapRenderer
    {
        public FrameMesh[] LayerMeshes;
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        public static bool TileCollisionDebugging = false;

        UnityEngine.Color[] lights;

        public void Initialize(Material material, Transform transform, int drawOrder)
        {
            LayerMeshes = new FrameMesh[LayerCount];


            for (int i = 0; i < LayerCount; i++)
            {
                LayerMeshes[i] = new FrameMesh(("layerMesh" + i), material, transform,
                    GameState.TileSpriteAtlasManager.GetSpriteAtlas(0), drawOrder + i);
            }


            lights = new UnityEngine.Color[64];
            for (int i = 0; i < 64; i++)
            {
                for (int k = 0; k < 32; k++)
                {
                    var color = new Color(
                    UnityEngine.Random.Range(0.0f, 1.0f),
                    UnityEngine.Random.Range(0.0f, 1.0f),
                    UnityEngine.Random.Range(0.0f, 1.0f),
                    1
                    );
                    if (Math.Sqrt(color.r * color.r + color.g * color.g + color.b * color.b) < 0.8)
                    {
                        continue;
                    }

                    lights[i] = color;
                    break;
                }
            }

            lights[0] = Color.red;
            lights[1] = Color.blue;
            lights[2] = Color.green;
            //lights[3] = Color.magenta;
            //lights[4] = Color.cyan;
            //lights[5] = Color.gray;
            //lights[6] = Color.white;
            //lights[7] = Color.yellow;

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
            
            ref var planet = ref GameState.Planet;
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
                UpdateFrontLayerMesh2(null, new UnityEngine.Vector3(0, 0, 0));
        }


        private static Vector2 oldBottomLeft;
        float time = 0;
        UnityEngine.GameObject player;
        public void UpdateFrontLayerMesh2(UnityEngine.GameObject player, UnityEngine.Vector3 position)
        {
            this.player = player;
            time += 0.04f;
            if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

            var cam = Camera.main;
            if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

            var bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            var topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
            var topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            var bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));


            LayerMeshes[(int)MapLayerType.Front].Clear();

            ref var planet = ref GameState.Planet;
            int index = 0;
            int light = 0;
            int border = 100;
            for (int y = (int)(bottomLeft.y - border); y < planet.TileMap.MapSize.Y && y <= (topRight.y + border); y++)
            {
                for (int x = (int)(bottomLeft.x - border); x < planet.TileMap.MapSize.X && x <= (bottomRight.x + border); x++)
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

                            var color = new Color(0, 0, 0, 0);
                            float fx = x;
                            float fy = y;
                            if (tile.FrontTileID == TileID.LIGHT)
                            {
                                //color = Color.green;
                                int lightIndex = light % lights.Length;
                                color = lights[lightIndex];
                                color.a = 0.225f;
                                //++light;
                            }
                            if (spriteId >= 0)
                            {
                                {

                                    Vector4 textureCoords = GameState.TileSpriteAtlasManager.GetSprite(spriteId).TextureCoords;

                                    const float width = 1;
                                    const float height = 1;

                                    LayerMeshes[(int)MapLayerType.Front].UpdateUV(textureCoords, (index) * 4);
                                    LayerMeshes[(int)MapLayerType.Front].UpdateVertex((index * 4), fx, fy, width, height, tile.FrontTileID == TileID.LIGHT, color.r, color.g, color.b, color.a, 0);
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

                                    LayerMeshes[(int)MapLayerType.Front].UpdateUV(textureCoords, (index) * 4);
                                    LayerMeshes[(int)MapLayerType.Front].UpdateVertex((index * 4), x, y, width, height, tile.FrontTileID == TileID.LIGHT, color.r, color.g, color.b, color.a, 0);
                                    index++;
                                }
                            }
                        }
                    }
                }
            }


            var frontMesh = LayerMeshes[(int)MapLayerType.Front];
            for (int i = 0; i < frontMesh.lightPositions.Count; i++)
            {
                var pos = frontMesh.lightPositions[i];
                //pos.x += MathF.Sin(time) * UnityEngine.Random.Range(0, i);
                //pos.y += MathF.Cos(time) * UnityEngine.Random.Range(0, i);
                frontMesh.lightPositions[i] = pos;
            }

            frontMesh.SetLightPosition(frontMesh.obj.transform);

            if (player != null)
            {
                var meshRenderer = player.GetComponentsInChildren<UnityEngine.SkinnedMeshRenderer>()[0];
                meshRenderer.sharedMaterial.SetVector("_KCG_Material", new Vector4(1, 0, 0, 0));
                FrameMesh.SetLightPosition(meshRenderer.sharedMaterial, frontMesh.lightPositions, frontMesh.lightColors, frontMesh.obj.transform);
            }

            var postProcess = Camera.main.GetComponentInChildren<PostProcess>();
            FrameMesh.SetLightPosition(postProcess.compositeMaterial, frontMesh.lightPositions, frontMesh.lightColors, frontMesh.obj.transform);

            var tt = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;
            //postProcess.compositeMaterial.SetMatrix("_KCG_ViewProjection", t);
            postProcess.compositeMaterial.SetMatrix("_KCG_ViewProjection", tt);
            postProcess.compositeMaterial.SetVector("_KCG_BottomLeft", new Vector4(bottomLeft.x, bottomLeft.y, 0, 0));
            postProcess.compositeMaterial.SetVector("_KCG_BottomRight", new Vector4(bottomRight.x - bottomLeft.x, topLeft.y - bottomLeft.y, 0, 0));
            postProcess.compositeMaterial.SetVector("_KCG_TopLeft", new Vector4(topLeft.x, topLeft.y, 0, 0));
            postProcess.compositeMaterial.SetVector("_KCG_TopRight", new Vector4(topRight.x, topRight.y, 0, 0));
        }


        Vector2 oldLightPos;

        public static Vector2 snap(Vector2 p)
        {
            p = Camera.main.WorldToScreenPoint(p);

            p.x = (int)p.x;
            p.y = (int)p.y;

            p = Camera.main.ScreenToWorldPoint(p);
            return p;
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

            ref var planet = ref GameState.Planet;
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
