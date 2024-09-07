
using UnityEngine;
using UnityEngine.UI;

public class Loading_spin : MonoBehaviour
{
    public Sprite good, bad;
    public float time;
    bool isBad;
    Image img;
    void Start() {
        img = GetComponent<Image>();

        if (Random.Range(0, 100) > 50) {
            img.sprite = good;
        } else {
            img.sprite = bad;
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 200f * Time.deltaTime));
    }
}
