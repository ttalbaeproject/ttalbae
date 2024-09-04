using UnityEngine;

public class PizzaItm : Itm
{
    float lifetime;

    void Update() {
        lifetime += Time.deltaTime;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (lifetime < 0.3f) return;
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.pizza++;

            Destroy(gameObject);
        }
    }
}
