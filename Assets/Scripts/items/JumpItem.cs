using UnityEngine;

public class JumpItem : Item
{
    public Trajectory trajectory;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;

    //���� ���º̤Ť���
    float originalDotSpace;
    Rigidbody2D playerRigidbody;
    float originalPlayerGravity;
    public bool is_jump = false;
    public bool is_effect = false;

    public float DotSpace = 0.1f;
    public float gravityScale = 1f;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //���� ������ ���� ����
        originalDotSpace = trajectory.DotSpacing;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            playerRigidbody = player.GetComponent<Rigidbody2D>(); //�÷��̾��� ������ٵ� ��������

            //������ �� ���̰Ÿ� ����
            trajectory.DotSpacing = DotSpace;

            //������ �Ÿ� ���̱�
            originalPlayerGravity = playerRigidbody.gravityScale;
            playerRigidbody.gravityScale = gravityScale;

            //������ �����
            spriteRenderer.enabled = false;
            collider.enabled = false;
            is_effect = true;
        }
    }

    //ȿ���� �����ϴ� �޼���
    public void Reset()
    {
        //�߷º���
        playerRigidbody.gravityScale = originalPlayerGravity;

        //������ ����
        trajectory.DotSpacing = originalDotSpace;

        is_effect = false;
        Destroy(gameObject);
    }
}
