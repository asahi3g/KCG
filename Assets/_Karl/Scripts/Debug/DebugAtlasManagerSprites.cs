using Sprites;

public class DebugAtlasManagerSprites : DebugAtlasManager
{
    protected override SpriteAtlas[] GetAtlases()
    {
        return GameState.SpriteAtlasManager.AtlasArray;
    }
}
