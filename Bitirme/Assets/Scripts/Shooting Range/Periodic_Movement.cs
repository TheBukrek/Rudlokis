using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Periodic_Movement : MonoBehaviour
{
    public float speed = 0.0f;
    float movement;
    float direction = 1.0f;
    public GameObject obj;
    XRSimpleInteractable grabbable;
    bool updateFlag = false;
    void Start()
    {
        grabbable = GetComponentInChildren<XRSimpleInteractable>();
    }
    void Update()
    {
        if(updateFlag){
            if (obj.transform.position.z > 3.0f)
            {
                direction = -1.0f;
            }
            else if (obj.transform.position.z < -3.0f)
            {
                direction = 1.0f;
            }
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, speed*direction*Time.deltaTime + obj.transform.position.z);
        }
    }
    // Update is called once per frame
    public void MoveShootingTargets(GameObject obj)
    {
        updateFlag = !updateFlag;
    }
}
