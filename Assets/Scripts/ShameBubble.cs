using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameBubble : MonoBehaviour
{
    public float speed = 1f;
    public GameObject player;
    public GameObject partner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move toward player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player || collision.gameObject == partner)
        {
            Destroy(gameObject);
        }
    }
}
