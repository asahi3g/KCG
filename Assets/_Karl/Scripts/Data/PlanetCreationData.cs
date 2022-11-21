using KMath;
using Planet;
using PlanetTileMap;
using Tiled;

[System.Serializable]
public class PlanetCreationData : IPlanetCreationResult
{
    private string _fileName;
    private Tiled.TiledMap _map;
    private TileProperty[][] _properties;
    private Vec2i _size;
    private Planet.PlanetState _planet;

    public PlanetCreationData(string fileName, Tiled.TiledMap map, TileProperty[][] properties, Vec2i size, Planet.PlanetState planet)
    {
        _fileName = fileName;
        _map = map;
        _properties = properties;
        _size = size;
        _planet = planet;
    }


    public string GetFileName()
    {
        return _fileName;
    }

    public TiledMap GetTileMap()
    {
        return _map;
    }

    public TileProperty[][] GetTileProperties()
    {
        return _properties;
    }

    public Vec2i GetMapSize()
    {
        return _size;
    }

    public PlanetState GetPlanet()
    {
        return _planet;
    }
}
