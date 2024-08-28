using UnityEngine;
//여기는 상속해볼려다가안되서 포기한 부분
public class Item : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //플래이어와접촉시 아이템제거
        if (collision.collider.CompareTag("Player"))
        {
             Destroy(gameObject); //아이템 제거
        }
    }
}
