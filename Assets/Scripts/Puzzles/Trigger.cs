using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public Animator animator;
    public Animator animator2;
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
            animator2.SetTrigger("Door");
            animator.SetTrigger("Press");
        }

    }
}
