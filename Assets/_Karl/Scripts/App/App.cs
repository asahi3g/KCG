using Engine3D;
using UnityEngine;

public class App : Singleton<App>
{
    [SerializeField] private Player _player;
    [SerializeField] private AppUI _uI;
    
    public Player GetPlayer() => _player;
    public AppUI GetUI() => _uI;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    protected override void Awake()
    {
        base.Awake();
        
        GameResources.Initialize();
        AssetManager assetManager = AssetManager.Singelton; // force initialization
        GameState.AudioSystem = new Audio.AudioSystem();
        GameState.AudioSystem.SetAudioSource(GetComponent<AudioSource>());
        
        GameState.TileSpriteAtlasManager.UpdateAtlasTextures();
        GameState.SpriteAtlasManager.UpdateAtlasTextures();
        GameState.IsInitialized = true;
    }
    
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameState.IsInitialized = false;
    }
}
