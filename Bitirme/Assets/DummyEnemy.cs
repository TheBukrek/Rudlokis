using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject Player;

    private Rigidbody m_Rigidbody;
    public float maxHealth = 100;
    public float currentHealth;
    public float moveSpeed = 2.2f;
    public float turnAroundSpeed = 3.3f;
    public float dashSpeed = 6.0f;
    public float dashTime = 0.5f;
    public float dashTimeCurrent = 0.5f;
    public float dashCooldown = 5.0f;
    public float dashCooldownCurrent = 5.0f;
    public bool dashReady = false;
    private Vector3 dashTarget;
    void Start()
    {
         m_Rigidbody = transform.GetChild(0).gameObject. GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {

            gameObject.SetActive(false);

        }
        if (dashReady)
        {
            dashTimeCurrent -= Time.deltaTime;

            if (dashTimeCurrent < 0)
            {
                dashReady = false;
                dashTimeCurrent = dashTime;
            }
            else
            {
                //dash through to player's last position
                //transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
                m_Rigidbody.AddForce(transform.forward * dashSpeed * Time.deltaTime);
                //transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            }
        }
        else
        {
            //move towards to player normally
            //transform.position=Vector3.MoveTowards(transform.position,Player.transform.position , (moveSpeed) * Time.deltaTime);
            m_Rigidbody.AddForce(transform.forward * moveSpeed / (dashCooldownCurrent + 1.0f) * Time.deltaTime);

            dashCooldownCurrent -= Time.deltaTime;
            if (dashCooldownCurrent <= 0)
            {
                dashReady = true;
                dashTarget = Player.transform.position;
                dashCooldownCurrent = dashCooldown;
            }
        }


        //slowly look at player
        if (!dashReady)
        {
            //fly above to observe
            m_Rigidbody.AddForce(transform.up * Random.Range(12.0f / dashCooldown, 240.0f / dashCooldown) * Time.deltaTime);

            /*Vector3 relativePos = Player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnAroundSpeed * Time.deltaTime);
            */
        }
        //transform.LookAt(Player.transform);
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        
        if (other.tag != "Enemy")
        {

            
            TakeDamage(50);
        }
    }
   
}
