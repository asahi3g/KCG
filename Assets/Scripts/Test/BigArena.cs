using KMath;

class BigArena : UnityEngine.MonoBehaviour
{

    public void Start()
    {
        Initialize();
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        UnityEngine.Application.targetFrameRate = 60;

        GameResources.Initialize();
        GameState.ScreenManager.Init(transform);
    }

    public void Update()
    {
        GameState.ScreenManager.Update();
        GameState.ScreenManager.Draw();
    }


    private void OnGUI()
    {
        GameState.ScreenManager.OnGui(); 
    }

    private void OnDrawGizmos()
    {
      
    }

}

