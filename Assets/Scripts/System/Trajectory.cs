using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotsNumber;
    [SerializeField] private GameObject dotsParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float dotSpacing;
    [SerializeField][Range(0.01f, 0.3f)] private float dotMinScale;
    [SerializeField][Range(0.3f, 1f)] private float dotMaxScale;

    public Transform[] DotsList { get; private set; }

    public float DotSpacing
    {
        get => dotSpacing;
        set => dotSpacing = value;
    }

    Vector2 pos;
    float timeStamp;

    void Start()
    {
        Hide();
        PrepareDots();
    }

    void PrepareDots()
    {
        DotsList = new Transform[dotsNumber];

        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;
        for (int i = 0; i < dotsNumber; i++)
        {
            Transform dot = Instantiate(dotPrefab, null).transform;
            dot.parent = dotsParent.transform;
            dot.localScale = Vector3.one * scale;
            DotsList[i] = dot;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = ballPos.x + forceApplied.x * timeStamp;
            pos.y = ballPos.y + forceApplied.y * timeStamp - Physics2D.gravity.magnitude * timeStamp * timeStamp / 2f;

            DotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }

    //포물선 색상 변경 함수
    public void SetDotsColor(Color color)
    {
        foreach (Transform dot in DotsList)
        {
            SpriteRenderer dotRenderer = dot.GetComponent<SpriteRenderer>();
            if (dotRenderer != null)
            {
                dotRenderer.color = color;
            }
        }
    }
}
