using UnityEngine;

public class JumpItem : Item
{
    public Trajectory trajectory;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;

    //���� ���º̤Ť���
    float originalDotSpace;
    Color originalColor;
    Rigidbody2D playerRigidbody;
    float originalPlayerGravity;
    public bool is_jump = false;
    public bool is_effect = false;
    public SpriteRenderer dotSprite;
    public float DotSpace = 0.1f;
    public float gravityScale = 1f;

    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //���� ������ ���� ����
        originalDotSpace = trajectory.DotSpacing;
        originalColor = dotSprite.color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) //�÷��̾�ε�������
        {
            Player player = collision.collider.GetComponent<Player>();
            playerRigidbody = player.GetComponent<Rigidbody2D>(); //�÷��̾��� ������ٵ� ��������

            //������ �� ���̰Ÿ� ����
            trajectory.DotSpacing = DotSpace;
            trajectory.SetDotsColor(Color.yellow);
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
        for (int i = 0; i < trajectory.DotsList.Length; i++)
        {
            SpriteRenderer dotRenderer = trajectory.DotsList[i].GetComponent<SpriteRenderer>();
            if (dotRenderer != null)
            {
                dotRenderer.color = originalColor;
            }
        }
        is_effect = false;
        Destroy(gameObject);//������ ����
    }
}
