//impors

//NEED SPRITE RENDERER COMPONENT TO WORK
[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(UnityEngine.SpriteRenderer))]
public class PixelSnap : UnityEngine.MonoBehaviour
{
    // Sprite target
    private UnityEngine.Sprite sprite;

    // Acutal poisiton world space
    private UnityEngine.Vector3 actualPosition;

    // Option to restore position
    private bool shouldRestorePosition;

    void Awake()
    {
        if (SceneManager.Instance != null)
        {
            SceneManager.Instance.Register(this, Enums.SceneObjectType.SceneObjectTypeUtilityScript);
        }
    }

    // Use this for initialization
    void Start()
    {
        // Assigning renderer
        UnityEngine.SpriteRenderer renderer = GetComponent<UnityEngine.SpriteRenderer>();
        if (renderer != null)
        {
            sprite = renderer.sprite;
        }
        else
        {
            sprite = null;
        }
    }

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnWillRenderObject.html
    void OnWillRenderObject()
    {
        UnityEngine.Camera cam = UnityEngine.Camera.current;
        if (!cam)
            return;

        PixelPerfectCameraTestTool pixelPerfectCamera = cam.GetComponent<PixelPerfectCameraTestTool>();

        shouldRestorePosition = true;
        actualPosition = transform.position;

        float cameraPPU = (float)cam.pixelHeight / (2f * cam.orthographicSize);
        float cameraUPP = 1.0f / cameraPPU;

        UnityEngine.Vector2 camPos = new UnityEngine.Vector2(cam.transform.position.x, cam.transform.position.y);
        UnityEngine.Vector2 pos = new UnityEngine.Vector2(actualPosition.x, actualPosition.y);
        UnityEngine.Vector2 relPos = pos - camPos;

        UnityEngine.Vector2 offset = new UnityEngine.Vector2(0, 0);
        // offset for screen pixel edge if screen size is odd
        offset.x = (cam.pixelWidth % 2 == 0) ? 0 : 0.5f;
        offset.y = (cam.pixelHeight % 2 == 0) ? 0 : 0.5f;
        // offset for pivot in Sprites
        UnityEngine.Vector2 pivotOffset = new UnityEngine.Vector2(0, 0);
        if (sprite != null)
        {
            pivotOffset = sprite.pivot - new UnityEngine.Vector2(UnityEngine.Mathf.Floor(sprite.pivot.x), UnityEngine.Mathf.Floor(sprite.pivot.y)); // the fractional part in texture pixels           
            
            float camPixelsPerAssetPixel = cameraPPU / sprite.pixelsPerUnit;
            pivotOffset *= camPixelsPerAssetPixel; // convert to screen pixels
            
        }
        
        // Convert the units to pixels, round them, convert back to units. The offsets make sure that the distance we round is from screen pixel (fragment) edges totexel  edges.
        relPos.x = (UnityEngine.Mathf.Round(relPos.x / cameraUPP - offset.x) + offset.x + pivotOffset.x) * cameraUPP;
        relPos.y = (UnityEngine.Mathf.Round(relPos.y / cameraUPP - offset.y) + offset.y + pivotOffset.y) * cameraUPP;
        
        pos = relPos + camPos;

        transform.position = new UnityEngine.Vector3(pos.x, pos.y, actualPosition.z);
    }

    // This scripts is based on the assumption that every camera that calls OnWillRenderObject(), will call OnRenderObject() before any other
    // camera calls any of these methods.
    void OnRenderObject()
    {
        //Debug.Log("on did" + Camera.current);
        if (shouldRestorePosition)
        {
            shouldRestorePosition = false;
            transform.position = actualPosition;
        }
    }

}
