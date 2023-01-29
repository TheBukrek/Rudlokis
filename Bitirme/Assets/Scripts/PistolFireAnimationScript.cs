using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolFireAnimationScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponentInParent<XRGrabInteractable>();
        grabbable.activated.AddListener(PlayFireAnimation);
    }

    public void PlayFireAnimation(ActivateEventArgs args)
    {
        animator.Play("Pistol Fire");
    } 
}
