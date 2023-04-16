using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    public GameObject vrHeadset;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(1, vrHeadset.transform.position.y , 1);
        this.transform.position = new Vector3(vrHeadset.transform.position.x, this.transform.position.y, vrHeadset.transform.position.z);
    }
}
