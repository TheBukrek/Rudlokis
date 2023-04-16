using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MultiInteractable : XRBaseInteractable
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        XRBaseInteractor interactor = selectingInteractor;
        IXRSelectInteractor xRSelectInteractor = firstInteractorSelecting;

        List<IXRSelectInteractor> xRSelectInteractors = interactorsSelecting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
