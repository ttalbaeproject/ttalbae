using UnityEngine;

public class JumpDownItm : Itm
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            
            player.AddEffect(new JumpDownEffect(10));

            gameObject.SetActive(false);
        }
    }
}
