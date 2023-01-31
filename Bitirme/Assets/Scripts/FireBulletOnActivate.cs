using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed;
    public int ammoCount;
    public int currentAmmo;
    public TextMeshProUGUI ammoCountText;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHitEffect;

    public InputActionProperty inputReference;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammoCount;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        ammoCountText.text = ammoCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputReference.action.WasPressedThisFrame()) Reload();
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
            currentAmmo--;
            ammoCountText.text = currentAmmo.ToString();
        }
        else
        {
            //Disable Haptic
        }
    }
    
    public void Reload()
    {
        currentAmmo = ammoCount;
        ammoCountText.text = currentAmmo.ToString();
    }
}
