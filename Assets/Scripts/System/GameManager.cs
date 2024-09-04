using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public float playTime;

    public GameObject pizza;
    Vector2 pizzaPos = new(-12.4f, -5.714f);
    [SerializeField] GameObject pizza1;
    [SerializeField] GameObject pizza2;
    [SerializeField] GameObject pizza3;

    public bool IsStarted;
    void Start()
    {
        Instance = this;

        StartCoroutine(cutScene());
    }

    void Update()
    {
        if (IsStarted) {
            playTime += Time.deltaTime;
        }
    }

    IEnumerator dropItem(GameObject obj, float time) {
        obj.SetActive(true);
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        Vector2 startPos = obj.transform.position;

        obj.transform.position = new Vector3(startPos.x, startPos.y + 1);

        Color col = renderer.color;
        col.a = 0;

        for (int i = 0; i <= 20; i++) {
            col.a = i * 0.05f;

            yield return new WaitForSeconds(time / 20);
        }
    }

    void ShotPizza() {
        var piz = Instantiate(pizza, pizzaPos, Quaternion.identity);
        Rigidbody2D rb = piz.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 2 + Vector2.right * 4, ForceMode2D.Impulse);
    }

    public void DropPizza(Vector2 pos, float facing) {
        var piz = Instantiate(pizza, pos, Quaternion.identity);
        Rigidbody2D rb = piz.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 2 + Vector2.right * 4 * facing, ForceMode2D.Impulse);
    }

    IEnumerator cutScene() {
        pizza1.SetActive(false);
        pizza2.SetActive(false);
        pizza3.SetActive(false);

        Player.Main.facingRight = false;
        var movement = Player.Main.GetComponent<Movement>();
        movement.canMove = false;

        yield return new WaitForSeconds(1f);

        CamManager.main.Offset(new Vector2(-0.86f, 1.4f), 1);
        CamManager.main.CloseUp(3, 0, 1);

        yield return new WaitForSeconds(1f);

        Player.Main.Comment("흐아...");

        StartCoroutine(dropItem(pizza3, 0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(dropItem(pizza2, 0.3f));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(dropItem(pizza1, 0.3f));
        yield return new WaitForSeconds(1f);

        float rd = Random.Range(0, 100);
        if (rd <= 30f) 
            Player.Main.Comment("빨리 돈 벌어서 집 사야지 ㅜㅜ");
        else if (rd <= 60f) 
            Player.Main.Comment("오늘도 왜케 많은거야...");
        else if (rd <= 90f) 
            Player.Main.Comment("왜 나만 오면 주문만 하는거냐");
        else
            Player.Main.Comment("ㅅ발");

        ShotPizza();
        yield return new WaitForSeconds(0.5f);
        ShotPizza();
        yield return new WaitForSeconds(0.5f);
        ShotPizza();

        yield return new WaitForSeconds(1.5f);

        Player.Main.facingRight = true;

        yield return new WaitForSeconds(0.5f);

        Player.Main.Comment("준비하시고...");

        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.title.text = "GO! GO!";
        Player.Main.Comment("좋아 가보자고!");

        IsStarted = true;

        CamManager.main.Offset(Vector2.zero, 0.5f);
        CamManager.main.CloseOut(0.5f);

        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.title.text = "";

        movement.canMove = true;
       

        yield break;
    }
}
