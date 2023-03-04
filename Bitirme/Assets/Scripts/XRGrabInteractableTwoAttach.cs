using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondInt = new List<XRSimpleInteractable>();

    private Quaternion attachinitial;
    private XRBaseInteractor secondInteractor;

    public Transform leftAttachTransform;
    public Transform rightAttachTransform;


    void Start()
    {
        foreach(var item in secondInt)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {   
        if(secondInteractor && selectingInteractor)
        {
            selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);

        }
        base.ProcessInteractable(updatePhase);
    }
    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        Debug.Log("second grab");
        secondInteractor = interactor;
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
        attachinitial = interactor.attachTransform.localRotation;

    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
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
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("firts rel");
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachinitial;
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isalreadygrabed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabed;
    }
}
