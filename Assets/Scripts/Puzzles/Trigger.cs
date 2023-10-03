using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    BoxCollider2D button;
    public Animator animator;
    public bool trigger;
    public bool onetime;
    public bool open;
    public bool isA;
    public bool isSolid = true;

    void Start()
    {
        button = GetComponents<BoxCollider2D>()[1];
        if (!isSolid)
        {
            button.enabled = false;
        }
        open = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA" || collision.gameObject.tag == "cubeB")
        {
            if (collision.gameObject.tag == "cubeB")
            {
                isA = true;
            }

            if (!animator.GetBool("Press3"))
            {
                animator.SetBool("Press3", true);
                animator.SetBool("Press4", false);
                trigger = true;
            }
           
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "cubeA" || other.gameObject.tag == "cubeB")
        {
            if (!onetime && !open)
            {
    
                if (isA)
                {
                    if (other.gameObject.tag == "cubeA")
                    {
                        animator.SetBool("Press3", true);
                        animator.SetBool("Press4", false);
                    }
                    else
                    {
                        {
                            animator.SetBool("Press3", false);
                            animator.SetBool("Press4", true);
                            trigger = false;
                            isA = false;
                        }
                    }
                }
                else
                {
                    animator.SetBool("Press3", false);
                    animator.SetBool("Press4", true);
                    trigger = false;
                    isA = false;
                }
            }
        }
    }

}
