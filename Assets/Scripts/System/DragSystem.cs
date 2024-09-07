using UnityEngine;

public class DragSystem : MonoBehaviour
{
    #region Singleton class: GameManager

    public static DragSystem Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    Camera cam;

    public Movement ball;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;
    public float maxDistance;
    public float defMaxDist;

    bool isDragging = false;
    bool bySpace;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DesactivateRb();

        defMaxDist = maxDistance;
    }

    void Update()
    {
        if (ball.pl.OnGround() && ball.canMove) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                isDragging = true;
                bySpace = true;
                OnDragStart();
            }
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                bySpace = false;
                OnDragStart();
            }
        }

        if (isDragging)
        {
            OnDrag();

            if (bySpace)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isDragging = false;
                    OnDragEnd();
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                    OnDragEnd();
                }
            }
        }
    }

    //-Drag--------------------------------------
    void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
        ball.canMove = false;
        ball.pl.animator.SetBool("isReady", true);
    }

    void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);

        if (endPoint.x < startPoint.x)
        {
            ball.pl.facingRight = true;
        }
        else
        {
            ball.pl.facingRight = false;
        }

        if (distance > maxDistance)
        {
            distance = maxDistance;
        }

        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        CamManager.main.CloseUp(7 - distance / maxDistance * 1.5f, 0, 0.2f);

        //just for debug
        Debug.DrawLine(startPoint, endPoint);

        trajectory.UpdateDots(ball.pos + new Vector3(0, -1), force);

        UIManager.Instance.SetActionText("", Color.gray);
    }

    void OnDragEnd()
    {
        ball.ActivateRb();
        ball.pl.animator.SetBool("isReady", false);
        trajectory.Hide();

        CamManager.main.CloseOut(0.2f);

        ball.canMove = true;

        Debug.Log(distance);

        if (distance / maxDistance > 0.2f) {
            ball.Push(force);

            UIManager.Instance.SetActionText("점프", Color.white);

            SoundManager.Instance.Play("sfx.jump");

            var eff = Instantiate(ball.pl.jumpEff, ball.transform.position + new Vector3(0, 0.5f), Quaternion.identity);
            eff.AddComponent<SetLayer>().sortingLayer = "particle";
            Destroy(eff.gameObject, 1);
        }
    }
}
