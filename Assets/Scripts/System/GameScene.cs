using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
