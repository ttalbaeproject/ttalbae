using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Time.timeScale = 1;
        LoadingController.LoadScene("startScene");

        Itm.items.Clear();
    }

    public void RetryButton()
    {
        CountdownTimer.Instance.panel.SetActive(false);
        CountdownTimer.Instance.panel.SetActive(false);
        GameManager.Instance.Start();

        SoundManager.Instance.Play("music.sfx");
    }
}
