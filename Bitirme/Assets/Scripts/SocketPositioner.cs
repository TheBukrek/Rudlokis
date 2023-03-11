using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketPositioner : MonoBehaviour
{
    public GameObject origin;
    public Vector3 offSet;

    // Update is called once per frame
    void Update()
    { 
        var pos = origin.transform.position; 
        var forward = origin.transform.forward; 
        transform.position = pos + (forward * offSet.z);
        transform.position = new Vector3(transform.position.x, pos.y + offSet.y, transform.position.z);
    }
    
    private void OnDrawGizmos()
    {
            var pos = origin.transform.position;
            var forward = origin.transform.forward;
            forward.y = 0;
            Gizmos.color = Color.blue;
            // Gizmos.DrawLine(pos, pos + Vector3.ProjectOnPlane(gameObject.transform.forward, Vector3.up));
            Gizmos.DrawLine(pos, pos + forward.normalized);
    }
}
