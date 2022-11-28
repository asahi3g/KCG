using KMath;
using PlanetTileMap;

public interface IPlanetCreationResult
{
    public PlanetRenderer GetPlanetRenderer();
    public string GetFileName();
    public Tiled.TiledMap GetTileMap();
    
    public TileProperty[][] GetTileProperties();

    public Vec2i GetMapSize();

    public Planet.PlanetState GetPlanet();
}
