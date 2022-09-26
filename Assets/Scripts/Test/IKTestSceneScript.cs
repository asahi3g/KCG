using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTestSceneScript : MonoBehaviour
{
    GameObject aim;
    // Start is called before the first frame update
    void Start()
    {
        aim = GameObject.Find("AimTargetTest");
    }

    // Update is called once per frame
    void Update()
    {
       //aim.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
