using UnityEngine;

public class JumpUpItm : Itm
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.AddEffect(new JumpUpEffect(10));

            gameObject.SetActive(false);

            SoundManager.Instance.Play("sfx.item");
        }
    }
}
