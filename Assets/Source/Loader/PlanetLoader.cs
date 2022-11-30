using System;
using KMath;
using Planet;
using PlanetTileMap;
using Tiled;
using UnityEngine;
using UnityEngine.Events;

public static class PlanetLoader
{


    public static void Load(
        Transform transform,
        string fileName,
        Material tileMaterial,
        Camera camera,
        UnityAction<Result> onSuccess,
        UnityAction<IError> onFailed)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Failed(new ErrorData("Filename is null or empty"));
            return;
        }

        Result data;
        
        try
        {
            Tiled.TiledMap tileMap = Tiled.TiledMap.FromJson($"generated-maps/{fileName}.tmj", "generated-maps/");

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
            planet.InitializeSystems(tileMaterial, transform);
            
            
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

            
            Vector3 scPoint = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
            planet.TileMap.UpdateTileMapPositions((int)scPoint.x, (int)scPoint.y);

            Utility.FrameMesh highlightMesh = new Utility.FrameMesh("HighliterGameObject", tileMaterial, transform, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Generic), 30);
            
            PlanetTileMap.TileMapGeometry.BuildGeometry(planet.TileMap);
            
            data = new Result(fileName, tileMap, tiles, size, planet, highlightMesh);
        }
        catch (Exception e)
        {
            Failed(new ErrorData(e));
            return;
        }

        Success(data);
        
        
        void Success(Result result)
        {
            if (onSuccess == null) return;
            onSuccess.Invoke(result);
        }

        void Failed(IError error)
        {
#if UNITY_EDITOR
            UnityEditor.Selection.activeGameObject = transform.gameObject;
#endif
            
            if (onFailed == null) return;
            onFailed.Invoke(error);
        }
    }
    
    [System.Serializable]
    public class Result
    {
        private string _fileName;
        private Tiled.TiledMap _map;
        private TileProperty[][] _tileProperties;
        private Vec2i _size;
        private Planet.PlanetState _planetState;
        private Utility.FrameMesh _highlightMesh;

        public Result(
            string fileName,
            Tiled.TiledMap map,
            TileProperty[][] tileProperties,
            Vec2i size,
            Planet.PlanetState planetState,
            Utility.FrameMesh highlightMesh)
        {
            _fileName = fileName;
            _map = map;
            _tileProperties = tileProperties;
            _size = size;
            _planetState = planetState;
            _highlightMesh = highlightMesh;
        }


        public string GetFileName() => _fileName;

        public TiledMap GetTileMap() => _map;

        public TileProperty[][] GetTileProperties() => _tileProperties;

        public Vec2i GetMapSize() => _size;

        public PlanetState GetPlanetState() => _planetState;
    
        public Utility.FrameMesh GetHighlightMesh() => _highlightMesh;
    }
}
