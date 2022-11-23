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
    [SerializeField] private Transform _characters;

    private Planet.PlanetState _planet;
    private Utility.FrameMesh _highlightMesh;

    public void Initialize(Camera camera, UnityAction<IPlanetCreationResult> onSuccess, UnityAction<IError> onFailed)
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
                ref TileProperty property = ref tileProperties[i];
                tiles[(int) property.MaterialType][(int) property.BlockShapeType] = property;
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
                        TileID tileID = tiles[(int)tileMaterialAndShape.Material][(int)tileMaterialAndShape.Shape].TileID;

                        planet.TileMap.GetTile(i, j).FrontTileID = tileID;
                    }
                }
            }

            
            Vector3 scPoint = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
            planet.TileMap.UpdateTileMapPositions((int)scPoint.x, (int)scPoint.y);
            
            _highlightMesh = new Utility.FrameMesh("HighliterGameObject", _tileMaterial, transform, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);
            
            PlanetTileMap.TileMapGeometry.BuildGeometry(planet.TileMap);

            _planet = planet;
            data = new PlanetCreationData(_fileName, tileMap, tiles, size, _planet);
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

    public bool CreateCharacter(Vec2f position, AgentType agentType, out CharacterRenderer result)
    {
        result = null;
        if (_planet == null)
        {
            Debug.LogWarning("Cannot add character, planet is not initialized, skipping..");
        }
        else
        {
            AgentEntity agent = _planet.AddAgent(position, agentType);
            if (agent.hasAgentModel3D)
            {
                CharacterRenderer c = agent.agentModel3D.GameObject.GetComponent<CharacterRenderer>();
                c.transform.parent = _characters;
                c.SetAgent(agent);
                result = c;
            }
        }

        return result != null;
    }
}
