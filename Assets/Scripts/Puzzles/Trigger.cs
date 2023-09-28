using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject door;
    public Animator animator;
    //public Animator animator2;
    public bool trigger;
    public bool onetime;
    public bool open;
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA" || collision.gameObject.tag == "cubeB")
        {
            //Debug.Log("Here is the triggeer");
            //animator2.SetTrigger("Door");
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
                //Debug.Log("Here is the triggeer");
                //animator2.SetTrigger("Door");
                animator.ResetTrigger("Press");
                animator.SetTrigger("Press2");
                //animator.ResetTrigger("Press");
                trigger = false;
            }
        }
    }

}
