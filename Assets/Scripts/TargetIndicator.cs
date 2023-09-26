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

    public static TargetIndicator Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
