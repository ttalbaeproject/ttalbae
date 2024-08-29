using UnityEngine;

public class SpeedDownItm : Itm
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.effects.Add(new SpeedDownEffect(10));

            Destroy(gameObject);
        }
    }
}
