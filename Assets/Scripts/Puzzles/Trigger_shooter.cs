using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_shooter : MonoBehaviour
{
    public GameObject shooter;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cubeA" || collision.gameObject.tag == "cubeB")
        {
            Debug.Log("Here is the triggeer");
            //animator.SetTrigger("Press");
            animator.SetBool("Press3", true);
            shooter.GetComponent<SpikeShooter>().shutdown = true;
 
        }

    }
}
