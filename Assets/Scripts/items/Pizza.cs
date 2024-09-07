using UnityEngine;

public class PizzaItm : MonoBehaviour
{
    public float lifetime;

    void Start() {
        GameManager.Instance.pizzas.Add(this);
    }

    void Update() {
        lifetime += Time.deltaTime;

        if (lifetime >= -8 && lifetime < 0) {
            GameManager.Instance.pizzas.Remove(this);
            Instantiate(GameManager.Instance.npc, transform.position + new Vector3(0, 0.5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (lifetime < 0.3f) return;
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.pizza++;

            if (GameManager.Instance.IsStarted) {
                SoundManager.Instance.Play("sfx.drop");
            }

            GameManager.Instance.pizzas.Remove(this);
            Destroy(gameObject);
        }
    }
}
