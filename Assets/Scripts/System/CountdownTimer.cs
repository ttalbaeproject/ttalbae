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
    public Text deliverTxt, scoreTxt;
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

        var before = Ranking.data.Find((v)=>v.name == name);

        if (before != null) {
            if (before.score < GameManager.Instance.fullScore) {
                before.score = GameManager.Instance.fullScore;
                before.deliver = Player.Main.success;
            }
        } else {
            Ranking.data.Add(new RankData(){
                name = name,
                deliver = Player.Main.success,
                score = GameManager.Instance.fullScore,
            });
        }

        SoundManager.Instance.Play("music.menu");

        Ranking.Store();

        deliverTxt.text = "성공한 배달: " + Player.Main.success + "회";
        scoreTxt.text = "내 성과: " + GameManager.Instance.fullScore + "pt";
        
        panel.SetActive(true);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);

        StartCoroutine(anim());
    }

    IEnumerator anim() {
        for (int i = 0; i <= 40; i++) {
            Color colImg = img.color;
            colImg.a = i / 40f;

            img.color = colImg;
            deliverTxt.color = colImg;
            scoreTxt.color = colImg;

            yield return new WaitForSeconds(0.05f);
        }

        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
    }
}