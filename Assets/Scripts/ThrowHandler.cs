using System.Collections;
using System.Collections.Generic;
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
    public float detachDelay = 0.15f;
    public float resetDelay = 0.5f;
    public Rigidbody2D pivot;
    public float arrowRotation;

    private Rigidbody2D arrowRigidbody;
    private Rigidbody2D currentPlayerRigidbody;
    private SpringJoint2D currentPlayerSpringJoint;

    private Camera mainCamera;
    private bool isDragging;

    private Controller controller;
    private TargetIndicator targetIndicator;
    void Start()
    {
        controller = Controller.Instance;
        targetIndicator = TargetIndicator.Instance;
        Debug.Log("Hello already ran");
        playerB.transform.position = new Vector2(pivot.position.x, (pivot.position.y + 1.5f));
        playerB.transform.rotation = Quaternion.identity;
        arrow.transform.position = pivot.position;

        arrowRotation = targetIndicator.angle;
        arrowRigidbody = arrow.GetComponent<Rigidbody2D>();
        currentPlayerRigidbody = playerB.GetComponent<Rigidbody2D>();
        currentPlayerSpringJoint = playerB.GetComponent<SpringJoint2D>();

        currentPlayerRigidbody.isKinematic = true;

        currentPlayerSpringJoint.connectedBody = pivot;
        mainCamera = Camera.main;

        
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        playerB.transform.position = new Vector2(pivot.position.x, (pivot.position.y + 1.5f));
        playerB.transform.rotation = Quaternion.identity;
        arrow.transform.position = pivot.position;
        arrowRotation = targetIndicator.angle;
        currentPlayerRigidbody.isKinematic = true;
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
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

        isDragging = true;
        currentPlayerRigidbody.isKinematic = true;

        Vector2 touchPosition = new Vector2();

        Rect screenBounds = new Rect(0, 0, Screen.width, Screen.height);
        int subtract = 0;

        foreach (Touch touch in Touch.activeTouches)
        {
            Debug.Log("Hello");
            if (!screenBounds.Contains(touch.screenPosition))
            {
                subtract++;
                continue;
            }

            touchPosition += touch.screenPosition;

        }

        touchPosition /= (Touch.activeTouches.Count - subtract);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        arrowRigidbody.position = worldPosition;
        Debug.Log("Here is the World P " + worldPosition);



    }

    void LaunchPlayerNew()
    {
        currentPlayerRigidbody.velocity = new Vector2(Mathf.Cos(Mathf.PI * 2 * (arrowRotation/360)) * (targetIndicator.ShowDistance * 2.5f),
            Mathf.Sin(Mathf.PI * 2 * (arrowRotation/360)) * (targetIndicator.ShowDistance * 2.5f));
        currentPlayerRigidbody.isKinematic = false;
        Invoke(nameof(DetachPlayerNew), detachDelay);
    }

    void DetachPlayerNew()
    {
        SolidPlayer();
        controller.canThrow = false;
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
        currentPlayerSpringJoint.enabled = false;
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
        currentPlayerSpringJoint.connectedBody = pivot;

    }
}
