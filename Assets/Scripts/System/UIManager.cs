using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    public Text actionText, heightText, pizzaText, title, deliver_dist, scoreText, succText, missionText;
    public GameObject hud, indicate_mark;
    public Indicator indicator;
    public float actionTime;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
