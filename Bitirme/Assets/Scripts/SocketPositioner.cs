using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketPositioner : MonoBehaviour
{
    public GameObject origin;
    private Vector3 originPos;
    public Vector3 offSet;
    
    // Start is called before the first frame update
    void Start()
    {
        originPos = origin.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originPos;
    }

    private void OnDrawGizmos()
    {
        if (origin != null)
        {
            Gizmos.DrawLine(origin.transform.position, origin.transform.position + offSet);
        }
    }
}
