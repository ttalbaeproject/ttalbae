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
    [SerializeField] float maxDistance;

    bool isDragging = false;
    bool bySpace;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    public JumpItem SuperJump;
    public JumpItem LowJump;

    //---------------------------------------
    void Start()
    {
        cam = Camera.main;
        ball.DesactivateRb();
    }

    void Update()
    {
        if (ball.rb.velocity.y <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
        if (SuperJump.is_jump && SuperJump.is_effect)
        {
            SuperJump.Reset(); //효과 없애는 신호
        }
        if (LowJump.is_jump && LowJump.is_effect)
        {
            LowJump.Reset(); //효과 없애는 신호
        }
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

        //just for debug
        Debug.DrawLine(startPoint, endPoint);

        trajectory.UpdateDots(ball.pos + new Vector3(0, -1), force);
    }

    void OnDragEnd()
    {
        //push the ball
        ball.ActivateRb();

        ball.Push(force);

        ball.pl.animator.SetBool("isReady", false);

        ball.canMove = true;

        trajectory.Hide();
        if (SuperJump.is_effect) {
            SuperJump.is_jump = true; //점프한걸로 바꾸기
        }
        if (LowJump.is_effect)
        {
            LowJump.is_jump = true; //점프한걸로 바꾸기
        }
    }
}
