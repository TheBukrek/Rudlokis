using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed;
    public int ammoCount;
    public TextMeshProUGUI ammoCountText;
    private HapticInteractable hapticInteractable;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        
        ammoCountText.text = ammoCount.ToString();
        hapticInteractable = GetComponent<HapticInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs args)
    {
        if (ammoCount > 0)
        {
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            ammoCount--;
            ammoCountText.text = ammoCount.ToString();
            Destroy(spawnedBullet, 5f);
        }
        else
        {
            if(hapticInteractable.enabled)
                GetComponent<HapticInteractable>().enabled = false;
        }
    }
}
