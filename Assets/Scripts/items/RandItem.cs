using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandItm : Itm
{
    string RandVal() {
        string[] vals = {
            "speedUp",
            "speedDown",
            "jumpUp",
            "flip"
        };
        

        return vals[Mathf.RoundToInt(Random.Range(0f, vals.Length - 1))];
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            
            switch (RandVal())
            {
                case "speedUp":
                    player.AddEffect(new SpeedUpEffect(10));
                    break;
                case "speedDown":
                    player.AddEffect(new SpeedDownEffect(10));
                    break;
                case "jumpUp":
                    player.AddEffect(new JumpUpEffect(10));
                    break;
                case "jumpDown":
                    player.AddEffect(new JumpDownEffect(10));
                    break;
                case "flip":
                    player.AddEffect(new FlipEffect(10));
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
