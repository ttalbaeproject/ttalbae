using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public float playTime, deliverTime;
    public int score;
    public int fullScore;

    public PizzaItm pizza;
    Vector2 pizzaPos = new(-12.4f, -5.714f);
    [SerializeField] GameObject pizza1;
    [SerializeField] GameObject pizza2;
    [SerializeField] GameObject pizza3;
    public Npc npc;
    public List<DeliverPoint> Points = new();
    public List<DeliverPoint> endedPoint = new();
    public List<PizzaItm> pizzas = new();

    public bool IsStarted;
    void Awake() {
        Instance = this;
    }
    public void Start()
    {
        StartCoroutine(cutScene());
    }

    void Update()
    {
        if (IsStarted) {
            playTime += Time.deltaTime;
            deliverTime += Time.deltaTime;

            fullScore = Player.Main.success * 30 + score;

            if (Player.Main.notMove > 50) {
                UIManager.Instance.title.text = "곧 시작 화면으로 돌아갑니다.";
            }

            if (Player.Main.notMove > 60) {
                SceneManager.LoadScene("startScene");
            }
        }
    }

    public void EnterPoint(DeliverPoint point) {
        if (Player.Main.pizza <= 0) {
            Player.Main.Comment("<color=\"red\">피자를 안 들고 왔잖아!!!!!!</color>");

            return;
        }

        Points.Remove(point);
        endedPoint.Add(point);

        score += (int)(point.point - deliverTime * 0.05f);

        deliverTime = 0;

        Player.Main.pizza--;
        Player.Main.success++;

        float dist = Mathf.Abs(point.transform.position.x - Player.Main.transform.position.x);

        if (point.transform.position.x > Player.Main.transform.position.x) {
            ShotDummyPizza(1, dist);
        } else if (point.transform.position.x < Player.Main.transform.position.x) {
            ShotDummyPizza(-1, dist);
        } else {
            ShotDummyPizza(0, 0);
        }

        if (Points.Count <= 0) {
            RepairPoint();
        }

        EnableNextPoint();

        Player.Main.Comment("자 이제 다음으로 가자!");
    }

    public void RepairPoint() {
        foreach (DeliverPoint point in endedPoint) {
            Points.Add(point);
        }

        endedPoint.Clear();

        foreach (DeliverPoint point in Points) {
            point.gameObject.SetActive(false);
        }
        foreach (DeliverPoint point in endedPoint) {
            point.gameObject.SetActive(false);
        }
    }

    public void EnableNextPoint() {
        foreach (DeliverPoint p in Points) {
            p.gameObject.SetActive(false);
        }
        foreach (DeliverPoint p in endedPoint) {
            p.gameObject.SetActive(false);
        }

        var point = Points[Mathf.RoundToInt(Random.Range(0f, Points.Count - 1))];
        point.gameObject.SetActive(true);
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

        SoundManager.Instance.Play("sfx.click");
    }

    void ShotPizza() {
        var piz = Instantiate(pizza, pizzaPos, Quaternion.identity);
        Rigidbody2D rb = piz.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 2 + Vector2.right * 4, ForceMode2D.Impulse);

        SoundManager.Instance.Play("sfx.drop");
    }

    public void ShotDummyPizza(int facing, float dist) {
        var piz = Instantiate(pizza, Player.Main.transform.position + new Vector3(0, 1), Quaternion.identity);
        piz.lifetime = -10;
        Rigidbody2D rb = piz.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 3 + Vector2.right * dist, ForceMode2D.Impulse);
    }

    public void DropPizza(Vector2 pos, float facing) {
        var piz = Instantiate(pizza, pos, Quaternion.identity);
        Rigidbody2D rb = piz.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 2 + Vector2.right * 4 * facing, ForceMode2D.Impulse);
    }

    public void SupplyPizza(HomePoint home) {
        StartCoroutine(supplyPizza(home));
    }

    IEnumerator supplyPizza(HomePoint home) {
        home.stopDetect = true;
        CountdownTimer.Instance.timeRemaining += 40;

        var movement = Player.Main.GetComponent<Movement>();
        movement.canMove = false;

        UIManager.Instance.title.text = "추가 시간 40초!";

        Player.Main.transform.position = Player.Main.startPos;
        Player.Main.facingRight = false;

        CamManager.main.Offset(new Vector2(-0.86f, 1.4f), 0.3f);
        CamManager.main.CloseUp(3, 0, 0.3f);

        yield return new WaitForSeconds(0.4f);
        ShotPizza();
        yield return new WaitForSeconds(0.5f);
        ShotPizza();
        yield return new WaitForSeconds(0.5f);
        ShotPizza();
        yield return new WaitForSeconds(0.5f);

        UIManager.Instance.title.text = "";

        Player.Main.facingRight = true;

        CamManager.main.Offset(Vector2.zero, 0.3f);
        CamManager.main.CloseOut(0.3f);

        home.stopDetect = false;
        movement.canMove = true;

        foreach (Itm itm in Itm.items) {
            itm.gameObject.SetActive(true);
        }
    }

    IEnumerator cutScene() {
        yield return null;
        yield return null;

        IsStarted = false;

        pizza1.SetActive(false);
        pizza2.SetActive(false);
        pizza3.SetActive(false);

        SoundManager.Instance.Stop(4);

        foreach (Itm itm in Itm.items) {
            itm.gameObject.SetActive(true);
        }

        CountdownTimer.Instance.timeRemaining = CountdownTimer.Instance.timeDefault;
        CountdownTimer.Instance.timerIsRunning = true;

        Player.Main.pizza = 0;
        Player.Main.success = 0;
        score = 0;

        Player.Main.transform.position = Player.Main.startPos;
        Player.Main.facingRight = false;
        var movement = Player.Main.GetComponent<Movement>();
        movement.canMove = false;

        UIManager.Instance.hud.SetActive(false);
        UIManager.Instance.indicate_mark.SetActive(false);

        yield return new WaitForSeconds(1f);

        RepairPoint();

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

        yield return new WaitForSeconds(1f);

        UIManager.Instance.ShowTuto(1);
        SoundManager.Instance.Play("sfx.tuto");

        while (UIManager.Instance.inTuto) {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Player.Main.facingRight = true;

        yield return new WaitForSeconds(0.5f);

        Player.Main.Comment("준비하시고...");

        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.title.text = "GO! GO!";
        Player.Main.Comment("좋아 가보자고!");

        UIManager.Instance.hud.SetActive(true);
        UIManager.Instance.indicate_mark.SetActive(true);

        IsStarted = true;

        CamManager.main.Offset(Vector2.zero, 0.5f);
        CamManager.main.CloseOut(0.5f);

        SoundManager.Instance.Play("music.funny");

        EnableNextPoint();

        yield return new WaitForSeconds(1.5f);

        UIManager.Instance.title.text = "";

        movement.canMove = true;
       

        yield break;
    }
}
