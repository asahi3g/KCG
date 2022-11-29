using System;
using Sprites;
using UnityEngine;

public class DebugAtlasManagerTiles : DebugAtlasManager
{

    protected override SpriteAtlas[] GetAtlases()
    {
        return GameState.TileSpriteAtlasManager.GetSpriteAtlases();
    }
}
