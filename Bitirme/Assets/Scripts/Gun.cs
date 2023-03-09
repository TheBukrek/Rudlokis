using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public enum FireType  {Single, Auto, Burst};
public enum BulletType {Raycast, Projectile };
public class Gun : MonoBehaviour {

    

    [Header("References")]
    public Transform bulletSpawnPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHitEffect;
    public AudioSource shootingSound;
    public HapticController hapticController;


    [Header("Gun Config")]
    public FireType fireType;
    public BulletType bulletType;

    public float damage;
    public float fireRate;
    
    private int remainingBurstBullet;

    //[HideInInspector]
    public int ammoCount =100;


    [Header("Burst Fire Config")]
    [Range(2, 5)]
    public int numberOfBulletsToBurst;
    [Range(8,32)]
    public float burstFireRate; 
    private float lastBurstBulletFired; // smaller one

    private bool isHoldingTrigger = false;


    private float lastFired;
    private float lastFiredPistol;
    private bool justPressedTrigger = false;




    



    public bool HasEnoughAmmo()
    {

        if (fireType == FireType.Burst)
        {
            if (ammoCount >= numberOfBulletsToBurst)
            {
                return true;
            }
            else
            {
                Debug.Log("There are not sufficient ammo to fire " + numberOfBulletsToBurst + " bullets at one time on this burst-type weapon");
                return false;
            }
        }
        else {
            if (ammoCount != 0)
            {
                return true;
            }
            else
            {
                Debug.Log("No bullets left in the magazine");
                return false;
            }
        }

    }

    public void SpendAmmunition() {
        Debug.LogWarning("SpendAmmunition() is not implemented");
    }


    public void Fire()
    {
        
        Debug.Log("Fire()");
        if (HasEnoughAmmo())
        {
            Debug.Log("Gun has enough ammo to fire!");
            switch (fireType)
            {
                case FireType.Single:
                    FireSingle();
                    break;
                case FireType.Auto:
                    FireAuto();
                    break;
                case FireType.Burst:
                    FireBurst();
                    break;
                default:
                    break;
            }
        }
        
     }



    public void FireEffects() {
        
        shootingSound.Play();
        bulletHitEffect.Play();
        muzzleFlash.Play();
    }


    public void CalculateBulletRaycastHit() {
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out hit))
        {
            
            bulletHitEffect.transform.position = hit.point;
            bulletHitEffect.transform.forward = hit.normal;

            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                enemy.Damage(damage);
            }
        }
    }

    public void SpawnBulletProjectile() {
        Debug.LogWarning("SpawnBulletProjectile() function is not implemented yet");
    }

    protected void FireSingle() {
        
        if (Time.time - lastFiredPistol > 1 / fireRate && justPressedTrigger)
        {
            if (bulletType == BulletType.Raycast)
            {
                FireEffects();
                CalculateBulletRaycastHit();

            }
            else {
                FireEffects();
                SpawnBulletProjectile();
            }

            SpendAmmunition();
            hapticController.SendHaptics(1f, 0.1f);
            
            
            //update ammo text

            justPressedTrigger = false;
            lastFiredPistol = Time.time;
        }

        
    }

    protected void FireSingleBullet()
    {

        if (Time.time - lastFired > 1 / fireRate)
        {
            if (bulletType == BulletType.Raycast)
            {
                FireEffects();
                CalculateBulletRaycastHit();

            }
            else
            {
                FireEffects();
                SpawnBulletProjectile();
            }

            SpendAmmunition();
            hapticController.SendHaptics();
            //update ammo text


            if (fireType != FireType.Burst)
            {
                lastFired = Time.time;
                Debug.Log(lastFired);

            }
        }


    }



    public void setIsHoldingTriggerFALSE(DeactivateEventArgs arg0)
    {
        isHoldingTrigger = false;  
    }

    public void setIsHoldingTriggerTRUE(ActivateEventArgs args)
    {
        isHoldingTrigger = true;
        justPressedTrigger = true;
        
    }

    protected void FireAuto()
    {
        if (Time.time - lastFired > 1 / fireRate ) {
            FireSingleBullet();
        }



    }


    protected void FireBurst()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            Debug.Log("Gun is ready to burst!");


            if (remainingBurstBullet > 0 && Time.time - lastBurstBulletFired > 1 / burstFireRate)
            {
                Debug.Log("Next burst bullet is ready to fire");
                FireSingleBullet();

                remainingBurstBullet -= 1;
                
    
                lastBurstBulletFired = Time.time;

                if (remainingBurstBullet == 0) {
                    Debug.Log("Burst Fire completed");
                    remainingBurstBullet = numberOfBulletsToBurst;
                    lastFired = Time.time;
                }
            }



        }

    }




    // Start is called before the first frame update
    void Start()
    {
        lastFired = -999f;
        remainingBurstBullet = numberOfBulletsToBurst;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        shootingSound = GetComponent<AudioSource>();
        
        grabbable.activated.AddListener(setIsHoldingTriggerTRUE);
        grabbable.deactivated.AddListener(setIsHoldingTriggerFALSE);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingTrigger) {
            Fire();
        }  
    }
}
