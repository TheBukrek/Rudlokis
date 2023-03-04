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
        transform.position = origin.transform.rotation * (origin.transform.position + offSet);
    }

    private void OnDrawGizmos()
    {
        if (origin != null)
        {
            Gizmos.DrawLine(origin.transform.position, origin.transform.position + offSet);
        }
    }
}
