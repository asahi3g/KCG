//imports UnityEngine

using Planet.Background;

public class PlanetVisualEffectsTest : UnityEngine.MonoBehaviour
{
    // ATLAS
    [UnityEngine.SerializeField]
    private UnityEngine.Material Material;

    PlanetBackgroundVisualEffects planetVisualEffects;

    private bool Init;

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    void Start()
    {
        planetVisualEffects = new PlanetBackgroundVisualEffects();

        planetVisualEffects.Initialize(Material, transform, 1);

        Init = true;
    }

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    void Update()
    {
        if(Init)
        {
            // check if the sprite atlas textures needs to be updated
            GameState.SpriteAtlasManager.UpdateAtlasTextures();

            // check if the tile sprite atlas textures needs to be updated
            GameState.TileSpriteAtlasManager.UpdateAtlasTextures();

            // Draw The Visual Effects
            planetVisualEffects.Draw(Material, transform, 1);
        }
    }
}
