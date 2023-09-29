using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
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
    public bool followPlayer = true;

    // Swipe Touch Controls
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;

    // Managing Game Objects
    [SerializeField] public GameObject activePlayer;
    [SerializeField] public GameObject nonActivePlayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Instances
    private ThrowHandler throwHandler;

    // Camera
    [SerializeField] private GameObject virtualCamera;
    private new CinemachineVirtualCamera camera;

    // Current Player
    public Rigidbody2D player;
    private Rigidbody2D nonPlayer;
    public bool canThrow;


    // Splitting The Screen 3 ways
    private int splittedScreen = Screen.width / 3;

    // distance between player and partner, used for following and shame spawning
    public float playerDist = 4f;
    public float followBounds = 3f;

    // Materials
    public PhysicsMaterial2D NoFriction;
    public PhysicsMaterial2D Bouncey;

    // Camera
    private float cameraDelay = 1f;
    private bool cameraFollow;

    
    void Start()
    {
        // Instance Sets
        throwHandler = ThrowHandler.Instance;

        // Setup Touch, Player, Camera
        dragDistance = Screen.height * 30 / 100;
        activePlayer.GetComponent<PlayerB>().controller = this;
        player = activePlayer.GetComponent<Rigidbody2D>();
        nonPlayer = nonActivePlayer.GetComponent<Rigidbody2D>();
        camera = virtualCamera.GetComponent<CinemachineVirtualCamera>();
        Invoke(nameof(UpdateCamera), cameraDelay);
    }

    void Update()
    {
        PlayerTwoBounciness();
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
                //Debug.Log("HELD L");
                horizontal = -1f;
                player.velocity = new Vector2(horizontal * speed, player.velocity.y);
            }
            else if (touch.position.x >= 2 * splittedScreen && touch.phase == TouchPhase.Stationary)
            {
               // Debug.Log("HELD R");
                horizontal = 1f;
                player.velocity = new Vector2(horizontal * speed, player.velocity.y);
            }

            else if (touch.position.x >= splittedScreen && touch.position.x < 2 * splittedScreen)
            {
                //Debug.Log("Middle");
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
                        

                            // add hold touch for higher jumps?
   
                            Debug.Log("Swipe UP");                      
                        }
                        else
                        {
                            
                            Debug.Log("Swipe Down");
                            
                            // Distance
                            if (followPlayer && IsGrounded())
                            {
                                canThrow = true;
                                followPlayer = false;
                                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                                player.GetComponent<Collider2D>().enabled = false;
                                player.GetComponent<ThrowHandler>().playerB = nonActivePlayer;
                                player.GetComponent<ThrowHandler>().enabled = true;
                              
                            }
                        }
                    }
                }
                else
                {
                    followPlayer = !followPlayer;
                }
            }
        }
    }

    void UpdateCamera()
    {
        camera.Follow = activePlayer.transform;
    }

    void FollowPlayer()
    {
        if (followPlayer)
        {
            // distance was calculated in FixedUpdate 
            // if distance greater than a certain boundary, follow behind
            if (playerDist > followBounds)
            {
                float xPos = Vector2.MoveTowards(nonPlayer.transform.position, player.transform.position, playerDist * Time.deltaTime).x;
                nonPlayer.transform.position = new Vector2(xPos, nonPlayer.transform.position.y);
            }
        }   
    }

    void PlayerTwoBounciness()
    {
        if (followPlayer)
        {
            nonActivePlayer.GetComponent<BoxCollider2D>().sharedMaterial = NoFriction;
        }
        else
        {
            nonPlayer.GetComponent<BoxCollider2D>().sharedMaterial = Bouncey;
        }
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}