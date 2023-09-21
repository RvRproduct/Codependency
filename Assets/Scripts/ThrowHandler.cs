using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ThrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject playerB;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float detachDelay;

    private Rigidbody2D currentPlayerRigidbody;
    private SpringJoint2D currentPlayerSpringJoint;

    private Camera mainCamera;
    private bool isDragging;

    private GameObject playerInstance;

    void Start()
    {
        playerInstance = Instantiate(playerB, pivot.position, Quaternion.identity);

        currentPlayerRigidbody = playerInstance.GetComponent<Rigidbody2D>();
        currentPlayerSpringJoint = playerInstance.GetComponent<SpringJoint2D>();

        currentPlayerSpringJoint.connectedBody = pivot;
        mainCamera = Camera.main;
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
        if (currentPlayerRigidbody == null)
        {
            return;
        }

        if (Touch.activeTouches.Count == 0)
        {
            if (isDragging)
            {
                LaunchPlayer();
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
            if (!screenBounds.Contains(touch.screenPosition))
            {
                subtract++;
                continue;
            }

            touchPosition += touch.screenPosition;

        }

        touchPosition /= (Touch.activeTouches.Count - subtract);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentPlayerRigidbody.position = worldPosition;

        Debug.Log("Here is the World P " + worldPosition);



    }

    void LaunchPlayer()
    {
        currentPlayerRigidbody.isKinematic = false;
        currentPlayerRigidbody = null;

        Invoke(nameof(DetachPlayer), detachDelay);

    }

    void DetachPlayer()
    {
        currentPlayerSpringJoint.enabled = false;
        currentPlayerSpringJoint = null;

    }
}
