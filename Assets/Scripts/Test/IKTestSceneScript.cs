//imports UnityEngine

public class IKTestSceneScript : UnityEngine.MonoBehaviour
{
    UnityEngine.GameObject aim;
    // Start is called before the first frame update
    void Start()
    {
        aim = UnityEngine.GameObject.Find("AimTargetTest");
    }

    // Update is called once per frame
    void Update()
    {
       //aim.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
