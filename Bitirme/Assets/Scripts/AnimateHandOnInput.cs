using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    // Start is called before the first frame update

    public InputActionProperty pinchActionProperty;
    public InputActionProperty gripActionProperty;
    public Animator HandAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue= pinchActionProperty.action.ReadValue<float>();
        float gripValue= gripActionProperty.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger", triggerValue);
        HandAnimator.SetFloat("Grip", gripValue);
    }
}
