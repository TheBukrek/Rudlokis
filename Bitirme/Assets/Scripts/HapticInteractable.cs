using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]

public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;


    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {

        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            
            TriggerHaptic(controllerInteractor.xrController);
        }
    }
    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {

            controller.SendHapticImpulse(intensity, duration);
        }
    }

}
public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActive;
   
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActive.TriggerHaptic);
    }

    
}