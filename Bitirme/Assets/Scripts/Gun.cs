using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FireType  {Single, Auto, Burst};
public enum BulletType {Raycast, Projectile };
public class Gun : MonoBehaviour {

    [Header("References")]
    public Transform bulletSpawnPoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletHitEffect;
    public AudioSource shootingSound;


    [Header("Gun Config")]
    public FireType fireType;
    public BulletType bulletType;

    public float damage;
    public float fireRate;
    
    private int remainingBurstBullet;

    [HideInInspector]
    public int ammoCount;


    [Header("Burst Fire Config")]
    [Range(2, 5)]
    public int numberOfBulletsToBurst;
    [Range(8,32)]
    public float burstFireRate; 
    private float lastBurstBulletFired; // smaller one




    private float lastFired;




    



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


    public void Fire() { 
        Debug.Log("Fire()");
        if (HasEnoughAmmo()) {
            Debug.Log("Gun has enough ammo to fire!");
            switch (fireType) {
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
            FireEffects();
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
        

        if (bulletType == BulletType.Raycast)
        {
            CalculateBulletRaycastHit();

        }
        else {
            SpawnBulletProjectile();
        }

        SpendAmmunition();
        //update ammo text

        if (fireType != FireType.Burst) { 
            lastFired = Time.time;
            Debug.Log(lastFired);
        
        }
    }


    protected void FireAuto()
    {
        if (Time.time - lastFired > 1 / fireRate) {
            FireSingle();
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
                FireSingle();

                remainingBurstBullet -= 1;
                
    
                lastBurstBulletFired = Time.time;

                if (remainingBurstBullet == 0) {
                    Debug.Log("Burst Fire completed");
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
