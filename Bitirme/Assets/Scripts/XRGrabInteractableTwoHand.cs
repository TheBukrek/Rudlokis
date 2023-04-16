using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRGrabInteractableTwoHand : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    void Start()
    {
        
    }



    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        if(args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if(args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }
        base.OnSelectEntered(args);
    }
    
}
