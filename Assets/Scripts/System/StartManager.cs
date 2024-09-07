using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject btn1, btn2, background, title, content, rankingPanel;
    public RankPerson rankPerson;
    public InputField nameInput;
    void Start()
    {
        Ranking.Load();
        rankingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectStart() {
        SoundManager.Instance.Play("sfx.start");

        StartCoroutine(changeMotion());
    }

    public void StartStart() {
        SoundManager.Instance.Play("sfx.click");

        string name = nameInput.text;

        if (name == "clear") {
            nameInput.text = "";
            Ranking.data.Clear();
            Ranking.Store();

            return;
        }

        if (name.Length < 1) {
            return;
        }

        PlayerPrefs.SetString("PlayerName", name);

        LoadingController.LoadScene("gameScene");
    }

    IEnumerator changeMotion() {
        btn1.SetActive(false);
        btn2.SetActive(false);
        title.SetActive(false);

        LeanTween.scaleX(background, -1, 0.6f).setEaseOutElastic();
        yield return new WaitForSeconds(0.6f);

        ShowRank();
    }

    public void HideRank() {
        SoundManager.Instance.Play("sfx.click");

        StartCoroutine(hideMotion());
    }

    IEnumerator hideMotion() {
        btn1.SetActive(false);
        btn2.SetActive(false);
        title.SetActive(false);
        rankingPanel.SetActive(false);

        LeanTween.scaleX(background, 1, 0.6f).setEaseOutElastic();
        yield return new WaitForSeconds(0.6f);

        btn1.SetActive(true);
        btn2.SetActive(true);
        title.SetActive(true);
    }

    public void ShowRank() {
        rankingPanel.SetActive(true);

        foreach (Transform child in content.transform) {
            Destroy(child.gameObject);
        }

        var sorted = Ranking.data;
        sorted.Sort((a, b)=>b.score.CompareTo(a.score));

        for (int i = 0; i < sorted.Count; i++) {
            var pr = Instantiate(rankPerson, content.transform);
            pr.rank.text = (i + 1).ToString();
            pr.Name.text = sorted[i].name;
            pr.score.text = sorted[i].score.ToString();
            pr.deliver.text = sorted[i].deliver.ToString();
        }
    }
}
