using Sprites;

public class DebugAtlasManagerTiles : DebugAtlasManager
{
    protected override SpriteAtlas[] GetAtlases()
    {
        return GameState.TileSpriteAtlasManager.GetSpriteAtlases();
    }
}
