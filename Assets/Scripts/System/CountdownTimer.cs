using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public static CountdownTimer Instance;
    public float timeDefault;
    public float timeRemaining;
    public bool timerIsRunning;
    public GameObject panel;
    public Image img;
    public Text deliverTxt, scoreTxt, rankText, cmtText, bestText;
    public Button btn1, btn2;

    void Start()
    {
        Instance = this;

        panel.SetActive(false);
        UpdateCountdownText();
    }

    void Update()
    {
        if (timerIsRunning && GameManager.Instance.IsStarted)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownText();
            }
            else
            {
                // 타이머가 끝나면 게임씬으로 전환 (또는 다른 액션)
                timeRemaining = 0;
                timerIsRunning = false;
                OnCountdownFinished();
            }
        }
    }

    void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnCountdownFinished()
    {
        var movement = Player.Main.GetComponent<Movement>();
        movement.canMove = false;

        string name = PlayerPrefs.GetString("PlayerName");

        RankData myData;

        var before = Ranking.data.Find((v)=>v.name == name);

        myData = before;

        cmtText.text = "";

        if (before != null) {
            if (before.score < GameManager.Instance.fullScore) {
                before.score = GameManager.Instance.fullScore;
                before.deliver = Player.Main.success;

                cmtText.text = "<color=\"green\">신기록!</color>";
            }
        } else {
            myData = new RankData(){
                name = name,
                deliver = Player.Main.success,
                score = GameManager.Instance.fullScore,
            };

            Ranking.data.Add(myData);

            cmtText.text = "<color=\"red\">첫기록!</color>";
        }

        SoundManager.Instance.Play("music.menu");

        Ranking.Store();

        var sorted = Ranking.data;
        sorted.Sort((a, b)=>b.score.CompareTo(a.score));

        bestText.text = "내 BEST 성과: " + (before != null ? before.score : 0) + "pt";
        rankText.text = "현재 순위: " + (sorted.IndexOf(myData) + 1).ToString() + "위";
        deliverTxt.text = "성공한 배달: " + Player.Main.success + "회";
        scoreTxt.text = "내 성과: " + GameManager.Instance.fullScore + "pt";
        
        panel.SetActive(true);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);

        StartCoroutine(anim());
    }

    IEnumerator anim() {
        for (int i = 0; i <= 50; i++) {
            Color colImg = img.color;
            colImg.a = i / 50f;

            img.color = colImg;
            deliverTxt.color = scoreTxt.color = colImg;

            yield return new WaitForSeconds(0.05f);
        }

        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
    }
}