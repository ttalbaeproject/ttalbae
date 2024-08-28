using UnityEngine;
//����� ����غ����ٰ��ȵǼ� ������ �κ�
public class Item : MonoBehaviour
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
