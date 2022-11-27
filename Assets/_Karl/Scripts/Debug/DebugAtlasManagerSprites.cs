using Sprites;
using UnityEngine;

public class DebugAtlasManagerSprites : DebugAtlasManager
{
    
    
    protected override SpriteAtlas[] GetAtlases()
    {
        return GameState.SpriteAtlasManager.AtlasArray;
    }

}
