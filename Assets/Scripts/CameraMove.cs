


public class CameraMove : UnityEngine.MonoBehaviour
{
    public float CameraSpeed = 6.0f;

    void Awake()
    {
        //Check if Scene has SceneManager setup
        if(SceneManager.Instance != null)
        {
            SceneManager.Instance.Register(this, Enums.SceneObjectType.SceneObjectTypeUtilityScript);
        }
    }

    void Update()
    {
        var x = 0f;
        var y = 0f;

        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.A)) x = -1;
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.D)) x = 1;
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.W)) y = 1;
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.S)) y = -1;

        transform.position += UnityEngine.Vector3.right * x * UnityEngine.Time.deltaTime * CameraSpeed;
        transform.position += UnityEngine.Vector3.up * y * UnityEngine.Time.deltaTime * CameraSpeed;
    }
}
