using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is device act" + XRSettings.isDeviceActive);
        Debug.Log("Device Name" + XRSettings.loadedDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
