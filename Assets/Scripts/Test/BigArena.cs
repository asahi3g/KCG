using KMath;

class BigArena : UnityEngine.MonoBehaviour
{

    GameScreen.BigArenaScreen Screen = new GameScreen.BigArenaScreen();
    public void Start()
    {
        Initialize();
    }

    // create the sprite atlas for testing purposes
    public void Initialize()
    {
        UnityEngine.Application.targetFrameRate = 60;

        GameResources.Initialize();
        Screen.Init(transform);
    }

    public void Update()
    {
        Screen.Update();
        Screen.Draw();
    }


    private void OnGUI()
    {
        Screen.OnGui(); 
    }

    private void OnDrawGizmos()
    {
       Screen.OnDrawGizmos();
    }

}

