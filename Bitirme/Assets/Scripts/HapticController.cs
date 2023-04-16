using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
    

public class HapticController : MonoBehaviour
{
    public  XRBaseController leftController, rightController;
    public  float defaultAmplitude = 0.2f;
    public  float defaultDuration = 0.1f;


    [ContextMenu("Send Haptics")]
    public  void SendHaptics() {
        leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }
    public  void SendHaptics(float amplitude, float duration)
    {
        leftController.SendHapticImpulse(amplitude, duration);
        rightController.SendHapticImpulse(amplitude, duration);
    }
    public  void SendHaptics(bool isLeftHand, float amplitude, float duration)
    {
        if (isLeftHand)
        {
            leftController.SendHapticImpulse(amplitude, duration);
        }
        else {
            rightController.SendHapticImpulse(amplitude, duration);
        }
    }

    public  void SendHaptics(XRBaseController controller, float amplitude, float duration)
    {

        controller.SendHapticImpulse(amplitude, duration);
    }


}

