using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using TMPro;
using Touch = UnityEngine.Touch;
using TouchPhase = UnityEngine.TouchPhase;

public class Controller : MonoBehaviour
{
    public static Controller Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Player Properites
    private float horizontal;
    private float speed = 5f;
    private float jump = 10f;
    private bool isFacingRight = true;
    private bool followPlayer = true;

    // Swipe Touch Controls
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;

    // Managing Game Objects
    [SerializeField] public GameObject activePlayer;
    [SerializeField] public GameObject nonActivePlayer;
    [SerializeField] private GameObject controlUI;
    [SerializeField] private GameObject pivot;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TMP_Text textFollow;

    // Throwing
    private ThrowHandler throwHandler;

    // Camera
    [SerializeField] private GameObject virtualCamera;
    private new CinemachineVirtualCamera camera;

    // Current Player
    public Rigidbody2D player;
    private Rigidbody2D nonPlayer;
    public bool canThrow;

    // Follow?
    private GameObject follow;
    private Button followButton;

    // Splitting The Screen 3 ways
    private int splittedScreen = Screen.width / 3;



    // distance between player and partner, used for following and shame spawning
    public float playerDist = 4f;
    public float followBounds = 3f;


    void Start()
    {
        Application.targetFrameRate = 60;

        // Throwing
        throwHandler = ThrowHandler.Instance;
        // Setup follow
        follow = controlUI.transform.Find("Follow").gameObject;
        followButton = follow.GetComponent<Button>();
        // Setup Touch, Player, Camera
        dragDistance = Screen.height * 30 / 100;
        player = activePlayer.GetComponent<Rigidbody2D>();
        nonPlayer = nonActivePlayer.GetComponent<Rigidbody2D>();
        camera = virtualCamera.GetComponent<CinemachineVirtualCamera>();
        UpdateCamera();

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
        UpdateCamera();
        if (!canThrow)
        {
            SwipeTouch();
            ScreenTouch();
            FlipDirection();
            FollowPlayer();
        }
    }

    void FixedUpdate()
    {
        // find distance between partner and player. Other systems rely on this, so always needs to happen
        playerDist = Vector2.Distance(activePlayer.transform.position, nonPlayer.transform.position);

        if (!canThrow)
        {
            ToggleFollow();
            ClickActions();
        }
    }


    void ClickActions()
    {
        MoveTouch();

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void MoveTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < splittedScreen && touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("HELD L");
                horizontal = -1f;
                player.velocity = new Vector2(horizontal * speed, player.velocity.y);
            }
            else if (touch.position.x >= 2 * splittedScreen && touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("HELD R");
                horizontal = 1f;
                player.velocity = new Vector2(horizontal * speed, player.velocity.y);
            }

            else if (touch.position.x >= splittedScreen && touch.position.x < 2 * splittedScreen)
            {
                Debug.Log("Middle");
                horizontal = 0f;
                player.velocity = new Vector2(horizontal, player.velocity.y);
            }

        }

    }

    void ScreenTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.tapCount > 0)
            {
                // Debug.Log("touching the screen");
            }

            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                // Debug.Log("NOT touching screen");
                horizontal = 0f;
                player.velocity = new Vector2(horizontal, player.velocity.y);
                // nonPlayer.velocity = new Vector2(horizontal * speed, nonPlayer.velocity.y);
            }
        }
    }


    void FlipDirection()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }

    void SwipeTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch  = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if ((lp.x > fp.x))
                        {
                            Debug.Log("Swipe Right");
                        }
                        else
                        {
                            Debug.Log("Swipe Left");
                        }
                    }
                    else
                    {
                        if (lp.y > fp.y && IsGrounded())
                        {
                            player.velocity = new Vector2(player.velocity.x, jump);
                            if (followPlayer)
                            {
                                nonPlayer.velocity = new Vector2(nonPlayer.velocity.x, jump);
                            }

                            // add hold touch for higher jumps?
                            // player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
                            Debug.Log("Swipe UP");                      
                        }
                        else
                        {
                            
                            Debug.Log("Swipe Down");
                            
                            if (followPlayer)
                            {
                                if (nonPlayer.GetComponent<SpringJoint2D>().enabled == false)
                                {
                                    throwHandler.ResetPlayer();
                                }
                                canThrow = true;
                                followPlayer = false;
                                ToggleFollow();
                                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                                player.GetComponent<Collider2D>().enabled = false;
                                player.GetComponent<ThrowHandler>().playerB = nonActivePlayer;
                                player.GetComponent<ThrowHandler>().enabled = true;

                            }
                            
                  
                            //SwitchPlayer();
                        }
                    }
                }
                else
                {
                    Debug.Log("Tap");
                }
            }
        }
    }

    void UpdateCamera()
    {
        camera.Follow = activePlayer.transform;
    }

    void SwitchPlayer()
    {
        GameObject copyPlayer = activePlayer;
        activePlayer = nonActivePlayer;
        nonActivePlayer = copyPlayer;
        player = activePlayer.GetComponent<Rigidbody2D>();
        nonPlayer = nonActivePlayer.GetComponent<Rigidbody2D>();
        horizontal = 0f;
        nonPlayer.velocity = new Vector2(horizontal, player.velocity.y);

    }

    void FollowPlayer()
    {
        if (followPlayer)
        {
            // distance was calculated in FixedUpdate 
            // if distance greater than a certain boundary, follow behind
            if (playerDist > followBounds)
            {
                nonPlayer.transform.position = Vector2.MoveTowards(nonPlayer.transform.position, player.transform.position, playerDist * Time.deltaTime);
            }

        }
        
    }

    void ToggleFollow()
    {
        followButton.onClick.AddListener(()=>followPlayer = !followPlayer);
        if (followPlayer)
        {
            textFollow.text = "Following";
        }
        else
        {
            textFollow.text = "Not Following";
        }
    }


}

// OLD CODE


//private GameObject moveLeft;
//private GameObject moveRight;

//moveLeft = controlUI.transform.Find("Left").gameObject;
//moveRight = controlUI.transform.Find("Right").gameObject;


//void moveleft()
//{
//    if (moveleft.getcomponent<buttoncheck>().buttonpressed)
//    {
//        horizontal = -1f;
//        player.velocity = new vector2(horizontal * speed, player.velocity.y);

//    }
//    else
//    {
//        horizontal = 0f;
//        // this can only be called once since it doesn't need to check both buttons
//        player.velocity = new vector2(horizontal, player.velocity.y);
//    }


//}

//void moveright()
//{
//    if (moveright.getcomponent<buttoncheck>().buttonpressed)
//    {
//        horizontal = 1f;
//        player.velocity = new vector2(horizontal * speed, player.velocity.y);
//    }
//    else
//    {
//        horizontal = 0f;
//    }

//}
