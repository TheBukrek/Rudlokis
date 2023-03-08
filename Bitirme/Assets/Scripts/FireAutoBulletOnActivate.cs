using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireAutoBulletOnActivate : MonoBehaviour
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
    public bool isFiring=false;

    [Range (1, 25)]
    public float fireRate;
    private float _timeToFire;
    private float _timeToFireCurrent;



    //animation related
    //[SerializeField] private Animator myAnimator = null;
    
    HandData holdingHand;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammoCount;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        shootingSound = GetComponent<AudioSource>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.deactivated.AddListener(DoNotFire);
        
        grabbable.selectEntered.AddListener(SetHand);
        grabbable.selectExited.AddListener(UnsetHand);
        ammoCountText.text = ammoCount.ToString();

        // set Fire rate
        _timeToFire = 1 / fireRate;
        _timeToFireCurrent = _timeToFire;
    }
    void Update()
    {
        
        if (isFiring)
        {
            
            _timeToFireCurrent += Time.deltaTime;
            if (_timeToFireCurrent >= _timeToFire) {
                _timeToFireCurrent = ( _timeToFireCurrent - _timeToFire);
            
                if (currentAmmo > 0)
                {
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
                    //myAnimator.Play("RecoilAnim", 0);
                    currentAmmo--;
                    ammoCountText.text = currentAmmo.ToString();
                }
                else
                {
                    //Disable Haptic
                }
            }
            
        }
    }

    public void DoNotFire(DeactivateEventArgs arg0)
    {
            isFiring = false;  
    }

    public void FireBullet(ActivateEventArgs args)
    {
        isFiring = true;
        
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
