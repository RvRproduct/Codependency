using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    private float jump = 10f;
    private bool isFacingRight = true;

    [SerializeField] private GameObject activePlayer;
    [SerializeField] private GameObject nonActivePlayer;
    [SerializeField] private GameObject controlUI;

    private Camera mainCamera;
    private GameObject moveLeft;
    private GameObject moveRight;
    private Rigidbody2D player;


    void Start()
    {
        mainCamera = Camera.main;
        moveLeft = controlUI.transform.Find("Left").gameObject;
        moveRight = controlUI.transform.Find("Right").gameObject;
        player = activePlayer.GetComponent<Rigidbody2D>();

    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {

        ClickActions();
        
    }


    void ClickActions()
    {
        Move();
    }

    void Move()
    {
        MoveLeft();
        MoveRight();
    }
    void MoveLeft()
    {
        if (moveLeft.GetComponent<ButtonCheck>().buttonPressed)
        {
            horizontal = -1f;
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);

        }
        else
        {
            horizontal = 0f;
        }

        

    }

    void MoveRight()
    {
        if (moveRight.GetComponent<ButtonCheck>().buttonPressed)
        {
            horizontal = 1f;
            player.velocity = new Vector2(horizontal * speed, player.velocity.y);
        }
        else
        {
            horizontal = 0f;
        }
        
    }
}
