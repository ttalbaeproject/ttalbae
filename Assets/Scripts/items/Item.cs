using System.Collections.Generic;
using UnityEngine;

public class Itm : MonoBehaviour
{
    public static List<Itm> items = new();
    void Start() {
        items.Add(this);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
