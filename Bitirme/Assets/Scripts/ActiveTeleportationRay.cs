using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActiveTeleportationRay : MonoBehaviour
{
    public GameObject lefttp;
    public GameObject righttp;
        
    public InputActionProperty leftactive;
    public InputActionProperty rightactive;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lefttp.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftactive.action.ReadValue<float>() > 0.1f);
        righttp.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightactive.action.ReadValue<float>() > 0.1f);
    }
}
