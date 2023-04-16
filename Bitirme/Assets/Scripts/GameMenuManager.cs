using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuManager : MonoBehaviour
{

    public GameObject menu;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2f;
    public TextMeshProUGUI levelArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            Debug.Log("Show Menu");
            // menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        
        }

        if (menu.activeSelf)
        {
            menu.transform.position =
                head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1;
        }
        if (GameManager.Instance == null)
            levelArea.text = "";
        else
        levelArea.text = "XP:" + Mathf.FloorToInt(GameManager.Instance.xp) +"/"+ Mathf.FloorToInt(GameManager.Instance.requiredXp) + "\n Level:"+ Mathf.FloorToInt(GameManager.Instance.level);
    }
}
