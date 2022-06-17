using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scale = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width * 0.75)
        {
            transform.position += Vector3.right * Input.GetAxis("Mouse X") * -0.28f / scale;
            transform.position += Vector3.up    * Input.GetAxis("Mouse Y") * -0.28f / scale;
        }

        scale += Input.GetAxis("Mouse ScrollWheel") * 0.5f * scale;
        GetComponent<Camera>().orthographicSize = 20.0f / scale;
    }

    public void setPosition(float x, float y, float s)
    {
        scale = s;
        transform.position = new Vector3(x, y, -10);
        GetComponent<Camera>().orthographicSize = 20.0f / scale;
    }
}