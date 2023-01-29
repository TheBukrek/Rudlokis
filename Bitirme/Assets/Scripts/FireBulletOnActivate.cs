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
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHitEffect;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.
        
        ammoCountText.text = ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs args)
    {
        if (ammoCount > 0)
        {
            // GameObject spawnedBullet = Instantiate(bullet);
            // spawnedBullet.transform.position = spawnPoint.position;
            // spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            // Destroy(spawnedBullet, 5f);
            RaycastHit hit;
            if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                bulletHitEffect.transform.position = hit.point;
                bulletHitEffect.transform.forward = hit.normal;
                bulletHitEffect.Play();
            }
            muzzleFlash.Play();
            ammoCount--;
            ammoCountText.text = ammoCount.ToString();
        }
        else
        {
            //Disable Haptic
        }
    }
    
    public void Reload()
    {
        
    }
}
