using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ThrowHandler : MonoBehaviour
{
    public static ThrowHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject playerB;
    public GameObject arrow;
    public GameObject arrowPos;
    public float detachDelay = 0.15f;
    public float resetDelay = 0.5f;
    public Rigidbody2D pivot;
    public float arrowRotation;
    Vector3 worldPosition;
    private bool arrowOrgPosition;

    private Rigidbody2D arrowRigPos;
    private Rigidbody2D arrowRigidbody;
    public Rigidbody2D currentPlayerRigidbody;


    private Camera mainCamera;
    private bool isDragging;
    private bool launched;

    private Controller controller;
    private TargetIndicator targetIndicator;
    void Start()
    {
        controller = Controller.Instance;
        targetIndicator = TargetIndicator.Instance;
        // Debug.Log("Hello already ran");
        playerB.transform.position = new Vector2(pivot.position.x, (pivot.position.y + 1.5f));
        playerB.transform.rotation = Quaternion.identity;
        arrow.transform.position = pivot.position;

        arrowRotation = targetIndicator.angle;
        arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
        currentPlayerRigidbody.isKinematic = true;
        mainCamera = Camera.main;


    }

    void OnEnable()
    {
        launched = true;
        targetIndicator = TargetIndicator.Instance;
        arrowRigPos = arrowPos.GetComponent<Rigidbody2D>();
        currentPlayerRigidbody = playerB.GetComponent<Rigidbody2D>();
        EnhancedTouchSupport.Enable();
        playerB.transform.position = new Vector2(pivot.position.x, (pivot.position.y + 1.5f));
        playerB.transform.rotation = Quaternion.identity;
        arrow.transform.position = pivot.position;
        arrowRotation = targetIndicator.angle;
        currentPlayerRigidbody.isKinematic = true;
        currentPlayerRigidbody.velocity = Vector2.zero;
        isDragging = false;
        arrowRigPos.position = playerB.transform.position;
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        TouchThrow();
#elif UNITY_STANDALONE || UNITY_WEBGL
        MouseThrow();
    #endif
    }

    void TouchThrow()
    {
        arrowRotation = targetIndicator.angle;


        if (currentPlayerRigidbody == null)
        {
            return;
        }

        if (Touch.activeTouches.Count == 0)
        {
            if (isDragging)
            {
                LaunchPlayerNew();
            }

            isDragging = false;

            return;
        }

        if (!launched)
        {
            currentPlayerRigidbody.isKinematic = true;
        }

        isDragging = true;


        Vector2 touchPosition = new Vector2();

        Rect screenBounds = new Rect(0, 0, Screen.width, Screen.height);
        int subtract = 0;

        foreach (Touch touch in Touch.activeTouches)
        {
            if (!screenBounds.Contains(touch.screenPosition))
            {
                subtract++;
                continue;
            }

            touchPosition += touch.screenPosition;

        }

        touchPosition /= (Touch.activeTouches.Count - subtract);

        worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        arrowRigidbody.position = worldPosition;
        SetArrowPosition();
    }

    void MouseThrow()
    {
        arrowRotation = targetIndicator.angle;

        if (currentPlayerRigidbody == null)
        {
            return;
        }

        // Handle mouse button up (release) for launching the player
        if (Input.GetMouseButtonUp(0))  // Left mouse button released
        {
            if (isDragging)
            {
                LaunchPlayerNew();
            }

            isDragging = false;
            return;
        }

        // Set Rigidbody to kinematic while dragging (not launched)
        if (!launched)
        {
            currentPlayerRigidbody.isKinematic = true;
        }

        isDragging = true;

        // Get mouse position
        Vector2 mousePosition = Input.mousePosition;

        // Convert the screen position to world position using the camera
        worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Set the position of the arrow to the world position
        arrowRigidbody.position = worldPosition;
        SetArrowPosition();
    }

    void MaxThrowPower()
    {
        if (targetIndicator.ShowDistance > targetIndicator.HideDistance)
        {
            targetIndicator.ShowDistance = targetIndicator.HideDistance;
        }

    }
    void SetArrowPosition()
    {
        MaxThrowPower();
        
        arrowRigPos.position = new Vector3(playerB.transform.position.x + Mathf.Cos(Mathf.PI * 2 * (arrowRotation / 360)) * targetIndicator.ShowDistance,
            playerB.transform.position.y + (Mathf.Sin(Mathf.PI * 2 * (arrowRotation / 360))) * targetIndicator.ShowDistance);
        arrowPos.transform.rotation = arrow.transform.GetChild(0).transform.rotation;

    }
    void LaunchPlayerNew()
    {
        launched = true;
        MaxThrowPower();
        Debug.Log("Here is the Show Distance: " + targetIndicator.ShowDistance);
        currentPlayerRigidbody.velocity = new Vector2(Mathf.Cos(Mathf.PI * 2 * (arrowRotation/360)) * (targetIndicator.ShowDistance * 3f),
            Mathf.Sin(Mathf.PI * 2 * (arrowRotation/360)) * (targetIndicator.ShowDistance * 3f));
        
        currentPlayerRigidbody.isKinematic = false;
        Invoke(nameof(DetachPlayerNew), detachDelay);
    }

    void DetachPlayerNew()
    {
        controller.canThrow = false;
        SolidPlayer();
        // controller.followPlayer = true;
        controller.player.GetComponent<ThrowHandler>().enabled = false;
    }

    void LaunchPlayer()
    {
        Debug.Log("Is Dragging");
        currentPlayerRigidbody.isKinematic = false;
        // currentPlayerRigidbody = null;

        Invoke(nameof(DetachPlayer), detachDelay);

    }

    void DetachPlayer()
    {
        // currentPlayerSpringJoint = null;
        controller.canThrow = false;
        Invoke(nameof(SolidPlayer), resetDelay);
        controller.player.GetComponent<ThrowHandler>().enabled = false;


    }

    void SolidPlayer()
    {
        controller.player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        controller.player.GetComponent<Collider2D>().enabled = true;
    }

    public void ResetPlayer()
    {
        // currentPlayerSpringJoint.enabled = true;
        playerB.transform.position = pivot.position;
        playerB.transform.rotation = Quaternion.identity;

    }
}
