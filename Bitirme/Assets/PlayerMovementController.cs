using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class PlayerMovementController : NetworkBehaviour
{
    public float Speed = 0.1f;
    public GameObject PlayerModel;
    // Start is called before the first frame update
    public void Movement() {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

        transform.position += moveDirection * Speed;
    }

    public void Start()
    {
        PlayerModel.SetActive(false);

    }
    public void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 0.8f, Random.Range(-15, 7));
    }
    private void Update()
    {
        if (PlayerModel.activeSelf == false && SceneManager.GetActiveScene().name == "OnlineDeneme")
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("ldkfnslk");
                SceneManager.LoadScene("MainMenuScene");
            }
            SetPosition();
            PlayerModel.SetActive(true);
        }
        if (isOwned)
        {
            Movement();
        }
    }
}
