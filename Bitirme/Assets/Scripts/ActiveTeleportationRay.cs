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


    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumer, out bool leftValid);
        lefttp.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftactive.action.ReadValue<float>() > 0.1f);
        
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumer, out bool rightValid);
        righttp.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightactive.action.ReadValue<float>() > 0.1f);

        lefttp.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftactive.action.ReadValue<float>() > 0.1f);
        righttp.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightactive.action.ReadValue<float>() > 0.1f);
    }
}
