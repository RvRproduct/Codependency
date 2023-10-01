using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToPlayerCol : MonoBehaviour
{
    Controller controller;

    void Start()
    {
        controller = Controller.Instance;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cubeB")
        {
            controller.nonActivePlayer.GetComponent<BoxCollider2D>().isTrigger = true;
            controller.nonActivePlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            controller.nonActivePlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cubeB")
        {
            Debug.Log("Hello there this is else");
            controller.nonActivePlayer.GetComponent<BoxCollider2D>().isTrigger = false;
            controller.nonActivePlayer.GetComponent<Rigidbody2D>().isKinematic = false;
            controller.nonActivePlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        }


    }

}
