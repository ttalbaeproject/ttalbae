using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandItem : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            // playerRigidbody = player.GetComponent<Rigidbody2D>(); //�÷��̾��� ������ٵ� ��������

            // //������ �� ���̰Ÿ� ����
            // trajectory.DotSpacing = DotSpace;

            // //������ �Ÿ� ���̱�
            // originalPlayerGravity = playerRigidbody.gravityScale;
            // playerRigidbody.gravityScale = gravityScale;

            // //������ �����
            // spriteRenderer.enabled = false;
            // collider.enabled = false;
            // is_effect = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
