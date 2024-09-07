using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    static string nextScene;
    public GameObject[] images;

    [SerializeField]
    Slider progress;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void Start()
    {
        StartCoroutine(SceneProcess());
    }

    IEnumerator SceneProcess()
    {
        yield return new WaitForSeconds(1);
        
        AsyncOperation load = SceneManager.LoadSceneAsync(nextScene);
        load.allowSceneActivation = false;

        float timer = 0f;
        while (!load.isDone)
        {
            yield return null;

            timer += Time.unscaledTime;
            if (load.progress < 0.9f)
            {
                 progress.value = Mathf.Lerp(progress.value, load.progress, timer);
                if (progress.value >= load.progress)
                {
                    timer = 0f;
                }
            } else
            {
                progress.value = Mathf.Lerp(0.9f, 1f, timer);
                if (progress.value >= 1f)
                {
                    Debug.Log(timer);
                    load.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
