using UnityEngine;
using System.Collections;

public class SpeedItem : Item
{
    public float speed = 2f; //속도 증가
    public float effectTime = 10f; //지속 시간
    public Color effectColor = Color.red;
    Color originalColor; //원래 색상
    float originalSpeed; //원래 속도
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
            Effect(player); //효과 적용
        }
    }

    void Effect(Player player)
    {
        //원래 색상과 속도 저장
        originalColor = player.render.color;
        originalSpeed = playerMovement.speed; // 속도 저장

        //플레이어의 색상과 속도 변경
        player.render.color = effectColor;
        playerMovement.speed *= speed;

        spriteRenderer.enabled = false; //아이템숨기기
        collider.enabled = false; //충돌없애기

        //효과 유지
        StartCoroutine(EffectDuration());
    }

    IEnumerator EffectDuration()
    {
        //지정된 시간 동안 하기
        yield return new WaitForSeconds(effectTime);

        //색상과 속도를 원래대로 복원
        Player player = Player.Main; //씬의 플레이어를 가져옴        
            player.render.color = originalColor;
            playerMovement.speed = originalSpeed;//원래색속도로바뀌기

        // 아이템 제거
        Destroy(gameObject);
    }
}
