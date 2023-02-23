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
    public float damage;
    public TextMeshProUGUI ammoCountText;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHitEffect;
    public AudioSource shootingSound;
    public InputActionReference leftReload;
    public InputActionReference rightReload;
    private Bucket<bool> bucket;

    HandData holdingHand;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammoCount;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        shootingSound = GetComponent<AudioSource>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.selectEntered.AddListener(SetHand);
        grabbable.selectExited.AddListener(UnsetHand);
        ammoCountText.text = ammoCount.ToString();
        bucket = new Bucket<bool>(new List<bool>{true,true,false});
    }

    public void FireBullet(ActivateEventArgs args)
    {
        if (currentAmmo > 0)
        {
            // foreach (var x in bucket.Add(false))
            // {
            //     Debug.Log(x);
            // }
            Debug.Log(bucket.Pick());
            // GameObject spawnedBullet = Instantiate(bullet);
            // spawnedBullet.transform.position = spawnPoint.position;
            // spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            // Destroy(spawnedBullet, 5f);
            RaycastHit hit;
            if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit))
            {
                bulletHitEffect.transform.position = hit.point;
                bulletHitEffect.transform.forward = hit.normal;
                shootingSound.Play();
                if (hit.transform.CompareTag("Enemy"))
                {
                    
                    EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                    //enemy.health-=20f;
                    enemy.Damage(damage);
                    
                }
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
    
    public void Reload(InputAction.CallbackContext context)
    {
        currentAmmo = ammoCount;
        ammoCountText.text = currentAmmo.ToString();
    }

    public void SetHand(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();

            if (handData.handType == HandData.HandModelType.Right)
            {
                rightReload.action.started += Reload;
            }
            else
            {
                leftReload.action.started += Reload;
            }
        }
    }
    
    public void UnsetHand(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();

            if (handData.handType == HandData.HandModelType.Right)
            {
                rightReload.action.started -= Reload;
            }
            else
            {
                leftReload.action.started -= Reload;
            }
        }
    }
    
}
