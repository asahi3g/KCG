using KMath;

class GameTestScript : UnityEngine.MonoBehaviour
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
    }

    public void Update()
    {
    }


    private void OnGUI()
    {
    }

    private void OnDrawGizmos()
    {
      
    }

}

