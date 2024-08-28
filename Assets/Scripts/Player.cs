using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Main { get; private set; }
    public Animator animator;
    public SpriteRenderer render;
    public bool facingRight = true;
    Vector2 scaleDefault;

    public float jumpForce = 10f; // 점프력
    public bool isFlipped = false;

    void Awake()
    {
        Main = this;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        scaleDefault = transform.localScale;
    }
    public void Jump(Vector2 force)
    {
        // 점프력 적용
        GetComponent<Rigidbody2D>().AddForce(force * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    void Update()
    {
        //방향전환
        if (facingRight)
        {
            if (isFlipped)
            {
                transform.localScale = new Vector3(-scaleDefault.x, scaleDefault.y);
            }
            else
            {
                transform.localScale = new Vector3(scaleDefault.x, scaleDefault.y);
            }
        }
        else
        {
            if (isFlipped)
            {
                transform.localScale = new Vector3(scaleDefault.x, scaleDefault.y);
            }
            else
            {
                transform.localScale = new Vector3(-scaleDefault.x, scaleDefault.y);
            }
        }

    }

    bool CheckGround(RaycastHit2D[] casts)
    {
        for (int i = 0; i < casts.Length; i++)
        {
            RaycastHit2D cast = casts[i];

            if ((LayerMask.GetMask("object") & (1 << cast.transform.gameObject.layer)) != 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool OnGround()
    {
        RaycastHit2D[] center = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.8f), Vector2.down, 0.2f);
        RaycastHit2D[] left = Physics2D.RaycastAll(transform.position + new Vector3(-0.3f, -0.8f), Vector2.down, 0.2f);
        RaycastHit2D[] right = Physics2D.RaycastAll(transform.position + new Vector3(0.3f, -0.8f), Vector2.down, 0.2f);

        bool l = CheckGround(left), c = CheckGround(center), r = CheckGround(right);

        if (l && c && r)
        {
            return true;
        }

        if (l && c)
        {
            return true;
        }

        if (c && r)
        {
            return true;
        }

        if (!l && c && !r)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + new Vector3(0, -0.8f), transform.position + new Vector3(0, -1f));

        Gizmos.DrawLine(transform.position + new Vector3(0.3f, -0.8f), transform.position + new Vector3(0.3f, -1f));
        Gizmos.DrawLine(transform.position + new Vector3(-0.3f, -0.8f), transform.position + new Vector3(-0.3f, -1f));
    }
}