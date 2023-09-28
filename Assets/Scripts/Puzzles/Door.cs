using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int trigger_number;
    public GameObject trigger1;
    public GameObject trigger2;
    private Trigger t1;
    private Trigger t2;
    public Animator animator;
    //public bool tt1;
    // Start is called before the first frame update
    void Start()
    {
        if (trigger_number == 1)
        {
            trigger2 = null;
            t1 = trigger1.GetComponent<Trigger>();
        }
        else
        {
            t1 = trigger1.GetComponent<Trigger>();
            t2 = trigger2.GetComponent<Trigger>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger_number == 1)
        {
            //tt1 = t1.trigger;
            if (t1.trigger)
            {
                Debug.Log("1111");
                animator.SetTrigger("Door");
                t1.open = true;
            }
        }
        else
        {
            if (t1.trigger && t2.trigger)
            {
                animator.SetTrigger("Door");
                t1.open = true;
                t2.open = true;
            }
        }
    }
}
