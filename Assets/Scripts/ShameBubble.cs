using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameBubble : MonoBehaviour
{
    public float speed = 1f;
    public float shameThreshold = 4;
    public GameObject player;
    public GameObject partner;
    public Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.playerDist <= shameThreshold)
        {
            // move toward partner
            transform.position = Vector2.MoveTowards(transform.position, partner.transform.position, speed * Time.deltaTime);
        }
        else
        {
            // move toward player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && controller.playerDist > shameThreshold)
        {
            Destroy(gameObject);
            player.GetComponent<PlayerB>().ShamePlayer();
        }
        if(collision.gameObject == partner && controller.playerDist <= shameThreshold)
        {
            Destroy(gameObject);
            partner.GetComponent<ColorFlash>().FlashColor(Color.white);
        }
    }
}
