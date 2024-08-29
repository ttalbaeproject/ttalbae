using UnityEngine;

public class Itm : MonoBehaviour
{
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //�÷��̾�����˽� ����������
        if (collision.collider.CompareTag("Player"))
        {
             Destroy(gameObject); //������ ����
        }
    }
}
