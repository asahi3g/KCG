using UnityEngine;

public class App : Singleton<App>
{
    [SerializeField] private AppUI _uI;


    public AppUI GetUI() => _uI;

    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
