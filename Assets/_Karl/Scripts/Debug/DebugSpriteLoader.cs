using Sprites;
using UnityEngine;

public class DebugSpriteLoader : DebugBase
{
    [SerializeField] private SpriteSheet[] _spriteSheets;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        _spriteSheets = GameState.SpriteLoader.SpriteSheets;
    }
}
