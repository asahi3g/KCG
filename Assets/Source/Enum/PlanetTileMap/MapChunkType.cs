namespace Enums.PlanetTileMap
{
    public enum MapChunkType
    {
        // Not initialized chunk
        Error = 0,
        // Chunk with only Air tiles
        Empty,
        // At least one non air tile exist
        NotEmpty
    }
}
