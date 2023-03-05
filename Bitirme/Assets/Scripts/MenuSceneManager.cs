using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeCurrentSceneToPolygon() {
        SceneManager.LoadScene(1);
    }

    public void doExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
