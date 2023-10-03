using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Animator animator;
    public bool trigger;
    public bool onetime;
    public bool open;

    void Start()
    {
        open = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA" || collision.gameObject.tag == "cubeB")
        {
            animator.ResetTrigger("Press2");
            animator.SetTrigger("Press");
            trigger = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "cubeA" || other.gameObject.tag == "cubeB")
        {
            if (!onetime && !open)
            {
                animator.ResetTrigger("Press");
                animator.SetTrigger("Press2");
                trigger = false;
            }
        }
    }

}
