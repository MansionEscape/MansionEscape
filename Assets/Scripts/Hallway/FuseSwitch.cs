using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseSwitch : MonoBehaviour
{
    public GameObject fuseSwitch;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            fuseSwitch.transform.eulerAngles = new Vector3(
                fuseSwitch.transform.eulerAngles.x,
                fuseSwitch.transform.eulerAngles.y,
                fuseSwitch.transform.eulerAngles.z + -90
                );
        }
    }
}
