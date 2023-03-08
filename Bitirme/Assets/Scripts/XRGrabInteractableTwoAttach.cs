using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondInt = new List<XRSimpleInteractable>();

    //public GrabHandPoseForSimpleIntreractable GrabHandPoseForSimple;
    private Quaternion attachinitial;
    private XRBaseInteractor secondInteractor;

    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    public Transform leftAttachTransformSecond;
    public Transform rightAttachTransformSecond;

    public enum TwoHandRotationType {None, First, Second };
    public TwoHandRotationType rotationType;

    
    void Start()
    {
        foreach(var item in secondInt)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);

            
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }

    

    /*protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }
        base.OnSelectEntered(args);
    }*/

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {   
            attachTransform = leftAttachTransform;
            
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }
        base.OnSelectEntering(args);
    }

    
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {   
        if(secondInteractor && selectingInteractor)
        {
            selectingInteractor.transform.rotation = GetTwoHandRotation();

        }
        base.ProcessInteractable(updatePhase);
    }

    
    public Quaternion GetTwoHandRotation()
    {
        Quaternion targetRot = new Quaternion();
        if (rotationType==TwoHandRotationType.None)
        {
            targetRot = Quaternion.LookRotation(secondInteractor.transform.position - selectingInteractor.attachTransform.position);
        }
        else if (rotationType == TwoHandRotationType.First)
        {
            targetRot = Quaternion.LookRotation(secondInteractor.transform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else if (rotationType == TwoHandRotationType.Second)
        {
            targetRot = Quaternion.LookRotation(secondInteractor.transform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
        }
        return targetRot;
    }
    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {

        
        secondInteractor = interactor;
        
        if (secondInteractor.transform.CompareTag("Left Hand"))
        {
            //secondInteractor.attachTransform = leftAttachTransformSecond;

        }
        else if (secondInteractor.transform.CompareTag("Right Hand"))
        {
            //secondInteractor.attachTransform = rightAttachTransformSecond;
        }
        
    }

    

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("second rel");
        secondInteractor = null;
    }

    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("first grab");
        
        base.OnSelectEntered(interactor);
        attachinitial = interactor.transform.localRotation;

    }
    

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("firts rel");
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.transform.localRotation = attachinitial;
    }

    [System.Obsolete]
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isalreadygrabed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabed;
    }
}
