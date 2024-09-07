using UnityEngine;

public class SpeedUpItm : Itm
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            player.effects.Add(new SpeedUpEffect(10));

            gameObject.SetActive(false);

            SoundManager.Instance.Play("sfx.item");
        }
    }
}
