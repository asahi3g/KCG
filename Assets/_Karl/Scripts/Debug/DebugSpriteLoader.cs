using Sprites;
using UnityEngine;

public class DebugSpriteLoader : MonoBehaviour
{
    [SerializeField] private SpriteSheet[] _spriteSheets;

    void Update()
    {
        _spriteSheets = GameState.SpriteLoader.SpriteSheets;
    }
}
