using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}
    public Text actionText, heightText;
    public float actionTime;
    void Start()
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

        heightText.text = Mathf.Floor(Player.Main.transform.position.y + 6).ToString() + "M";
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
