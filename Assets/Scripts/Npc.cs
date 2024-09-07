using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    Animator animator;
    float lifetime;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("pickup");
        animator.SetBool("holding", true);

        SoundManager.Instance.Play("sfx.npc");
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime > 2) {
            Destroy(gameObject);
        }
    }
}
