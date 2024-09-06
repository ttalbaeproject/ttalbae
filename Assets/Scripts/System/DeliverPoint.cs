using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverPoint : MonoBehaviour
{
    public int point;
    void Start()
    {
        if (!GameManager.Instance.Points.Contains(this) && !GameManager.Instance.endedPoint.Contains(this)) {
            GameManager.Instance.Points.Add(this);
            gameObject.SetActive(false);
        }

        point = Mathf.RoundToInt(Vector2.Distance(transform.position, Player.Main.transform.position) * 0.6f);
    }

    public bool IsOutScreen()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var isOutScreen = screenPoint.x <= 0 || screenPoint.x >= Screen.width || screenPoint.y <= 0 || screenPoint.y >= Screen.height;
        return isOutScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.Main.pizza > 0 || GameManager.Instance.pizzas.Count > 0) && GameManager.Instance.IsStarted) {
            float dist = Vector2.Distance(transform.position, Player.Main.transform.position);
            if (dist <= 2.5f && Player.Main.OnGround()) {
                GameManager.Instance.EnterPoint(this);
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
