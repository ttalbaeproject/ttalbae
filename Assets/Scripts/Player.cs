using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Main { get; private set; }
    public Animator animator;
    public SpriteRenderer render;
    public bool facingRight = true;
    Vector2 scaleDefault;

    public float jumpForce = 10f; // ������
    public bool isFlipped = false;
    public List<Effect> effects = new();
    List<Effect> removeEffs = new();

    public Color spriteCol;

    public int pizza;
    public int success;
    float commentTime, effTime;
    public Text commentText;
    public Image comment;
    public SpriteRenderer effIndi;
    public Sprite good, bad;
    public Vector2 startPos;
    public float notMove;
    public GameObject jumpEff;

    void Awake()
    {
        Main = this;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        scaleDefault = transform.localScale;
        spriteCol = render.color;

        startPos = transform.position;
    }
    public void Comment(string text) {
        commentText.text = text;
        comment.gameObject.SetActive(true);
        commentTime = 2.5f;
    }
    public void EffectIndicate(Sprite img) {
        effIndi.sprite = img;
        effIndi.gameObject.SetActive(true);
        effTime = 2f;

        effIndi.transform.localPosition = new Vector3(0, 0.6f);
        LeanTween.moveLocalY(effIndi.gameObject, 0.8f, 2.5f);
    }
    public void Jump(Vector2 force)
    {
        // ������ ����
        GetComponent<Rigidbody2D>().AddForce(force * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    public void AddEffect(Effect eff) {
        for (int i = 0; i < effects.Count; i++) {
            if (effects[i].Id == eff.Id) {
                effects[i].duration += eff.time;

                eff.duration += effects[i].duration;

                effects[i].ended = true;
                Destroy(effects[i].icon.gameObject);
            }
        }

        effects.Add(eff);
        eff.OnStart();
    }

    void Update()
    {
        if (comment.gameObject.activeSelf) {
            commentTime -= Time.deltaTime;

            Color col = comment.color;
            col.a = commentTime;

            comment.color = col;

            if (commentTime <= 0) {
                comment.gameObject.SetActive(false);
            }

            if (facingRight) {
                commentText.transform.localScale = new Vector3(Mathf.Abs(commentText.transform.localScale.x), commentText.transform.localScale.y, commentText.transform.localScale.z);
            } else {
                commentText.transform.localScale = new Vector3(-Mathf.Abs(commentText.transform.localScale.x), commentText.transform.localScale.y, commentText.transform.localScale.z);
            }
        }

        if (effIndi.gameObject.activeSelf) {
            effTime -= Time.deltaTime;

            Color col = effIndi.color;
            col.a = effTime;

            if (col.a > 0.5f) {
                col.a = 0.5f;
            }

            effIndi.color = col;

            if (effTime <= 0) {
                effIndi.gameObject.SetActive(false);
            }
        }

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

        foreach (Effect eff in effects) {
            eff.OnUpdate();
            
            if (eff.ended) {
                removeEffs.Add(eff);
            }
        }

        foreach (Effect eff in removeEffs) {
            effects.Remove(eff);
        }

        removeEffs.Clear();
    }

    public bool HasEffect(string id) {
        foreach (Effect eff in effects) {
            if (eff.Id == id) {
                return true;
            }
        }

        return false;
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
        RaycastHit2D[] center = Physics2D.RaycastAll(transform.position + new Vector3(0, -0.6f), Vector2.down, 0.2f);
        RaycastHit2D[] left = Physics2D.RaycastAll(transform.position + new Vector3(-0.3f, -0.6f), Vector2.down, 0.2f);
        RaycastHit2D[] right = Physics2D.RaycastAll(transform.position + new Vector3(0.3f, -0.6f), Vector2.down, 0.2f);

        bool l = CheckGround(left), c = CheckGround(center), r = CheckGround(right);

        if (l || c || r) {
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((LayerMask.GetMask("object") & (1 << collision.gameObject.layer)) != 0)
        {
            if (collision.relativeVelocity.y <= -5) {
                if (pizza > 0) {
                    GameManager.Instance.DropPizza(transform.position, facingRight ? 1 : -1);
                    pizza--;

                    SoundManager.Instance.Play("sfx.dropPizza");

                    if (pizza <= 0) {
                        Comment("잠깐.. 다 떨어졌어!!!!");
                    } else {
                        int rd = Random.Range(0, 100);

                        if (rd <= 30) {
                            Comment("으앗!");
                        } else if (rd <= 60) {
                            Comment("잠깐만!");
                        } else if (rd <= 90) {
                            Comment("제발 제발!");
                        } else {
                            Comment("걍 때려칠까");
                        }
                    }
                }
            }
        }
    }
}
