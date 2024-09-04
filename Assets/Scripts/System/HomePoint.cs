using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePoint : MonoBehaviour
{
    public bool stopDetect;
    public bool IsOutScreen()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var isOutScreen = screenPoint.x <= 0 || screenPoint.x >= Screen.width || screenPoint.y <= 0 || screenPoint.y >= Screen.height;
        return isOutScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopDetect) return;

        if (Player.Main.pizza <= 0 && GameManager.Instance.pizzas.Count <= 0) {
            float dist = Vector2.Distance(transform.position, Player.Main.transform.position);
            if (GameManager.Instance.IsStarted && dist <= 2.5f && Player.Main.OnGround()) {
                GameManager.Instance.SupplyPizza(this);
            }

            UIManager.Instance.deliver_dist.text = (Mathf.Floor(dist * 10) / 10).ToString() + "M";
            
            if (IsOutScreen()) {
                UIManager.Instance.indicator.DrawIndicator(gameObject, UIManager.Instance.indicate_mark);
            } else {
                UIManager.Instance.indicate_mark.transform.position = new Vector3(transform.position.x, transform.position.y, UIManager.Instance.indicate_mark.transform.position.z);
            }
        }
    }
}
