using UnityEngine;
using System.Collections;

public class FlipItem : Itm
{
    public float flipTime = 10f; //���� ���� �ð�
    public float effectTime = 10f; //ȭ�� ��ȿ�� ���� �ð�
    public Color effectColor = Color.magenta; //ȭ���ȿ�� ��

    Camera mainCamera; //ī�޶�
    Color originalColor; //���� ��

    void Start()
    {
        //ī�Ŷ����ͼ�ȿ������
        mainCamera = Camera.main;
        originalColor = mainCamera.backgroundColor;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //�÷��̾����ӽ� ȿ��
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            StartCoroutine(Flip(player));
        }
    }

    IEnumerator Flip(Player player)
    {
        player.isFlipped = true; //�¿���� Ȱ��ȭ
        StartCoroutine(ScreenEffect()); //ȭ�� ���� ����

        //10�� ��� 
        yield return new WaitForSeconds(flipTime);
        player.isFlipped = false; //�¿���� ��Ȱ��ȭ
    }

    IEnumerator ScreenEffect()
    {
        mainCamera.backgroundColor = effectColor; // ��������
        yield return new WaitForSeconds(effectTime); //ȿ�����ӽð�
        mainCamera.backgroundColor = originalColor; //���� ��� ��������
    }
}
