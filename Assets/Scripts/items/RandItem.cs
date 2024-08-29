using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandItm : Itm
{
    string RandVal() {
        string[] vals = {
            "speedUp",
            "speedDown",
            "jumpUp"
        };

        return vals[Random.Range(0, vals.Length)];
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            
            switch (RandVal())
            {
                case "speedUp":
                    player.effects.Add(new SpeedUpEffect(10));
                    break;
                case "speedDown":
                    player.effects.Add(new SpeedUpEffect(10));
                    break;
                case "jumpUp":
                    player.effects.Add(new JumpUpEffect(10));
                    break;
            }

            Destroy(gameObject);
        }
    }
}
