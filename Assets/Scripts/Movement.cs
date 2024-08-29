using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D col;
    public Player pl;
    public bool isRunning, isJumping, isMoving, canMove;
    public float jumpTime;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Start()
    {
        pl = Player.Main;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        pl = GetComponent<Player>();

        defSpeed = speed;
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        isJumping = true;
        pl.animator.SetTrigger("jump");
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        // rb.velocity = Vector3.zero;
        // rb.angularVelocity = 0f;
        // rb.isKinematic = true;
    }

    public float speed = 5f;
    [HideInInspector] public float defSpeed;

    void FixedUpdate()
    {
        isMoving = false;

        if (isJumping) {
            jumpTime += Time.fixedDeltaTime;
        } else {
            jumpTime = 0;
        }

        if (canMove)
        {
            //������ȯ
            float moveDirection;
            if (pl.isFlipped)
                moveDirection = -1;
            else
                moveDirection = 1;

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveDirection * speed * Time.deltaTime, 0, 0);
                isMoving = true;
                pl.facingRight = false;

                if (isJumping && rb.velocity.y >= 3) {
                    UIManager.Instance.SetActionText("점프 + 왼쪽 가속!", Color.green, false);
                }

                if (isJumping && jumpTime <= 0.2) {
                    UIManager.Instance.SetActionText("점프 + 왼쪽 초가속!", Color.yellow);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0);
                isMoving = true;
                pl.facingRight = true;

                if (isJumping && rb.velocity.y >= 3) {
                    UIManager.Instance.SetActionText("점프 + 오른쪽 가속!", Color.green, false);
                }

                if (isJumping && jumpTime <= 0.2) {
                    UIManager.Instance.SetActionText("점프 + 오른쪽 초가속!", Color.yellow);
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
                isMoving = true;

                if (isJumping && rb.velocity.y >= 3) {
                    UIManager.Instance.SetActionText("점프 + 위쪽 가속!", Color.green, false);
                }

                if (isJumping && jumpTime <= 0.3) {
                    UIManager.Instance.SetActionText("점프 + 위쪽 초가속!", Color.yellow);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -speed * Time.deltaTime);
                isMoving = true;
            }
        }

        if (pl.OnGround() && rb.velocity.y <= 0)
        {
            rb.velocity = Vector2.zero;
            isJumping = false;
        }

        pl.animator.SetBool("isMoving", isMoving);
        pl.animator.SetBool("isJumping", isJumping);
        pl.animator.SetBool("isRunning", isRunning);
    }
}
