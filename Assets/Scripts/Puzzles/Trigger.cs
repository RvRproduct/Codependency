using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    //public GameObject door;
    public Animator animator;
    //public Animator animator2;
    public bool trigger;
    public bool onetime;
    public bool open;
    public bool isA;
    void Start()
    {
        open = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA" || collision.gameObject.tag == "cubeB")
        {
         
            //Debug.Log("Here is the triggeer");
            //animator2.SetTrigger("Door");
            if (collision.gameObject.tag == "cubeB")
            {
                isA = true;
            }
            //animator.ResetTrigger("Press2");
            //animator.SetTrigger("Press");
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
