using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speed = 2f; //�ӵ� ����
    public float effectTime = 10f; //���� �ð�
    public Color effectColor = Color.red;
    Color originalColor; //���� ����
    float originalSpeed; //���� �ӵ�
    Movement playerMovement;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            playerMovement = player.GetComponent<Movement>();
            Effect(player); //ȿ�� ����
        }
    }

    void Effect(Player player)
    {
        //���� ����� �ӵ� ����
        originalColor = player.render.color;
        originalSpeed = playerMovement.speed; // �ӵ� ����

        //�÷��̾��� ����� �ӵ� ����
        player.render.color = effectColor;
        playerMovement.speed *= speed;

        spriteRenderer.enabled = false; //�����ۼ����
        collider.enabled = false; //�浹���ֱ�

        //ȿ�� ����
        StartCoroutine(EffectDuration());
    }

    IEnumerator EffectDuration()
    {
        //������ �ð� ���� �ϱ�
        yield return new WaitForSeconds(effectTime);

        //����� �ӵ��� ������� ����
        Player player = Player.Main; //���� �÷��̾ ������        
            player.render.color = originalColor;
            playerMovement.speed = originalSpeed;//�������ӵ��ιٲ��

        // ������ ����
        Destroy(gameObject);
    }
}
