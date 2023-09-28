using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform Target;
    public float HideDistance;
    public float ShowDistance;
    public float angle;
    public GameObject arrowFull;
    public GameObject arrowPoint;

    public static TargetIndicator Instance;
    private SpriteRenderer arrowPartOne;
    private SpriteRenderer arrowPartTwo;

    private Controller controller;
    private ThrowHandler throwHandler;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        controller = Controller.Instance;
        throwHandler = ThrowHandler.Instance;
        arrowPartOne = arrowFull.GetComponent<SpriteRenderer>();
        arrowPartTwo = arrowPoint.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (controller.player.GetComponent<ThrowHandler>().enabled)
        {
            arrowPartOne.enabled = true;
            arrowPartTwo.enabled = true;
        }
        else
        {
            arrowPartOne.enabled = false;
            arrowPartTwo.enabled = false;
        }

        var dir = Target.position - transform.position;

        ShowDistance = dir.magnitude; 

        if (dir.magnitude > HideDistance)
        {
            SetChildrenActive(false);
        }
        else
        {
            SetChildrenActive(true);

            angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }

    }


}
