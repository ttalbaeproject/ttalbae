using UnityEngine;
using System.Collections;

public class FlipItem : Item
{
    public float flipTime = 10f; //반전 지속 시간
    public float effectTime = 10f; //화면 색효과 지속 시간
    public Color effectColor = Color.magenta; //화면색효과 색

    Camera mainCamera; //카메라
    Color originalColor; //원래 색

    void Start()
    {
        //카매라가져와서효과적용
        mainCamera = Camera.main;
        originalColor = mainCamera.backgroundColor; //배경 원래색 저장
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //플래이어접속시 효과
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            StartCoroutine(Flip(player)); //화면전환시작
        }
    }

    IEnumerator Flip(Player player)
    {
        player.isFlipped = true; //좌우반전 활성화
        StartCoroutine(ScreenEffect()); //화면 색상 변경

        //10초 대기 
        yield return new WaitForSeconds(flipTime);
        player.isFlipped = false; //좌우반전 비활성화
    }

    IEnumerator ScreenEffect()
    {
        mainCamera.backgroundColor = effectColor; // 배경색깔설정
        yield return new WaitForSeconds(effectTime); //효과지속시간
        mainCamera.backgroundColor = originalColor; //원래 배경 색상적용
    }
}
