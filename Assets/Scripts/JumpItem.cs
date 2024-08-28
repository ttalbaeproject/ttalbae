using UnityEngine;

public class JumpItem : Item
{
    public Trajectory trajectory;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;

    //원래 상태뵤ㅕㄴ수
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

        //원래 포물선 간격 저장
        originalDotSpace = trajectory.DotSpacing;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            playerRigidbody = player.GetComponent<Rigidbody2D>(); //플래이어의 리지드바디 가져오기

            //포물선 원 사이거리 증가
            trajectory.DotSpacing = DotSpace;

            //조머프 거리 높이기
            originalPlayerGravity = playerRigidbody.gravityScale;
            playerRigidbody.gravityScale = gravityScale;

            //아이템 숨기기
            spriteRenderer.enabled = false;
            collider.enabled = false;
            is_effect = true;
        }
    }

    //효과를 복구하는 메서드
    public void Reset()
    {
        //중력복구
        playerRigidbody.gravityScale = originalPlayerGravity;

        //포물선 복구
        trajectory.DotSpacing = originalDotSpace;

        is_effect = false;
        Destroy(gameObject);
    }
}
