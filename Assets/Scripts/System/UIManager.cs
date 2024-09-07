using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    public Text actionText, heightText, pizzaText, title, deliver_dist, scoreText, succText, missionText, tutoText, pageText;
    public GameObject hud, indicate_mark, pause, tuto, tutoNext, tutoPrev;
    public Indicator indicator;
    public float actionTime;
    public Transform effects;
    public EffectIcon effectIcon;
    public Image tutoImage;
    public Sprite tuto1, tuto2, tuto3;
    public bool inTuto;
    int page;
    void Awake()
    {
        Instance = this;
    }

    public void Pause() {
        Time.timeScale = 0;
        pause.gameObject.SetActive(true);
    }
    public void Resume() {
        Time.timeScale = 1;
        pause.gameObject.SetActive(false);
    }

    public void ShowTuto(int pg = 1) {
        inTuto = true;
        tuto.SetActive(true);

        page = pg;

        if (page == 1) {
            tutoImage.sprite = tuto1;
            tutoText.text = "A, D를 눌러 좌, 우로 이동할 수 있습니다.";

            tutoPrev.SetActive(false);
            tutoNext.SetActive(true);
        } else if (page == 2) {
            tutoImage.sprite = tuto2;
            tutoText.text = "마우스 좌클릭 또는 SPACE를 누른 상태로 마우스 커서를 움직인 후 놓으면 원하는 강도로 점프할 수 있습니다.";

            tutoPrev.SetActive(true);
            tutoNext.SetActive(true);
        } else if (page == 3) {
            tutoImage.sprite = tuto3;
            tutoText.text = "점프하는 순간에 W를 꾹 누르면 더 높게, A / D를 꾹 누르면 더 멀리 점프할 수 있습니다.";

            tutoPrev.SetActive(true);
            tutoNext.SetActive(false);
        }
    }

    public void CloseTuto() {
        inTuto = false;
        tuto.SetActive(false);

        SoundManager.Instance.Play("sfx.click");
    }

    public void Nextpage() {
        ShowTuto(page+1);

        SoundManager.Instance.Play("sfx.click");
    }

    public void Prevpage() {
        ShowTuto(page-1);

        SoundManager.Instance.Play("sfx.click");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }

        if (actionTime > 0) {
            actionTime -= Time.deltaTime;
        } else {
            actionText.text = "";
        }

        heightText.text = Mathf.Floor(Player.Main.transform.position.y + 7).ToString() + "M";
        pizzaText.text = "보유한 피자: " + Player.Main.pizza.ToString() + "개";

        scoreText.text = GameManager.Instance.fullScore.ToString() + "pt";
        succText.text = Player.Main.success.ToString();

        if (Player.Main.pizza <= 0) {
            if (GameManager.Instance.pizzas.Count > 0) {
                missionText.text = "피자를 모두 떨어뜨렸습니다!\n떨어진 피자를 주워서 목표 위치까지 배달하세요!";
            } else {
                missionText.text = "피자를 전부 배달했습니다!\n피자집으로 돌아가서 다시 피자를 받아오세요!";
            }
        } else {
            missionText.text = "피자를 챙겼습니다!\n목표 위치까지 배달하세요!";
        }
    }

    public void SetActionText(string txt, Color col, bool overrid = true) {
        if (!overrid && actionText.text != "" && actionText.text != "점프") {
            return;
        }

        actionText.text = txt;
        actionText.color = col;

        actionTime = 0.8f;
    }
}
