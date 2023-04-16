using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    
    public float health;
    // Start is called before the first frame update
    Animator animator;
    public bool isDying = false;
    private float timeToDestroy = 1.3f;

    // Start is called before the first frame update
    public GameObject player;
    public float speed;
    public float attackSpeed=0.7f;
    private float timeToAttack = 0f;
    public AudioClip[] attackSounds;
    public AudioClip[] deathSounds;
    public AudioClip[] hitSounds;

    private void Start(){
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Health
        if (health <= 0)
        {   
            
            if(timeToDestroy == 1.3f){
                animator.SetTrigger("die");
                GetComponent<AudioSource>().clip = attackSounds[Random.Range(0, deathSounds.Length-1)];
                GetComponent<AudioSource>().Play();
                GameManager.Instance.AddXp(GameManager.Instance.enemyXpReward1);
                
            }
            if(timeToDestroy <= 0f){
                Destroy(gameObject);
            }
            timeToDestroy -= Time.deltaTime;
            
        }else{
            //Enemy Movement
            timeToAttack -= Time.deltaTime;
            transform.LookAt(player.transform);
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance > 0.8f) { 
                // Enemy is moving to the player
                transform.position += transform.forward * speed * Time.deltaTime;
                animator.SetBool("isRunning", true);
            }else{
                // Enemy is attacking to the player
                animator.SetBool("isRunning", false);
                if(timeToAttack <= 0f){
                    if(!GetComponent<AudioSource>().isPlaying){
                        GetComponent<AudioSource>().clip = attackSounds[Random.Range(0, attackSounds.Length-1)];
                        GetComponent<AudioSource>().Play();
                    }
                    animator.SetTrigger("attack");

                    timeToAttack = 1f / attackSpeed;
                }else{
                    timeToAttack -= Time.deltaTime;
                }
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(!GetComponent<AudioSource>().isPlaying){
            GetComponent<AudioSource>().clip = hitSounds[Random.Range(0, attackSounds.Length-1)];
            GetComponent<AudioSource>().Play();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
