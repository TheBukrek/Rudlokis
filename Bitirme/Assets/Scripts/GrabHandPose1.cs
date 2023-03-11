using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//
public class GrabHandPose1 : MonoBehaviour
{
    public float poseTransitionDuration;

    public HandData rightFirstHandPose;
    public HandData rightSecondHandPose;
    public HandData leftFirstHandPose;
    public HandData leftSecondHandPose;

    private Vector3 firstStartingHandPosition;
    private Vector3 secondStartingHandPosition;
    private Vector3 firstFinalHandPosition;
    private Vector3 secondFinalHandPosition;

    private Quaternion firstStartingHandRotation;
    private Quaternion secondStartingHandRotation;
    private Quaternion firstFinalHandRotation;
    private Quaternion secondFinalHandRotation;

    private Quaternion[] firstStartingFingerRotations;
    private Quaternion[] secondStartingFingerRotations;
    private Quaternion[] firstFinalFingerRotations;
    private Quaternion[] secondFinalFingerRotations;

    private HandData firstHandData;
    private HandData secondHandData;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPose);

        rightFirstHandPose.gameObject.SetActive(false);
        rightSecondHandPose.gameObject.SetActive(false);
        leftFirstHandPose.gameObject.SetActive(false);
        leftSecondHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        //Set second hand data if first hand data exists
        if (firstHandData != null)
        {
            if (arg.interactorObject is XRDirectInteractor)
            {
                HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;
                secondHandData = handData;

                if (handData.handType == HandData.HandModelType.Right)
                {
                    SetSecondHandDataValues(handData, rightSecondHandPose);
                }
                else
                {
                    SetSecondHandDataValues(handData, leftSecondHandPose);
                }

                StartCoroutine(SetHandDataRoutine(handData, secondFinalHandPosition, secondFinalHandRotation,
                    secondFinalFingerRotations,
                    secondStartingHandPosition, secondStartingHandRotation, secondStartingFingerRotations));
            }
            else if (arg.interactorObject is XRRayInteractor)
            {
                HandData handData = arg.interactorObject.transform.parent.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;
                secondHandData = handData;

                if (handData.handType == HandData.HandModelType.Right)
                {
                    SetSecondHandDataValues(handData, rightSecondHandPose);
                }
                else
                {
                    SetSecondHandDataValues(handData, leftSecondHandPose);
                }

                StartCoroutine(SetHandDataRoutine(handData, secondFinalHandPosition, secondFinalHandRotation,
                    secondFinalFingerRotations,
                    secondStartingHandPosition, secondStartingHandRotation, secondStartingFingerRotations));
            }
        }
        else
        {
            if (arg.interactorObject is XRDirectInteractor)
            {
                HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;
                firstHandData = handData;

                if (handData.handType == HandData.HandModelType.Right)
                {
                    SetFirstHandDataValues(handData, rightFirstHandPose);
                }
                else
                {
                    SetFirstHandDataValues(handData, leftFirstHandPose);
                }

                StartCoroutine(SetHandDataRoutine(handData, firstFinalHandPosition, firstFinalHandRotation,
                    firstFinalFingerRotations,
                    firstStartingHandPosition, firstStartingHandRotation, firstStartingFingerRotations));
            }
            else if (arg.interactorObject is XRRayInteractor)
            {
                HandData handData = arg.interactorObject.transform.parent.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;
                firstHandData = handData;

                if (handData.handType == HandData.HandModelType.Right)
                {
                    SetFirstHandDataValues(handData, rightFirstHandPose);
                }
                else
                {
                    SetFirstHandDataValues(handData, leftFirstHandPose);
                }

                StartCoroutine(SetHandDataRoutine(handData, firstFinalHandPosition, firstFinalHandRotation,
                    firstFinalFingerRotations,
                    firstStartingHandPosition, firstStartingHandRotation, firstStartingFingerRotations));
            }
        }
    }

    public void UnsetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            if (secondHandData == handData)
            {
                StartCoroutine(SetHandDataRoutine(secondHandData, secondStartingHandPosition,
                    secondStartingHandRotation, secondStartingFingerRotations,
                    secondFinalHandPosition, secondFinalHandRotation, secondFinalFingerRotations));
                secondHandData = null;
            }
            else
            {
                if (secondHandData != null)
                {
                    StartCoroutine(SetHandDataRoutine(handData, firstFinalHandPosition, firstFinalHandRotation,
                        firstFinalFingerRotations,
                        secondFinalHandPosition, secondFinalHandRotation, secondFinalFingerRotations));

                    firstHandData = secondHandData;
                    secondHandData = null;
                }
                else
                {
                    StartCoroutine(SetHandDataRoutine(handData, firstStartingHandPosition, firstStartingHandRotation,
                        firstStartingFingerRotations,
                        firstFinalHandPosition, firstFinalHandRotation, firstFinalFingerRotations));
                }
            }
        }
        else if (arg.interactorObject is XRRayInteractor)
        {
            HandData handData = arg.interactorObject.transform.parent.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            // StartCoroutine(SetHandDataRoutine(handData, startingHandPosition, startingHandRotation, startingFingerRotations,
            //     finalHandPosition, finalHandRotation, finalFingerRotations));
        }
    }

    public void SetFirstHandDataValues(HandData h1, HandData h2)
    {
        firstStartingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y,
            h1.root.localPosition.z / h1.root.localScale.z);
        firstFinalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y,
            h2.root.localPosition.z / h2.root.localScale.z);

        // startingHandPosition = h1.root.localPosition;
        // finalHandPosition = h2.root.localPosition;

        firstStartingHandRotation = h1.root.localRotation;
        firstFinalHandRotation = h2.root.localRotation;

        var length = h1.fingerBones.Length;
        firstStartingFingerRotations = new Quaternion[length];
        firstFinalFingerRotations = new Quaternion[length];

        for (int i = 0; i < length; i++)
        {
            firstStartingFingerRotations[i] = h1.fingerBones[i].localRotation;
            firstFinalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetSecondHandDataValues(HandData h1, HandData h2)
    {
        secondStartingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y,
            h1.root.localPosition.z / h1.root.localScale.z);
        secondFinalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y,
            h2.root.localPosition.z / h2.root.localScale.z);

        // startingHandPosition = h1.root.localPosition;
        // finalHandPosition = h2.root.localPosition;

        secondStartingHandRotation = h1.root.localRotation;
        secondFinalHandRotation = h2.root.localRotation;

        var length = h1.fingerBones.Length;
        secondStartingFingerRotations = new Quaternion[length];
        secondFinalFingerRotations = new Quaternion[length];

        for (int i = 0; i < length; i++)
        {
            secondStartingFingerRotations[i] = h1.fingerBones[i].localRotation;
            secondFinalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public IEnumerator SetHandDataRoutine(HandData h, Vector3 newPosition, Quaternion newRotation,
        Quaternion[] newBonesRotation, Vector3 startingPosition, Quaternion startingRotation,
        Quaternion[] startingBonesRotation)
    {
        float timer = 0;

        while (timer < poseTransitionDuration)
        {
            Vector3 p = Vector3.Lerp(startingPosition, newPosition, timer / poseTransitionDuration);
            Quaternion r = Quaternion.Lerp(startingRotation, newRotation, timer / poseTransitionDuration);

            h.root.localPosition = p;
            h.root.localRotation = r;

            for (int i = 0; i < newBonesRotation.Length; i++)
            {
                h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i],
                    timer / poseTransitionDuration);
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }
}