using System;
using Enums;
using Enums.PlanetTileMap;
using KMath;
using PlanetTileMap;
using UnityEngine;
using UnityEngine.Events;

public class PlanetRenderer : BaseMonoBehaviour
{
    [TextArea(3, 6)]
    [SerializeField] private string _fileName;
    [SerializeField] private Material _tileMaterial;
    [SerializeField] private Transform _agents;
    [SerializeField] private bool _debug;
    [SerializeField] private Transform _debugParent;
    [SerializeField] private DebugChunk _debugChunk;

    private Planet.PlanetState _planet;
    private Utility.FrameMesh _highlightMesh;

    public Planet.PlanetState GetPlanet() => _planet;

    public class Event : UnityEvent<PlanetRenderer> { }

    public void Initialize(Camera cam, UnityAction<IPlanetCreationResult> onSuccess, UnityAction<IError> onFailed)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            Failed(new ErrorData("Filename is null or empty"));
            return;
        }

        PlanetCreationData data;
        
        try
        {
            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson($"generated-maps/{_fileName}.tmj", "generated-maps/");

            int materialCount = Enum.GetNames(typeof(Enums.MaterialType)).Length;
            int geometryTilesCount = Enum.GetNames(typeof(Enums.TileGeometryAndRotation)).Length;

            TileProperty[][] tiles = new TileProperty[materialCount][];

            for (int i = 0; i < materialCount; i++)
            {
                tiles[i] = new TileProperty[geometryTilesCount];
            }

            TileProperty[] tileProperties = GameState.TileCreationApi.TilePropertyArray;
            int tilePropertiesLength = tileProperties.Length;

            for (int i = 0; i < tilePropertiesLength; i++)
            {
                TileProperty property = tileProperties[i];
                int a = (int) property.MaterialType;
                int b = (int) property.BlockShapeType;
                tiles[a][b] = property;
            }

            int width = ((tileMap.width + 16 - 1) / 16) * 16;
            int height = ((tileMap.height + 16 - 1) / 16) * 16;
            Vec2i size = new Vec2i(width, height);

            Planet.PlanetState planet = GameState.Planet;
            planet.Init(size);
            planet.InitializeSystems(_tileMaterial, transform);
            
            
            for(int j = 0; j < tileMap.height; j++)
            {
                for(int i = 0; i < tileMap.width; i++)
                {
                    int tileIndex = tileMap.layers[0].data[i + ((tileMap.height - 1) - j) * tileMap.width] - 1;
                    if (tileIndex >= 0)
                    {
                        Tiled.TiledMaterialAndShape tileMaterialAndShape = tileMap.GetTile(tileIndex);

                        ref Tile tile = ref planet.TileMap.GetTile(i, j);
                        int t1 = (int) tileMaterialAndShape.Material;
                        int t2 = (int) tileMaterialAndShape.Shape;
                        tile.FrontTileID = tiles[t1][t2].TileID;
                    }
                }
            }

            
            Vector3 scPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, cam.nearClipPlane));
            planet.TileMap.UpdateTileMapPositions((int)scPoint.x, (int)scPoint.y);

            _highlightMesh = new Utility.FrameMesh("HighliterGameObject", _tileMaterial, transform, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);
            
            PlanetTileMap.TileMapGeometry.BuildGeometry(planet.TileMap);

            _planet = planet;
            data = new PlanetCreationData(this, _fileName, tileMap, tiles, size, _planet);
            
            if (_debug)
            {
                DebugTiles(planet.TileMap);
            }
        }
        catch (Exception e)
        {
            Failed(new ErrorData(e));
            return;
        }

        Success(data);
        
        
        void Success(IPlanetCreationResult result)
        {
            if (onSuccess == null) return;
            onSuccess.Invoke(result);
        }

        void Failed(IError error)
        {
#if UNITY_EDITOR
            UnityEditor.Selection.activeGameObject = gameObject;
#endif
            
            if (onFailed == null) return;
            onFailed.Invoke(error);
        }
    }

    public bool CreateAgent(Vec2f position, AgentType agentType, int faction, out AgentRenderer result)
    {
        result = null;
        if (_planet == null)
        {
            Debug.LogWarning("Cannot add character, planet is not initialized, skipping..");
        }
        else
        {
            AgentEntity agent = _planet.AddAgent(position, agentType, faction);
            if (agent.hasAgentModel3D)
            {
                AgentRenderer agentRenderer = agent.agentModel3D.Renderer;
                agentRenderer.transform.parent = _agents;
                agentRenderer.SetAgent(this, agent);
                result = agentRenderer;
            }
        }

        return result != null;
    }

    private void DebugTiles(PlanetTileMap.TileMap tileMap)
    {
        ClearDebugTiles();
        if (tileMap == null) return;

        Chunk[] chunks = tileMap.ChunkArray;
        int length = chunks.Length;
        
        int y = tileMap.MapSize.Y;
        int x = tileMap.MapSize.X;

        for(int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                Enums.PlanetTileMap.TileID tileID = tileMap.GetFrontTileID(i, j);

                if (tileID == TileID.Air)
                {
                    continue;
                }
                
                TileProperty properties = GameState.TileCreationApi.GetTileProperty(tileID);

                DebugChunk debugChunk = Instantiate(_debugChunk, _debugParent, false);
                debugChunk.SetChunk(i, j, properties);
            }
        }
    }

    private void ClearDebugTiles()
    {
        _debugParent.DestroyChildren();
    }
}
