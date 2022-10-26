//import UnityEngine


using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : UnityEngine.MonoBehaviour
{
    // Key struct to keep key settings
    public struct Key
    {
        public UnityEngine.KeyCode keyCode;
        public Enums.eKeyEvent keyEvent;
        public string keyName;
    }

    // Player State
    private Enums.PlayerState playerState = Enums.PlayerState.Pedestrian;

    // Currently Active Key
    public Key activeKey;

    public float scale = 1.0f;

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        //Check if Scene has SceneManager setup
        if (SceneManager.Instance != null)
        {
            SceneManager.Instance.Register(this, Enums.SceneObjectType.SceneObjectTypeUtilityScript);
        }
    }

    public void Controls()
    {
        if (playerState == Enums.PlayerState.Pedestrian)
        {
            // Decrease zoom with -
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadMinus))
            {
                PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
                if (pixelCam.targetCameraHalfWidth < 15.0f)
                    pixelCam.targetCameraHalfWidth += 1.0f;
                // Update Zoomed ortho pixel perfect calculation
                pixelCam.adjustCameraFOV();
            }

            // Increase Zoom with +
            if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadPlus))
            {
                PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
                if (pixelCam.targetCameraHalfWidth > 1.5f)
                    pixelCam.targetCameraHalfWidth -= 1.0f;
                // Update Zoomed ortho pixel perfect calculation
                pixelCam.adjustCameraFOV();
            }

            if (UnityEngine.Input.mouseScrollDelta.y > 0)
            {
                PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
                if (pixelCam.targetCameraHalfWidth > 1.5f)
                    pixelCam.targetCameraHalfWidth -= 1.0f;
                // Update Zoomed ortho pixel perfect calculation
                pixelCam.adjustCameraFOV();
            }
            else if (UnityEngine.Input.mouseScrollDelta.y < -0.5f)
            {
                PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
                if (pixelCam.targetCameraHalfWidth < 15.0f)
                    pixelCam.targetCameraHalfWidth += 1.0f;
                // Update Zoomed ortho pixel perfect calculation
                pixelCam.adjustCameraFOV();
            }
        }

        if (UnityEngine.Input.mouseScrollDelta.y > 0)
        {
            PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
            if (pixelCam.targetCameraHalfWidth > 1.5f)
                pixelCam.targetCameraHalfWidth -= 1.0f;
            // Update Zoomed ortho pixel perfect calculation
            pixelCam.adjustCameraFOV();
        }
        else if (UnityEngine.Input.mouseScrollDelta.y < -0.5f)
        {
            PixelPerfectCameraTestTool pixelCam = UnityEngine.Camera.main.GetComponent<PixelPerfectCameraTestTool>();
            if (pixelCam.targetCameraHalfWidth < 15.0f)
                pixelCam.targetCameraHalfWidth += 1.0f;
            // Update Zoomed ortho pixel perfect calculation
            pixelCam.adjustCameraFOV();
        }
    }

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    private void Update()
    {
        // Detect Key Call
        //DetectKey();

        scale += UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 0.5f * scale;
        Camera.main.orthographicSize = 20.0f / scale;

        Controls();
    }

    // Returns if referenced key pressed or not
    public bool IsKeyPressed(UnityEngine.KeyCode key)
    {
        if(activeKey.ToString() == key.ToString())
        {
            return true;
        }
        return false;
    }

    // Doc: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnGUI.html
    private void OnGUI()
    {
        // Detect Input device to understand which device player using.
        DetectInputDevice();
    }

    // Detecting Input device from input actions
    private Enums.eInputDevice DetectInputDevice()
    {
        // If any mouse or keyboard key detected, set input device to keyboard+mouse
        if(UnityEngine.Event.current.isKey ||
            UnityEngine.Event.current.isMouse)
        {
            return Enums.eInputDevice.KeyboardMouse;
        }

        // If any mouse hover event detected, set input device to keyboard+mouse
        if (UnityEngine.Input.GetAxis("Mouse X") != 0.0f ||
            UnityEngine.Input.GetAxis("Mouse Y") != 0.0f)
        {
            return Enums.eInputDevice.KeyboardMouse;
        }

        // Else, return none device.
        return Enums.eInputDevice.Invalid;
    }
}