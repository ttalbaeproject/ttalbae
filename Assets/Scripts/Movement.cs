


using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D col;
    public Player pl;
    public bool isRunning, isJumping, isMoving, canMove;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    void Start()
    {
        pl = Player.Main;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        pl = GetComponent<Player>();
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
    void FixedUpdate()
    {
        isMoving = false;
        
        if (canMove) {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                isMoving = true;

                pl.facingRight = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                isMoving = true;

                pl.facingRight = true;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -speed * Time.deltaTime);
                isMoving = true;
            }
        }

        if (pl.OnGround() && rb.velocity.y <= 0) {
            rb.velocity = Vector2.zero;
            isJumping = false;
        }

        pl.animator.SetBool("isMoving", isMoving);
        pl.animator.SetBool("isJumping", isJumping);
        pl.animator.SetBool("isRunning", isRunning);
    }
}
